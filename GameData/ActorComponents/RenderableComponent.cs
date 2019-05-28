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
using Gamedata.ActorComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.ActorComponents
{
    public class RenderableComponent : ActorComponent
    {
        Rectangle actorRectangle;
        Texture2D actorTexture;
        public string ResourcePath;

        public override void Initialize(Game game, Actor actor)
        {
            base.Initialize(game, actor);
            actorTexture = game.Content.Load<Texture2D>(ResourcePath);
            actorRectangle = new Rectangle(Owner.Position.ToPoint(), new Point(100, 100));
        }

        public override void Update(GameTime gameTime)
        {
            actorRectangle.Location = Owner.Position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(actorTexture, actorRectangle, Color.White);
        }
    }
}