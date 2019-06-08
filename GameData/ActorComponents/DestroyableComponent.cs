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

namespace GameData.ActorComponents
{
    public class DestroyableComponent : ActorComponent
    {
        public event EventHandler Destroyed;

        public override void Initialize(ContentManager content, Actor owner)
        {
            Destroyed += DestroyableComponent_Destroyed;
            base.Initialize(content, owner);
        }

        private void DestroyableComponent_Destroyed(object sender, EventArgs e)
        {
            base.OnActionPerformed();
        }

        public void Destroy()
        {
            Destroyed?.Invoke(Owner, null);
        }
    }
}