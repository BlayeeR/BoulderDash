using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameData.ActorComponents
{
    public class ExitComponent : ActorComponent
    {
        #region Fields
        public event EventHandler ExitEntered;
        #endregion

        #region Properties
        [ContentSerializerIgnore]
        public bool IsOpen { get; private set; }
        #endregion

        public override void Initialize(ContentManager content, Actor owner)
        {
            IsOpen = false;
            base.Initialize(content, owner);
        }
        public void Open()
        {
            Owner.GetComponent<RenderableComponent>().AtlasRectangle = new Rectangle(32, 96, 16, 16);
            IsOpen = true;
        }

        public void OnEntered(Actor player)
        {
            Owner.GetComponent<RenderableComponent>().DrawMe = false;
            player.GetComponent<PlayerComponent>().LockMovement = true;
            ExitEntered?.Invoke(Owner, null);
        }
    }
}