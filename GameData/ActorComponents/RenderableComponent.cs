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
using GameData.ActorComponents;
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
        [ContentSerializerIgnore]
        public Color DrawColor;
        public Rectangle AtlasRectangle;
        [ContentSerializerIgnore]
        public bool DrawMe = true;

        public override void Initialize(ContentManager content, Actor actor)
        {
            base.Initialize(content, actor);
            actorTexture = content.Load<Texture2D>(ResourcePath);
            actorRectangle = new Rectangle(Owner.Position.ToPoint(), Owner.Size.ToPoint());
        }

        public override void Update(GameTime gameTime)
        {
            actorRectangle.Location = Owner.Position.ToPoint();
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(DrawMe)
                spriteBatch.Draw(actorTexture, actorRectangle, AtlasRectangle, DrawColor);
        }
    }
}