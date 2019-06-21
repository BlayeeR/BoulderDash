using System;
using System.Collections.Generic;
using System.Linq;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameData
{
    public class Menu : IComponent
    {
        #region ContentSerializerFields
        public string ID;
        [ContentSerializer]
        private readonly float Scale = 1;
        [ContentSerializer]
        private Color Color = Color.White;
        [ContentSerializer]
        private readonly string Font = "Fonts/DefaultFont";
        public float LineHeight;
        public Vector2 Position;
        public string Alignment;
        public List<MenuItem> Items = new List<MenuItem>();
        #endregion

        #region Fields
        public event EventHandler MenuItemTapped;
        #endregion

        #region Properties
        [ContentSerializerIgnore]
        public float TextScale { get => Items.Count == 0 ? 0.2f : Items.FirstOrDefault().Scale; set => Items.ForEach(x => x.Scale = value); }
        [ContentSerializerIgnore]
        public Color TextColor { get => Items.Count == 0 ? Color.White : Items.FirstOrDefault().TextColor; set => Items.ForEach(x => x.TextColor = value); }
        [ContentSerializerIgnore]
        public string FontResource { get => Items.Count == 0 ? "Fonts/DefaultFont" : Items.FirstOrDefault().FontResource; set => Items.ForEach(x => x.FontResource = value); }
        #endregion

        public void LoadContent(ContentManager content)
        {
            TextScale = Scale;
            TextColor = Color;
            FontResource = Font;
            Items.ForEach(x =>
            {
                x.LoadContent(content);
                x.OnTap += MenuItemTapped;
            });
            Align();
        }

        public void UnloadContent()
        {
            Items.ForEach(x => x.UnloadContent());
        }

        public void Update(GameTime gameTime)
        {
            Items.ForEach(x => x.Update(gameTime));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Items.ForEach(x => x.Draw(spriteBatch));
        }

        public void Align()
        {
            switch (Alignment)
            {
                case "LEFT":
                    {
                        Items.ForEach(x =>
                        {
                            x.Position = new Vector2(Position.X, Items.IndexOf(x) * LineHeight * x.CalculateSize().Y + Position.Y);
                        });
                        break;
                    }
                case "RIGHT":
                    {
                        Items.ForEach(x =>
                        {
                            x.Position = new Vector2(Position.X - x.CalculateSize().X, Items.IndexOf(x) * LineHeight * x.CalculateSize().Y + Position.Y);
                        });
                        break;
                    }
                default:
                case "CENTER":
                    {
                        Items.ForEach(x =>
                        {
                            x.Position = new Vector2((Position.X - x.CalculateSize().X) / 2, Items.IndexOf(x) * LineHeight * x.CalculateSize().Y + Position.Y);
                        });
                        break;
                    }

            }
        }
    }
}