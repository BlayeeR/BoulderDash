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
    public class Text : Sprite
    {
        public Color Color;
        public string String;
        public string LinkType;
        public string LinkID;
        private SpriteFont font;
        public override void Initialize(Game game)
        {
            font = game.Content.Load<SpriteFont>(ResourcePath);
        }

        public Vector2 TextSize()
        {
            return font.MeasureString(String)*Size;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, String, Position, Color,0.0f, Vector2.Zero, Size, SpriteEffects.None, 0.0f);
        }
    }
}