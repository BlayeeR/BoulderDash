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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GameData.ActorComponents
{
    public class CollectableComponent : ActorComponent
    {
        public event EventHandler Collected;
        [ContentSerializerIgnore]
        public Song CollectedSound;

        public override void Initialize(ContentManager content, Actor owner)
        {
            CollectedSound = content.Load<Song>("Sounds/Collect");
            base.Initialize(content, owner);
        }

        public void Collect()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(CollectedSound);
            Collected?.Invoke(Owner, null);
            base.OnActionPerformed();
        }
    }
}