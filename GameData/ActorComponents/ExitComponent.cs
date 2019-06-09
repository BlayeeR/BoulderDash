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
    public class ExitComponent : ActorComponent
    {
        public event EventHandler ExitEntered;
        public bool IsOpen { get; private set; }
        public override void Initialize(ContentManager content, Actor owner)
        {
            IsOpen = false;
            base.Initialize(content, owner);
        }
        public void Open()
        {
            Owner.Components.OfType<RenderableComponent>().FirstOrDefault().AtlasRectangle = new Rectangle(32, 96, 16, 16);
            IsOpen = true;
        }

        public void OnEntered(Actor player)
        {
            Owner.Components.OfType<RenderableComponent>().FirstOrDefault().DrawMe = false;
            player.Components.OfType<PlayerComponent>().FirstOrDefault().LockMovement = true;
            ExitEntered?.Invoke(Owner, null);
        }
    }
}