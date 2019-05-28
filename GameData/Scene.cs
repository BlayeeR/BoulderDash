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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameData
{
    public class Scene 
    {
        [ContentSerializerIgnore]
        protected Game game;
        //public string BackgroundImage;
        public List<Sprite> Sprites = new List<Sprite>();

        public void Initialize(Game game)
        {
            Sprites.ForEach(x => x.Initialize(game));
            this.game = game;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprites.ForEach(x => x.Draw(spriteBatch));
        }
    }
}