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
using Microsoft.Xna.Framework.Graphics;

namespace GameData.Sprites
{
    public class Text : Sprite
    {
        public Color Color;
        public string String;
        private SpriteFont font;
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>(ResourcePath);
        }

        public Vector2 TextSize()
        {
            return font.MeasureString(String)*Size;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, String, Position, Color,0.0f, Vector2.Zero, Size, SpriteEffects.None, 0.0f);
        }

        public override bool Contains(Point point)
        {
            return (new Rectangle(Position.ToPoint(), TextSize().ToPoint())).Contains(point);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void UnloadContent()
        {
        }
    }
}