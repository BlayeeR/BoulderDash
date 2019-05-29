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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameShared.Interfaces
{
    public interface IComponent
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void LoadContent(ContentManager content);
        void UnloadContent();
    }
}