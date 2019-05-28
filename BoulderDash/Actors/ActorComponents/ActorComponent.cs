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
using Microsoft.Xna.Framework;

namespace BoulderDash.ActorComponents
{
    public abstract class ActorComponent
    {
        [XmlIgnore]
        public Actor2D Owner { get; set; }
        public abstract void Update(GameTime gameTime);

    }
}