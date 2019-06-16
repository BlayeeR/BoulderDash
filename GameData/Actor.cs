using GameData.ActorComponents;
using GameData.Maps;
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
    public class Actor : IComponent, IFocusable
    {
        public string Name;
        public uint ID;
        public Vector2 Position { get; set; }
        public List<ActorComponent> Components = new List<ActorComponent>();
        [ContentSerializerIgnore]
        public Vector2 Size { get { return new Vector2(16); } }
        [ContentSerializerIgnore]
        public ActorMap Owner;

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

        [ContentSerializerIgnore]
        public List<Actor> Neighbours
        {
            get
            {
                List<Actor> n = Owner.Actors.Where(x => x.Position.X >= Position.X - Size.X &&
                                                  x.Position.X <= Position.X + Size.X &&
                                                  x.Position.Y >= Position.Y - Size.Y &&
                                                  x.Position.Y <= Position.Y + Size.Y).ToList();
                //n.Remove(this);
                return n;
            }
        }
    }
}
