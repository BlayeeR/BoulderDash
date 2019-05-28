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
using Microsoft.Xna.Framework.Graphics;

namespace GameData.Sprites
{
    public abstract class Sprite
    {
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Size{get;set;}
        public virtual string ResourcePath { get; set; }

        public abstract void Initialize(Game game);
        public abstract void Draw(SpriteBatch spriteBatch);

    }
}