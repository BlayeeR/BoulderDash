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

namespace Gamedata.ActorComponents
{
    public class PlayerComponent : ActorComponent
    {
        public void MoveDown()
        {
            Owner.Position += new Vector2(0, -10);
        }

        public void MoveUp()
        {
            Owner.Position += new Vector2(0, 10);
        }

        public void MoveRight()
        {
            Owner.Position += new Vector2(10, 0);
        }

        public void MoveLeft()
        {
            Owner.Position += new Vector2(-10, 0);
        }

        public override void Update(GameTime gameTime)
        {
               
        }
    }
}