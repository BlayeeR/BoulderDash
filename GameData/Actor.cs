using GameData.ActorComponents;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace GameData
{
    public class Actor : IDraw, IUpdate, ILoad
    {
        public string Name;
        public uint ID;
        public Vector2 Position { get; set; }
        public List<ActorComponent> Components = new List<ActorComponent>();
        [ContentSerializerIgnore]
        public Vector2 Size { get { return new Vector2(16); } }

        public void Update(GameTime gameTime)
        {
            Components.ForEach(x => x.Update(gameTime));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Components.OfType<RenderableComponent>().FirstOrDefault().Draw(spriteBatch);
        }

        public void LoadContent(ContentManager content)
        {
            Components.ForEach(x => x.Initialize(content, this));
        }

        public void UnloadContent()
        {
        }
    }
}
