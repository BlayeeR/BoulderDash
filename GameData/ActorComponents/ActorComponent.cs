using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GameData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Gamedata.ActorComponents
{
    public abstract class ActorComponent
    {
        [ContentSerializerIgnore]
        public Actor Owner { get; private set; }
        public abstract void Update(GameTime gameTime);
        public virtual void Initialize(Game game, Actor owner)
        {
            this.Owner = owner;
        }

    }
}