using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class ActorMap : IComponent
    {
        public string Name;
        public int Time, DiamondsRequired, DiamondValue, BonusDiamondValue;
        public TileManager Tiles;

        public void Update(GameTime gameTime)
        {
            Tiles.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Tiles.Draw(spriteBatch);
        }

        public void LoadContent(ContentManager content)
        {
            Tiles.LoadContent(content);
        }

        public void UnloadContent()
        {
            Tiles.UnloadContent();
        }
    }
}