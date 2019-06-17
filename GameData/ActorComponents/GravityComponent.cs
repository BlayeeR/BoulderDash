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
        private double timer = 0;
        private bool stoppedFalling = false;
        public override void Initialize(ContentManager content, Actor owner)
        {
            RockFalling = content.Load<Song>("Sounds/RockHittingGround");
            base.Initialize(content, owner);
        }

        public override void Update(GameTime gameTime)
        {
            timer = (timer >= 500) ? 0 : timer + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer == 0)
            {
                if (!MoveDown())
                {//if cant
                    if (stoppedFalling)
                    {
                        if(RockFalling != null && MediaPlayer.Queue.ActiveSong != Owner.Owner.MapStartSound)
                            MediaPlayer.Play(RockFalling);
                        stoppedFalling = false;
                    }
                    //check neighbour in bottom left
                    Actor temp = Owner.Neighbours.Where(y => y.Position.X == Owner.Position.X - Owner.Size.X && y.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
                    //if there is any try to move left
                    if (!((temp == null || temp.IsPlayer) && MoveLeft()))
                    {//cant move left, entity in the way
                        //check right bottom
                        temp = Owner.Neighbours.Where(y => y.Position.X == Owner.Position.X + Owner.Size.X && y.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
                        //empty
                        if (temp == null || temp.IsPlayer)
                            //try move
                            MoveRight();
                    }
                }
                else
                    stoppedFalling = true;
                    
            }
            base.Update(gameTime);  
        }

    }
}