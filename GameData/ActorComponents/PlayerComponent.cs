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

namespace GameData.ActorComponents
{
    public class PlayerComponent : MovableComponent
    {
        private double timer = 0;
        private bool killed = false;

        public event EventHandler PlayerKilled;
        public override void Initialize(ContentManager content, Actor owner)
        {
            base.Initialize(content, owner);
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