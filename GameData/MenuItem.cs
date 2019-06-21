using System;
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
        #region ContentSerializerProperties
        public string Text { get => TextSprite.String; set => TextSprite.String = value; }
        #endregion

        #region ContentSerializerFields
        public string LinkType;
        public string LinkID;
        #endregion

        #region Fields
        public event EventHandler OnTap;
        private Text TextSprite = new Text();
        #endregion

        #region Properties
        [ContentSerializerIgnore]
        public string FontResource { get => TextSprite.ResourcePath; set => TextSprite.ResourcePath = value; }
        [ContentSerializerIgnore]
        public Color TextColor { get => TextSprite.Color; set => TextSprite.Color = value; }
        [ContentSerializerIgnore]
        public float Scale { get => TextSprite.Size.X; set => TextSprite.Size = new Vector2(value); }
        [ContentSerializerIgnore]
        public Vector2 Position { get => TextSprite.Position; set => TextSprite.Position = value; }
        #endregion

        public void LoadContent(ContentManager content)
        {
            TextSprite.LoadContent(content);
            InputManager.Instance.OnTap += Instance_OnTap;
        }

        public void UnloadContent()
        {
            TextSprite.UnloadContent();
        }

        private void Instance_OnTap(object sender, EventArgs e)
        {
            if (TextSprite.Contains(((GestureSample)sender).Position.ToPoint()))
                OnTap?.Invoke(this, null);
        }

        public void Update(GameTime gameTime)
        {
            TextSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TextSprite.Draw(spriteBatch);
        }

        public Vector2 CalculateSize()
        {
            return TextSprite.Size;
        }
    }
}