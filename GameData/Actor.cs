using Gamedata.ActorComponents;
using GameData.ActorComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace GameData
{
    public class Actor
    {
        public uint ID;
        public Vector2 Position { get; set; }
        public List<ActorComponent> Components = new List<ActorComponent>();

        public void Initialize(Game game)
        {
            Components.ForEach(x=>x.Initialize(game, this));
        }

        public void Update(GameTime gameTime)
        {
            Components.ForEach(x => x.Update(gameTime));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Components.OfType<RenderableComponent>().FirstOrDefault().Draw(spriteBatch);
        }
    }
}
