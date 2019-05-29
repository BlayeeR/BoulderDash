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
    public class Image : Sprite
    {
        private Rectangle rectangle;
        private Texture2D texture;
        private Vector2 position;
        [ContentSerializerIgnore]
        public override Vector2 Position { get => position; set { position = value;
                if (rectangle != null)
                    rectangle.Location = value.ToPoint();
            } }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public override bool Contains(Point point)
        {
            return rectangle.Contains(point);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(ResourcePath);
            rectangle = new Rectangle(Position.ToPoint(), Size.ToPoint());
        }

        public override void UnloadContent()
        {
        }
    }
}