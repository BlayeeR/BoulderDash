using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GameData.ActorComponents
{
    public class GravityComponent : MovableComponent
    {
        #region Fields
        private Song RockFalling;
        private double timer = 0;
        private int checkSurroundings = 0;
        private bool checkedInCurrentTick = false;
        public event EventHandler StartedFalling, StoppedFalling;
        #endregion

        #region Properties
        [ContentSerializerIgnore]
        public bool IsFalling { get; private set; }
        #endregion

        public void CheckSurroundings()
        {
            checkSurroundings++;
        }

        public override Actor Owner { get => base.Owner; protected set {
                base.Owner = value;
                value.Owner.MapStarted += Owner_MapStarted;
            } }


        public override void Initialize(ContentManager content, Actor owner)
        {
            RockFalling = content.Load<Song>("Sounds/RockHittingGround");
            StartedFalling += GravityComponent_StartedFalling;
            StoppedFalling += GravityComponent_StoppedFalling;
            base.ActionPerformed += GravityComponent_ActionPerformed;
            base.Initialize(content, owner);
        }

        private void GravityComponent_ActionPerformed(object sender, EventArgs e)
        {
            checkSurroundings++;
        }

        public override void Update(GameTime gameTime)
        {
            timer = (timer >= 250) ? 0 : timer + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer == 0)
            {
                if (checkSurroundings != 0)
                {
                    checkSurroundings--;
                    TryMove();
                }
            }
            checkedInCurrentTick = false;
            base.Update(gameTime);
        }

        private void GravityComponent_StoppedFalling(object sender, EventArgs e)
        {
            IsFalling = false;
            if (RockFalling != null && MediaPlayer.Queue.ActiveSong != Owner.Owner.MapStartSound)
                MediaPlayer.Play(RockFalling);
        }

        private void GravityComponent_StartedFalling(object sender, EventArgs e)
        {
            IsFalling = true;
        }

        private void Owner_MapStarted(object sender, EventArgs e)
        {
            checkSurroundings++;
        }

        public void TryMove()
        {
            if (!checkedInCurrentTick)
            {
                if (!MoveDown())
                {//if cant
                    Actor temp = Owner.CloseNeighbours.Where(y => y.Position.X == Owner.Position.X && y.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
                    if (temp != null && temp.HasComponent<GravityComponent>())
                    {
                        //check neighbour in bottom left
                        temp = Owner.CloseNeighbours.Where(y => y.Position.X == Owner.Position.X - Owner.Size.X && y.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
                        //if there is any try to move left
                        if (!((temp == null || temp.IsPlayer) && MoveLeft()))
                        {//cant move left, entity in the way
                         //check right bottom
                            temp = Owner.CloseNeighbours.Where(y => y.Position.X == Owner.Position.X + Owner.Size.X && y.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
                            //empty, //try move
                            if (!((temp == null || temp.IsPlayer) && MoveRight()))
                            {
                                if (IsFalling)
                                    StoppedFalling?.Invoke(Owner, null);
                            }
                            else if (!IsFalling)
                                StartedFalling?.Invoke(Owner, null);
                        }
                        else if (!IsFalling)
                            StartedFalling?.Invoke(Owner, null);
                    }
                    else if (IsFalling)
                        StoppedFalling?.Invoke(Owner, null);
                }
                else if(!IsFalling)
                    StartedFalling?.Invoke(Owner, null);
                checkedInCurrentTick = true;
            }
        }
    }
}