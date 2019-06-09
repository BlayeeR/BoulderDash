﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GameData;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.ActorComponents
{
    public abstract class ActorComponent : IUpdate
    {
        private double timer = 0;
        private bool fireEvent = false;
        [ContentSerializerIgnore]
        public Actor Owner { get; private set; }
        public virtual void Update(GameTime gameTime)
        {
        }
        public event EventHandler ActionPerformed;
        public virtual void Initialize(ContentManager content, Actor owner)
        {
            this.Owner = owner;
        }

        protected void OnActionPerformed()
        {
            fireEvent = true;
            this.ActionPerformed?.Invoke(Owner, null);
        }
    }
}