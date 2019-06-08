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
using GameShared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameData.Sprites
{
    public class Text : Sprite
    {
        public Color Color;
        private string text;
        private List<Image> letters = new List<Image>();
        private Vector2 position;
        private ContentManager content;

        public event EventHandler Clicked;

        [ContentSerializerIgnore]
        public override Vector2 Position { get => position; set {
                position = value;
                float baseX = letters[0].Position.X;
                letters.ForEach(x => x.Position = new Vector2(x.Position.X - baseX + value.X, value.Y));
            } }

        [ContentSerializerIgnore]
        public override Vector2 Size { get {
                return new Vector2(letters[0].Size.X * letters.Count, letters[0].Size.Y);
            } }
        public string String { get => text; set
            {
                text = value;
                float lastPos = position.X;
                letters.ForEach(x => x.UnloadContent());
                letters.Clear();
                foreach (char c in String)
                {
                    switch (c)
                    {
                        case '/':
                            {
                                letters.Add(new Image(new Vector2(lastPos, position.Y), new Vector2(16, 8), "Textures/Sprites", Color, new Vector2(128, 32)));
                                break;
                            }
                        case char n when (n >= 40 && n <= 42):
                            {
                                letters.Add(new Image(new Vector2(lastPos, position.Y), new Vector2(16, 8), "Textures/Sprites", Color, new Vector2(128, 64 + (8 * (n - 40)))));
                                break;
                            }
                        case '_':
                            {
                                letters.Add(new Image(new Vector2(lastPos, position.Y), new Vector2(16, 8), "Textures/Sprites", Color, new Vector2(128, 256)));
                                break;
                            }
                        case char n when (n >= 48 && n <= 59):
                            {
                                letters.Add(new Image(new Vector2(lastPos, position.Y), new Vector2(16, 8), "Textures/Sprites", Color, new Vector2(128, 128 + (8 * (n - 48)))));
                                break;
                            }
                        case ' ':
                            {
                                letters.Add(new Image(new Vector2(lastPos, position.Y), new Vector2(16, 8), "Textures/Sprites", Color, new Vector2(128, 0)));
                                break;
                            }
                        case char n when (n >= 65 && n <= 90):
                            {
                                letters.Add(new Image(new Vector2(lastPos, position.Y), new Vector2(16, 8), "Textures/Sprites", Color, new Vector2(128, 264 + (8 * (n - 65)))));
                                break;
                            }
                        case char n when (n >= 97 && n <= 122):
                            {
                                letters.Add(new Image(new Vector2(lastPos, position.Y), new Vector2(16, 8), "Textures/Sprites", Color, new Vector2(128, 264 + (8 * (n - 97)))));
                                break;
                            }
                        case '[':
                            {
                                Color = Color.Yellow;
                                lastPos -= 16;
                                break;
                            }
                        case ']':
                            {
                                Color = Color.White;
                                lastPos -= 16;
                                break;
                            }

                    }
                    lastPos += 16;
                }
                if (content != null)
                    letters.ForEach(x => x.LoadContent(content));
            } }

        public Text()
        {
        }

        public Text(Vector2 position, string text, Color color)
        {
            this.position = position;
            this.Color = color;
            this.String = text;
        }

        public override void LoadContent(ContentManager content)
        {
            this.content = content;
            letters.ForEach(x => x.LoadContent(content));
            InputManager.Instance.OnTap += Instance_OnTap;
        }

        private void Instance_OnTap(object sender, EventArgs e)
        {
            if (sender is Vector2 && Contains(((Vector2)sender).ToPoint()))
                Clicked?.Invoke(this, null);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            letters.ForEach(x => x.Draw(spriteBatch));
        }

        public override bool Contains(Point point)
        {
            return (new Rectangle(Position.ToPoint(), Size.ToPoint())).Contains(point);
        }

        public override void Update(GameTime gameTime)
        {
            letters.ForEach(x => x.Update(gameTime));
        }

        public override void UnloadContent()
        {
            letters.ForEach(x => x.UnloadContent());
        }
    }
}