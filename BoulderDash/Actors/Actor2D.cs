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
using BoulderDash.ActorComponents;
using Microsoft.Xna.Framework;

namespace BoulderDash
{
    public class Actor2D
    {
        [XmlIgnore]
        public uint RefID { get; private set; }
        public uint BaseID { get; private set; }
        [XmlElement]
        public Vector2 Position { get; set; }

        [XmlArray("Components")]
        [XmlArrayItem(typeof(PhysicsComponent))]
        [XmlArrayItem(typeof(TestComponent))]
        public List<ActorComponent> Components = new List<ActorComponent>();

        public Actor2D()
        {
            RefID = 0;
        }

        public void AddComponent(ActorComponent component)
        {
            Components.Add(component);
        }

        public bool Initialize(uint RefID)
        {
            this.RefID = RefID;
            return true;
        }

        public bool Initialized()
        {
            if (RefID == 0)
                return false;
            return true;
        }

    }
}