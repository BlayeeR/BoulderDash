using System;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace GameData.ActorComponents
{
    public class DestroyableComponent : ActorComponent
    {
        #region Fields
        public event EventHandler Destroyed;
        #endregion

        public override void Initialize(ContentManager content, Actor owner)
        {
            base.Initialize(content, owner);
        }

        public void Destroy()
        {
            Destroyed?.Invoke(Owner, null);
            base.OnActionPerformed();
        }
    }
}