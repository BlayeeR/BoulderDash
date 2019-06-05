﻿using System;
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

namespace GameData.Maps
{
    public class ActorMap : IComponent
    {
        public string Name;
        public int Time, DiamondsRequired, DiamondValue, BonusDiamondValue;
        public TileManager Tiles;

        public void Update(GameTime gameTime)
        {
            Tiles.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Tiles.Draw(spriteBatch);
        }

        public void LoadContent(ContentManager content)
        {
            Tiles.LoadContent(content);
            InputManager.Instance.OnFlickDown += Instance_OnFlickDown;
            InputManager.Instance.OnFlickUp += Instance_OnFlickUp;
            InputManager.Instance.OnFlickLeft += Instance_OnFlickLeft;
            InputManager.Instance.OnFlickRight += Instance_OnFlickRight;
        }

        private void Instance_OnFlickRight(object sender, EventArgs e)
        {
            Tiles.Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault().Components.OfType<PlayerComponent>().FirstOrDefault().MoveRight();

        }

        private void Instance_OnFlickLeft(object sender, EventArgs e)
        {
            Tiles.Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault().Components.OfType<PlayerComponent>().FirstOrDefault().MoveLeft();
        }

        private void Instance_OnFlickUp(object sender, EventArgs e)
        {
            Actor player = Tiles.Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault();
            Actor neighbour = Tiles.Actors.Where(x => x.Position == player.Position + new Vector2(0, -16)).FirstOrDefault();
            if (player != null)
                if (neighbour != null)
                {
                    if (!neighbour.Components.OfType<BorderComponent>().Any())
                    {
                        player.Components.OfType<PlayerComponent>().FirstOrDefault().MoveUp();
                    }
                }
                else
                {
                    player.Components.OfType<PlayerComponent>().FirstOrDefault().MoveUp();
                }
            //.Components.Con.ForEach(y =>
            //{
            //    switch (y)
            //    {
            //        case BorderComponent b:
            //            {
            //                break;
            //            }
            //        default:
            //            {
            //                player.Components.OfType<PlayerComponent>().FirstOrDefault().MoveUp();
            //                break;
            //            }
            //    }
            //});
        }

        private void Instance_OnFlickDown(object sender, EventArgs e)
        {
            Tiles.Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault().Components.OfType<PlayerComponent>().FirstOrDefault().MoveDown();
        }

        public void UnloadContent()
        {
            Tiles.UnloadContent();
        }
    }
}