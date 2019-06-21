using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.ActorComponents
{
    public class RenderableComponent : ActorComponent
    {
        #region Fields
        private Rectangle actorRectangle;
        private Texture2D actorTexture;
        [ContentSerializerIgnore]
        public Color DrawColor;
        [ContentSerializerIgnore]
        public bool DrawMe = true;
        #endregion

        #region ContentSerializerFields
        public string ResourcePath;
        public Rectangle AtlasRectangle;
        #endregion

        public override void Initialize(ContentManager content, Actor actor)
        {
            base.Initialize(content, actor);
            actorTexture = content.Load<Texture2D>(ResourcePath);
            actorRectangle = new Rectangle(Owner.Position.ToPoint(), Owner.Size.ToPoint());
        }

        public override void Update(GameTime gameTime)
        {
            actorRectangle.Location = Owner.Position.ToPoint();
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(DrawMe)
                spriteBatch.Draw(actorTexture, actorRectangle, AtlasRectangle, DrawColor);
        }
    }
}