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
            Owner.PositionChanged += Owner_PositionChanged;
            actorTexture = content.Load<Texture2D>(ResourcePath);
            actorRectangle = new Rectangle(Owner.Position.ToPoint(), Owner.Size.ToPoint());
        }

        private void Owner_PositionChanged(object sender, System.EventArgs e)
        {
            actorRectangle.Location = Owner.Position.ToPoint();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(DrawMe)
                spriteBatch.Draw(actorTexture, actorRectangle, AtlasRectangle, DrawColor);
        }
    }
}