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

namespace GameData.ActorComponents
{
    public class PlayerComponent : ActorComponent
    {
        public event EventHandler OnPlayerMoved;
        public override void Initialize(ContentManager content, Actor owner)
        {
            base.Initialize(content, owner);
            InputManager.Instance.OnFlickDown += PlayerMoveDown;
            InputManager.Instance.OnFlickUp += PlayerMoveUp;
            InputManager.Instance.OnFlickLeft += PlayerMoveLeft;
            InputManager.Instance.OnFlickRight += PlayerMoveRight;

        }

        private void PlayerMoveRight(object sender, EventArgs e)
        {
            Owner.Position += new Vector2(Owner.Size.X, 0);
            OnPlayerMoved?.Invoke(Owner, null);
        }

        private void PlayerMoveLeft(object sender, EventArgs e)
        {
            Owner.Position += new Vector2(-Owner.Size.X, 0);
            OnPlayerMoved?.Invoke(Owner, null);
        }

        private void PlayerMoveUp(object sender, EventArgs e)
        {
            Owner.Position += new Vector2(0, Owner.Size.Y);
            OnPlayerMoved?.Invoke(Owner, null);
        }

        private void PlayerMoveDown(object sender, EventArgs e)
        {
            Owner.Position += new Vector2(0, -Owner.Size.Y);
            OnPlayerMoved?.Invoke(Owner, null);
        }

        public override void Update(GameTime gameTime)
        {
               
        }
    }
}