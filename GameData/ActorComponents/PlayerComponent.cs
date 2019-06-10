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
using GameShared;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameShared.Interfaces;
using Microsoft.Xna.Framework.Media;

namespace GameData.ActorComponents
{
    public class PlayerComponent : MovableComponent
    {
        private double timer = 0;
        private bool killed = false;
        private Song MovedSound;

        public event EventHandler PlayerKilled;
        public override void Initialize(ContentManager content, Actor owner)
        {
            MovedSound = content.Load<Song>("Sounds/Move");
            base.ActionPerformed += PlayerComponent_ActionPerformed;
            base.Initialize(content, owner);
        }

        private void PlayerComponent_ActionPerformed(object sender, EventArgs e)
        {
            if(MediaPlayer.State != MediaState.Playing)
                MediaPlayer.Play(MovedSound);
        }

        public override void Update(GameTime gameTime)
        {
            timer = (timer >= 500) ? 0 : timer + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer == 0 && killed)
            {
                PlayerKilled?.Invoke(Owner, null);
            }
            base.Update(gameTime);
        }

        public void Kill()
        {
            Owner.Components.OfType<RenderableComponent>().FirstOrDefault().DrawMe = false;
            Owner.Components.OfType<MovableComponent>().FirstOrDefault().LockMovement = true;
            killed = true;
        }
    }
}