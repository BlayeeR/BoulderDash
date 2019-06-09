﻿using System;
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
        public event EventHandler PlayerKilled;
        public override void Initialize(ContentManager content, Actor owner)
        {
            base.Initialize(content, owner);
        }

        public void Kill()
        {
            PlayerKilled?.Invoke(Owner, null);
        }
    }
}