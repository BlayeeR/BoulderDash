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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GameData.ActorComponents
{
    public class GravityComponent : MovableComponent
    {
        private Song RockFalling;
        private bool checkedInCurrentTick = false;
        public event EventHandler StartedFalling, StoppedFalling;
        [ContentSerializerIgnore]
        public bool IsFalling { get; private set; }

        public override Actor Owner { get => base.Owner; protected set {
                base.Owner = value;
                value.Owner.MapStarted += Owner_MapStarted;
            } }


        public override void Initialize(ContentManager content, Actor owner)
        {
            RockFalling = content.Load<Song>("Sounds/RockHittingGround");
            StartedFalling += GravityComponent_StartedFalling;
            StoppedFalling += GravityComponent_StoppedFalling;
            base.Initialize(content, owner);
        }

        private void GravityComponent_StoppedFalling(object sender, EventArgs e)
        {
            IsFalling = false;
            if (RockFalling != null && MediaPlayer.Queue.ActiveSong != Owner.Owner.MapStartSound)
                MediaPlayer.Play(RockFalling);
        }

        private void GravityComponent_StartedFalling(object sender, EventArgs e)
        {
            IsFalling = true;
        }

        private void Owner_MapStarted(object sender, EventArgs e)
        {
            base.checkSurroundings++;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            checkedInCurrentTick = false;
        }

        public void TryMove()
        {
            if (!checkedInCurrentTick)
            {
                if (!MoveDown())
                {//if cant
                    Actor temp = Owner.CloseNeighbours.Where(y => y.Position.X == Owner.Position.X && y.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
                    if (temp != null && temp.HasComponent<GravityComponent>())
                    {
                        //check neighbour in bottom left
                        temp = Owner.CloseNeighbours.Where(y => y.Position.X == Owner.Position.X - Owner.Size.X && y.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
                        //if there is any try to move left
                        if (!((temp == null || temp.IsPlayer) && MoveLeft()))
                        {//cant move left, entity in the way
                         //check right bottom
                            temp = Owner.CloseNeighbours.Where(y => y.Position.X == Owner.Position.X + Owner.Size.X && y.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
                            //empty, //try move
                            if (!((temp == null || temp.IsPlayer) && MoveRight()))
                            {
                                if (IsFalling)
                                    StoppedFalling?.Invoke(Owner, null);
                            }
                            else if (!IsFalling)
                                StartedFalling?.Invoke(Owner, null);
                        }
                        else if (!IsFalling)
                            StartedFalling?.Invoke(Owner, null);
                    }
                    else if (IsFalling)
                        StoppedFalling?.Invoke(Owner, null);
                }
                else if(!IsFalling)
                    StartedFalling?.Invoke(Owner, null);
                checkedInCurrentTick = true;
            }
        }
    }
}