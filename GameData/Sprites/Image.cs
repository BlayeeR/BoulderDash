using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.Sprites
{
    public class Image : Sprite
    {
        #region Fields
        private Rectangle sourceRectangle;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 size;
        private Rectangle atlasRectangle;
        #endregion

        #region ContentSerializerFields
        public Color Color;
        #endregion

        #region Properties
        [ContentSerializerIgnore]
        public override Vector2 Position { get => position; set { position = value;
                if (sourceRectangle != null)
                    sourceRectangle.Location = value.ToPoint();
            } }
        [ContentSerializerIgnore]
        public override Vector2 Size
        {
            get => size; set
            {
                size = value;
                if (sourceRectangle != null)
                    sourceRectangle.Size = value.ToPoint();
            }
        }
        #endregion

        public Image()
        { }

        public Image(Vector2 position, Vector2 size, string texturePath, Color color, Vector2 atlasPosition)
        {
            this.position = position;
            this.size = size;
            this.ResourcePath = texturePath;
            this.Color = color;
            if(atlasPosition !=Vector2.Zero)
                atlasRectangle = new Rectangle(atlasPosition.ToPoint(), size.ToPoint());
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(ResourcePath);
            sourceRectangle = new Rectangle(Position.ToPoint(), Size.ToPoint());
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!atlasRectangle.IsEmpty)
                spriteBatch.Draw(texture, sourceRectangle, atlasRectangle, Color);
            else
                spriteBatch.Draw(texture, sourceRectangle, Color);
        }

        public override bool Contains(Point point)
        {
            return sourceRectangle.Contains(point);
        }
    }
}