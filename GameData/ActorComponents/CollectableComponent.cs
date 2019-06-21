using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GameData.ActorComponents
{
    public class CollectableComponent : ActorComponent
    {
        #region Fields
        public event EventHandler Collected;
        [ContentSerializerIgnore]
        public Song CollectedSound;
        #endregion

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