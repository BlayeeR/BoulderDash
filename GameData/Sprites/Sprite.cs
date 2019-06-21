using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.Sprites
{
    public abstract class Sprite : IComponent
    {
        #region Properties
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Size{get;set;}
        public virtual string ResourcePath { get; set; }
        #endregion

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract bool Contains(Point point);
        public abstract void Update(GameTime gameTime);
        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
    }
}