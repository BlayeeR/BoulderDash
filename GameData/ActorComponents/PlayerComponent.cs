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
using Shared;

namespace GameData.ActorComponents
{
    public class PlayerComponent : ActorComponent
    {
        public override void Initialize(Game game, Actor owner)
        {
            base.Initialize(game, owner);
            InputManager.Instance.FlickDown += PlayerMoveDown;
            InputManager.Instance.FlickUp += PlayerMoveUp;
            InputManager.Instance.FlickLeft += PlayerMoveLeft;
            InputManager.Instance.FlickRight += PlayerMoveRight;
        }

        private void PlayerMoveRight(object sender, EventArgs e)
        {
            Owner.Position += new Vector2(Owner.Size.X, 0);
        }

        private void PlayerMoveLeft(object sender, EventArgs e)
        {
            Owner.Position += new Vector2(-Owner.Size.X, 0);
        }

        private void PlayerMoveUp(object sender, EventArgs e)
        {
            Owner.Position += new Vector2(0, Owner.Size.Y);
        }

        private void PlayerMoveDown(object sender, EventArgs e)
        {
            Owner.Position += new Vector2(0, -Owner.Size.Y);
        }

        public override void Update(GameTime gameTime)
        {
               
        }
    }
}