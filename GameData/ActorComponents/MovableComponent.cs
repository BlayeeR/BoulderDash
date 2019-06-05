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

namespace GameData.ActorComponents
{
    public class MovableComponent : ActorComponent
    {
        public event EventHandler OnActorMoved;
        public void MoveRight()
        {
            Owner.Position += new Vector2(Owner.Size.X, 0);
            OnActorMoved?.Invoke(Owner, null);
        }

        public void MoveLeft()
        {
            Owner.Position += new Vector2(-Owner.Size.X, 0);
            OnActorMoved?.Invoke(Owner, null);
        }

        public void MoveUp()
        {
            Owner.Position += new Vector2(0, -Owner.Size.Y);
            OnActorMoved?.Invoke(Owner, null);
        }

        public void MoveDown()
        {
            Owner.Position += new Vector2(0, Owner.Size.Y);
            OnActorMoved?.Invoke(Owner, null);
        }
        public override void Update(GameTime gameTime)
        {
            
        }
    }
}