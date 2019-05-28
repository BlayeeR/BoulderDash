using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BoulderDash
{
    public class ActorManager
    {
        private uint lastActorID;
        private uint GetNextActorID { get { return ++lastActorID; }}

        public ActorManager()
        {
            lastActorID = 0;
        }

        public Actor2D CreateActor2D(string actorResource)
        {
            using (TextReader reader = new StreamReader(actorResource))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Actor2D));
                return serializer.Deserialize(reader) as Actor2D;
            }
        }

        public Actor2D CreateActor2DFromString(string actorResource)
        {
            using (TextReader reader = new StringReader(actorResource))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Actor2D));
                return serializer.Deserialize(reader) as Actor2D;
            }
        }

        public void SerializeActor2D(string filename, Actor2D actor)
        {
            using (TextWriter writer = new StreamWriter(filename))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Actor2D));
                xml.Serialize(writer, actor);
            }
        }

        public string SerializeActor2DToString(Actor2D actor)
        {
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer xml = new XmlSerializer(typeof(Actor2D));
                xml.Serialize(writer, actor);
                return writer.ToString();
            }
        }
    }
}