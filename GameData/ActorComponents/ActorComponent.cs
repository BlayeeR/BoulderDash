using System;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameData.ActorComponents
{
    public abstract class ActorComponent : IUpdate
    {
        #region Properties
        [ContentSerializerIgnore]
        public virtual Actor Owner { get; protected set; }
        #endregion

        #region Fields
        public event EventHandler ActionPerformed;
        #endregion

        public virtual void Initialize(ContentManager content, Actor owner)
        {
            this.Owner = owner;
        }

        public virtual void Update(GameTime gameTime)
        {
        }
        
        protected void OnActionPerformed()
        {
            this.ActionPerformed?.Invoke(Owner, null);
        }
    }
}