using GameData.ActorComponents;
using GameData.Maps;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameData
{
    public class Actor : IComponent, IFocusable
    {
        #region ContentSerializerFields
        public string Name;
        public uint ID;
        [ContentSerializer]
        private List<ActorComponent> Components = new List<ActorComponent>();
        #endregion

        #region ContentSerializerProperties
        public Vector2 Position { get => position;
            set
            {
                position = value;
                PositionChanged?.Invoke(this, null);
            }
        }
        #endregion

        #region Fields
        [ContentSerializerIgnore]
        public ActorMap Owner;
        private Vector2 position;

        public event EventHandler PositionChanged;
        #endregion

        #region Properties
        [ContentSerializerIgnore]
        public Vector2 Size { get { return new Vector2(16); } }
        [ContentSerializerIgnore]
        public List<Actor> CloseNeighbours
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
        [ContentSerializerIgnore]
        public List<Actor> Neighbours
        {
            get
            {
                List<Actor> n = Owner.Actors.Where(x => x.Position.X >= Position.X - Size.X * 2 &&
                                                  x.Position.X <= Position.X + Size.X * 2 &&
                                                  x.Position.Y >= Position.Y - Size.Y * 2 &&
                                                  x.Position.Y <= Position.Y + Size.Y * 2).ToList();
                //n.Remove(this);
                return n;
            }
        }
        [ContentSerializerIgnore]
        public bool IsPlayer { get { return HasComponent<PlayerComponent>(); } }
        #endregion

        public void LoadContent(ContentManager content)
        {
            Components.ForEach(x => x.Initialize(content, this));
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            Components.ForEach(x => x.Update(gameTime));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Components.OfType<RenderableComponent>().FirstOrDefault().Draw(spriteBatch);
        }

        public bool HasComponent<T>()
        {
            return Components.OfType<T>().Any();
        }

        public T GetComponent<T>()
        {
            return Components.OfType<T>().First();
        }

        public void AddComponent(ActorComponent component)
        {
            Components.Add(component);
        }
        public void RemoveComponent(ActorComponent component)
        {
            Components.Remove(component);
        }
    }
}
