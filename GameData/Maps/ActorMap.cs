using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GameData.ActorComponents;
using GameData.Sprites;
using GameShared;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameData.Maps
{
    public class ActorMap : IComponent
    {
        private double calculateScoreTimer = 0;
        private bool ended = false;
        public int ID;
        [ContentSerializer]
#pragma warning disable IDE0044 // Add readonly modifier
        private string Description, DiamondsRequiredValue, TimeValue;
        [ContentSerializer]
        private int SlowGrowth;
        public int DiamondValue, BonusDiamondValue;
        [ContentSerializerIgnore]
        public int DiamondsRequired, Time, Score=0, DiamondsCollected=0;
        private double timer = 0;
        [ContentSerializer]
        private Color BackgroundColor1, BackgroundColor2, ForegroundColor;
        public Vector2 TileDimensions;
        [ContentSerializerIgnore]
        public Song MapStartSound, MapEndSound;
        [ContentSerializer(ElementName = "Raw")]
        private List<string> raw;
        [ContentSerializerIgnore]
        public List<Actor> Actors = new List<Actor>();
        [ContentSerializerIgnore]
        public Vector2 Size;
        [ContentSerializerIgnore]
        public Actor Player { get; private set; }
        public event EventHandler PlayerKilled, MapCompleted;
#pragma warning restore IDE0044 // Add readonly modifier
        public void Update(GameTime gameTime)
        {
            Actors.ForEach(x=>x.Update(gameTime));
            timer = (timer >= 1000) ? 0 : timer + gameTime.ElapsedGameTime.TotalMilliseconds;
            calculateScoreTimer = (calculateScoreTimer >= 10) ? 0 : calculateScoreTimer + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer == 0 && !ended)
            {
                Time--;
                if (Time == 0)
                    Player.GetComponent<PlayerComponent>().Kill();
            }
            if(ended &&calculateScoreTimer == 0)
            {
                if (Time == 0)
                {
                    MapCompleted?.Invoke(this, null);
                }
                else
                {
                    Time--;
                    Score++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Actors.ForEach(x => x.Draw(spriteBatch));
        }

        public void LoadContent(ContentManager content)
        {
            //i = row y, j column x
            for(int i = 0; i < raw.Count; i++)
            {
                for (int j = 0; j < raw[i].Length; j++)
                {
                    Actor actor = null;
                    using (ContentManager c = new ContentManager(content.ServiceProvider, content.RootDirectory))
                    {
                        switch (raw[i][j])
                        {
                            case 'W':
                                {
                                    actor = c.Load<Actor>("Actors/Border");
                                    break;
                                }
                            case 'X':
                                {
                                    actor = c.Load<Actor>("Actors/Player");
                                    Player = actor;
                                    break;
                                }
                            case '.':
                                {
                                    actor = c.Load<Actor>("Actors/Dirt");
                                    break;
                                }
                            case 'w':
                                {
                                    actor = c.Load<Actor>("Actors/Wall");
                                    break;
                                }
                            case 'r':
                                {
                                    actor = c.Load<Actor>("Actors/Boulder");
                                    break;
                                }
                            case 'd':
                                {
                                    actor = c.Load<Actor>("Actors/Diamond");
                                    break;
                                }
                            case 'P':
                                {
                                    actor = c.Load<Actor>("Actors/Exit");
                                    break;
                                }
                        }
                    }
                    if (actor != null)
                    {
                        actor.LoadContent(content);
                        actor.GetComponent<RenderableComponent>().DrawColor = ForegroundColor;
                        actor.Owner = this;
                        actor.Position = new Vector2(j * TileDimensions.X, i * TileDimensions.Y);
                        Actors.Add(actor);
                    }
                }
            }
            Actors.Where(x => x.HasComponent<ExitComponent>()).ToList().ForEach(x => x.GetComponent<ExitComponent>().ExitEntered += ActorMap_ExitEntered);
            Actors.Where(x => x.HasComponent<DestroyableComponent>()).ToList().ForEach(x => x.GetComponent<DestroyableComponent>().Destroyed += ActorMap_ActorDestroyed);
            Actors.Where(x => x.HasComponent<PlayerComponent>()).ToList().ForEach(x => x.GetComponent<PlayerComponent>().PlayerKilled += ActorMap_PlayerKilled);
            Actors.Where(x => x.HasComponent<CollectableComponent>()).ToList().ForEach(x => x.GetComponent<CollectableComponent>().Collected += ActorMap_Collected);

            //force 1lvl
            Time = Int32.Parse(TimeValue.Split(" ").FirstOrDefault());
            DiamondsRequired = Int32.Parse(DiamondsRequiredValue.Split(" ").FirstOrDefault());

            Size = new Vector2(Actors.Last().Position.X-Actors[0].Position.X+TileDimensions.X,Actors.Last().Position.Y-Actors[0].Position.Y+TileDimensions.Y);
            InputManager.Instance.OnFlickDown += Instance_OnFlickDown;
            InputManager.Instance.OnFlickUp += Instance_OnFlickUp;
            InputManager.Instance.OnFlickLeft += Instance_OnFlickLeft;
            InputManager.Instance.OnFlickRight += Instance_OnFlickRight;
            MapStartSound = content.Load<Song>("Sounds/MapStart");
            MapEndSound = content.Load<Song>("Sounds/MapComplete");
            MediaPlayer.Play(MapStartSound);
        }

        private void ActorMap_ExitEntered(object sender, EventArgs e)
        {
            ended = true;
            MediaPlayer.Stop();
            MediaPlayer.Play(MapEndSound);
        }

        private void ActorMap_PlayerKilled(object sender, EventArgs e)
        {
            PlayerKilled?.Invoke(sender, null);
        }

        private void ActorMap_Collected(object sender, EventArgs e)
        {
            Actors.Remove(sender as Actor);
            (sender as Actor).GetComponent<CollectableComponent>().Collected -= ActorMap_Collected;
            if (DiamondsRequired > DiamondsCollected)
                Score += DiamondValue;
            else
                Score += BonusDiamondValue;
            DiamondsCollected++;
            if(DiamondsCollected== DiamondsRequired && !Actors.Where(x => x.HasComponent<ExitComponent>()).FirstOrDefault().GetComponent<ExitComponent>().IsOpen)
            {
                Actor exit = Actors.Where(x => x.HasComponent<ExitComponent>()).FirstOrDefault();
                exit.RemoveComponent(exit.GetComponent<BorderComponent>());
                exit.GetComponent<ExitComponent>().Open();
            }
        }

        private void ActorMap_ActorDestroyed(object sender, EventArgs e)
        {
            Actors.Remove(sender as Actor);
            (sender as Actor).GetComponent<DestroyableComponent>().Destroyed -= ActorMap_ActorDestroyed;
        }

        private void Instance_OnFlickRight(object sender, EventArgs e)
        {
            Player.GetComponent<PlayerComponent>().MoveRight();
        }

        private void Instance_OnFlickLeft(object sender, EventArgs e)
        {
            Player.GetComponent<PlayerComponent>().MoveLeft();
        }

        private void Instance_OnFlickUp(object sender, EventArgs e)
        {
            Player.GetComponent<PlayerComponent>().MoveUp();
        }

        private void Instance_OnFlickDown(object sender, EventArgs e)
        {
            Player.GetComponent<PlayerComponent>().MoveDown();
        }

        public void UnloadContent()
        {
            Actors.Where(x => x.HasComponent<DestroyableComponent>()).ToList().ForEach(x => x.GetComponent<DestroyableComponent>().Destroyed -= ActorMap_ActorDestroyed);
            Actors.ForEach(x=>x.UnloadContent());
        }

    }
}