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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameData.ActorComponents
{
    public class MovableComponent : ActorComponent
    {
        private bool tryKill = false;
        private Vector2 killPosition = Vector2.Zero;
        private double timer = 0;

        public override void Update(GameTime gameTime)
        {
            timer = (timer >= 1000) ? 0 : timer + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer == 0 && tryKill)
            {
                if (Owner.Owner.Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault().Position == killPosition)
                    Owner.Owner.Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault().Components.OfType<PlayerComponent>().FirstOrDefault().Kill();
                tryKill = false;
            }
            base.Update(gameTime);
        }

        public override void Initialize(ContentManager content, Actor owner)
        {
            base.Initialize(content, owner);
        }

        public bool MoveRight()
        {
            Actor target = Owner.Neighbours.Where(x => x.Position.X == Owner.Position.X + Owner.Size.X && x.Position.Y == Owner.Position.Y).FirstOrDefault();
            //player wants to move
            if (Owner.Components.OfType<PlayerComponent>().Any())
            {
                try
                {
                    //border in the way, cant move
                    if (target.Components.OfType<BorderComponent>().Any())
                        return false;
                    //movable object in the way
                    if (target.Components.OfType<MovableComponent>().Any())
                    {
                        //try to move
                        if (target.Components.OfType<MovableComponent>().FirstOrDefault().MoveRight())
                        {
                            //object moved, move player
                            Owner.Position += new Vector2(Owner.Size.X, 0);
                            base.OnActionPerformed();
                            return true;
                        }
                    }
                    //destroyable object in the way
                    else if (target.Components.OfType<DestroyableComponent>().Any())
                    {
                        //destroy object
                        target.Components.OfType<DestroyableComponent>().FirstOrDefault().Destroy();
                        //move
                        Owner.Position += new Vector2(Owner.Size.X, 0);
                        base.OnActionPerformed();
                        return true;

                    }
                    else if (target.Components.OfType<CollectableComponent>().Any())
                    {
                        //collect object
                        target.Components.OfType<CollectableComponent>().FirstOrDefault().Collect();
                        //move
                        Owner.Position += new Vector2(Owner.Size.X, 0);
                        base.OnActionPerformed();
                        return true;

                    }
                }
                catch
                {
                    //empty space, move
                    Owner.Position += new Vector2(Owner.Size.X, 0);
                    base.OnActionPerformed();
                    return true;
                }
            }
            else //object wants to move
            {
                //no other entity in the way
                if(target == null || target.Components.OfType<PlayerComponent>().Any())
                {
                    //move

                    if (target != null)
                    {
                        killPosition = target.Position;
                        tryKill = true;
                    }
                    base.OnActionPerformed();
                    Owner.Position += new Vector2(Owner.Size.X, 0);
                    return true;
                }
            }
            return false;
        }

        public bool MoveLeft()
        {
            Actor target = Owner.Neighbours.Where(x => x.Position.X == Owner.Position.X - Owner.Size.X && x.Position.Y == Owner.Position.Y).FirstOrDefault();
            //player wants to move
            if (Owner.Components.OfType<PlayerComponent>().Any())
            {
                try
                {
                    //border in the way, cant move
                    if (target.Components.OfType<BorderComponent>().Any())
                        return false;
                    //movable object in the way
                    if (target.Components.OfType<MovableComponent>().Any())
                    {
                        //try to move
                        if (target.Components.OfType<MovableComponent>().FirstOrDefault().MoveLeft())
                        {
                            //object moved, move player
                            Owner.Position += new Vector2(-Owner.Size.X, 0);
                            base.OnActionPerformed();
                            return true;
                        }
                    }
                    //destroyable object in the way
                    else if (target.Components.OfType<DestroyableComponent>().Any())
                    {
                        //destroy object
                        target.Components.OfType<DestroyableComponent>().FirstOrDefault().Destroy();
                        //move
                        Owner.Position += new Vector2(-Owner.Size.X, 0);
                        base.OnActionPerformed();
                        return true;

                    }
                    else if (target.Components.OfType<CollectableComponent>().Any())
                    {
                        //collect object
                        target.Components.OfType<CollectableComponent>().FirstOrDefault().Collect();
                        //move
                        Owner.Position += new Vector2(-Owner.Size.X, 0);
                        base.OnActionPerformed();
                        return true;

                    }
                }
                catch
                {
                    //empty space, move
                    Owner.Position += new Vector2(-Owner.Size.X, 0);
                    base.OnActionPerformed();
                    return true;
                }
            }
            else //object wants to move
            {
                //no other entity in the way
                if (target == null || target.Components.OfType<PlayerComponent>().Any())
                {
                    //move
                    if (target != null)
                    {
                        killPosition = target.Position;
                        tryKill = true;
                    }
                    Owner.Position += new Vector2(-Owner.Size.X, 0);
                    base.OnActionPerformed();
                    return true;
                }
            }
            return false;
        }

        public bool MoveUp()
        {
            Actor target = Owner.Neighbours.Where(x => x.Position.X == Owner.Position.X && x.Position.Y == Owner.Position.Y-Owner.Size.Y).FirstOrDefault();
            //player wants to move
            if (Owner.Components.OfType<PlayerComponent>().Any())
            {
                try
                {
                    //border in the way, cant move
                    if (target.Components.OfType<BorderComponent>().Any())
                        return false;
                    //destroyable object in the way
                    else if (target.Components.OfType<DestroyableComponent>().Any())
                    {
                        //destroy object
                        target.Components.OfType<DestroyableComponent>().FirstOrDefault().Destroy();
                        //move
                        Owner.Position += new Vector2(0, -Owner.Size.Y);
                        base.OnActionPerformed();
                        return true;
                    }
                    else if (target.Components.OfType<CollectableComponent>().Any())
                    {
                        //collect object
                        target.Components.OfType<CollectableComponent>().FirstOrDefault().Collect();
                        //move
                        Owner.Position += new Vector2(0, -Owner.Size.Y);
                        base.OnActionPerformed();
                        return true;
                    }
                }
                catch
                {
                    //empty space, move
                    Owner.Position += new Vector2(0, -Owner.Size.Y);
                    base.OnActionPerformed();
                    return true;
                }
            }
            return false;
        }

        public bool MoveDown()
        {
            Actor target = Owner.Neighbours.Where(x => x.Position.X == Owner.Position.X && x.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
            //player wants to move
            if (Owner.Components.OfType<PlayerComponent>().Any())
            {
                try
                {
                    //border in the way, cant move
                    if (target.Components.OfType<BorderComponent>().Any())
                        return false;
                    //destroyable object in the way
                    else if (target.Components.OfType<DestroyableComponent>().Any())
                    {
                        //destroy object
                        target.Components.OfType<DestroyableComponent>().FirstOrDefault().Destroy();
                        //move
                        Owner.Position += new Vector2(0, Owner.Size.Y);
                        base.OnActionPerformed();
                        return true;

                    }
                    else if (target.Components.OfType<CollectableComponent>().Any())
                    {
                        //collect object
                        target.Components.OfType<CollectableComponent>().FirstOrDefault().Collect();
                        //move
                        Owner.Position += new Vector2(0, Owner.Size.Y);
                        base.OnActionPerformed();
                        return true;

                    }
                }
                catch
                {
                    //empty space, move
                    Owner.Position += new Vector2(0, Owner.Size.Y);
                    base.OnActionPerformed();
                    return true;
                }
            }
            else if (Owner.Components.OfType<GravityComponent>().Any())
            {
                //no other entity in the way
                if (target == null)
                {
                    //move
                    Owner.Position += new Vector2(0, Owner.Size.Y);
                    base.OnActionPerformed();
                    return true;
                }
                else if(target.Components.OfType<PlayerComponent>().Any())
                {
                    killPosition = target.Position;
                    tryKill = true;
                    Owner.Position += new Vector2(0, Owner.Size.Y);
                    base.OnActionPerformed();
                    return true;
                }
                
            }
            return false;
        }
    }
}