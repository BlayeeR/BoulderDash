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
using GameData.Sprites;
using GameShared;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameData
{
    public class MenuItem : IComponent
    {
        private Text TextSprite = new Text();
        public string Text { get => TextSprite.String; set => TextSprite.String = value; }
        public string LinkType;
        public string LinkID;
        [ContentSerializerIgnore]
        public string FontResource { get => TextSprite.ResourcePath; set => TextSprite.ResourcePath = value; }
        [ContentSerializerIgnore]
        public Color TextColor { get => TextSprite.Color; set => TextSprite.Color = value; }
        [ContentSerializerIgnore]
        public float Scale { get => TextSprite.Size.X; set => TextSprite.Size = new Vector2(value); }
        [ContentSerializerIgnore]
        public Vector2 Position { get => TextSprite.Position; set => TextSprite.Position = value; }

        private void Instance_Tap(object sender, EventArgs e)
        {
            if (TextSprite.Contains(((GestureSample)sender).Position.ToPoint()))
                TextSprite.Color = Color.Black;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TextSprite.Draw(spriteBatch);
        }

        public Vector2 CalculateSize()
        {
            return TextSprite.TextSize();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void LoadContent(ContentManager content)
        {
            TextSprite.LoadContent(content);
            InputManager.Instance.Tap += Instance_Tap;
        }

        public void UnloadContent()
        {
        }
    }
}