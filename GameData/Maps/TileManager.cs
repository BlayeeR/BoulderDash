using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GameData.Sprites;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.Maps
{
    public class TileManager : IComponent
    {
        public Vector2 TileDimensions;
        [ContentSerializer]
        private List<string> Rows;
        [ContentSerializerIgnore]
        public List<Actor> Actors = new List<Actor>();
        private readonly string regexPattern = "(\\d)";

        public void Update(GameTime gameTime)
        {
            Actors.ForEach(x => x.Update(gameTime));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Actors.ForEach(x => x.Draw(spriteBatch));
        }

        public void LoadContent(ContentManager content)
        {
            Regex regex = new Regex(regexPattern);
            MatchCollection matches;
            for (int i = 0; i < Rows.Count; i++)
            {
                matches = regex.Matches(Rows[i]);
                for (int j = 0; j < matches.Count; j++)
                {
                    if (!matches[j].Groups[1].Value.Equals("0"))
                    {
                        using (ContentManager c = new ContentManager(content.ServiceProvider, content.RootDirectory))
                        {
                            Actor actor = c.Load<Actor>($"Actors/{matches[j].Groups[1].Value}");
                            actor.Position = new Vector2(j * TileDimensions.X, i * TileDimensions.Y);
                            actor.LoadContent(content);
                            Actors.Add(actor);
                        }
                    }
                }
            }
        }

        public void UnloadContent()
        {
            Actors.ForEach(x => x.UnloadContent());
        }
    }
}