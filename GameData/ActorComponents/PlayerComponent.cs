using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GameData.ActorComponents
{
    public class PlayerComponent : MovableComponent
    {
        #region Fields
        private double timer = 0;
        private bool killed = false;
        private Song MovedSound;
        public event EventHandler PlayerKilled;
        #endregion

        public override void Initialize(ContentManager content, Actor owner)
        {
            MovedSound = content.Load<Song>("Sounds/Move");
            base.ActionPerformed += PlayerComponent_ActionPerformed;
            base.Initialize(content, owner);
        }

        public override void Update(GameTime gameTime)
        {
            timer = (timer >= 500) ? 0 : timer + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer == 0 && killed)
            {
                PlayerKilled?.Invoke(Owner, null);
            }
            base.Update(gameTime);
        }

        private void PlayerComponent_ActionPerformed(object sender, EventArgs e)
        {
            if (MediaPlayer.State != MediaState.Playing)
                MediaPlayer.Play(MovedSound);
        }

        public void Kill()
        {
            Owner.GetComponent<RenderableComponent>().DrawMe = false;
            Owner.GetComponent<MovableComponent>().LockMovement = true;
            killed = true;
        }
    }
}