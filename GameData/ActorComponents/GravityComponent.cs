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
using Microsoft.Xna.Framework.Content;

namespace GameData.ActorComponents
{
    public class GravityComponent : MovableComponent
    {
        
        public override void Initialize(ContentManager content, Actor owner)
        {
            base.Initialize(content, owner);
        }

    }
}