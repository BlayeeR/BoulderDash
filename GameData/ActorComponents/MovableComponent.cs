﻿using System;
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
        [ContentSerializerIgnore]
        public bool LockMovement = false;
        private double timer = 0;
        protected int checkSurroundings = 0;
 
        public override void Update(GameTime gameTime)
        {
            timer = (timer >= 250) ? 0 : timer + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer == 0)
            { 
                if(checkSurroundings != 0)
                {
                    checkSurroundings--;
                    Owner.Neighbours.Where(x => x.HasComponent<GravityComponent>()).ToList().ForEach(x => x.GetComponent<GravityComponent>().TryMove());
                }
            }
            base.Update(gameTime);
        }

        public override void Initialize(ContentManager content, Actor owner)
        {
            base.Initialize(content, owner);
            base.ActionPerformed += MovableComponent_ActionPerformed;
        }

        private void MovableComponent_ActionPerformed(object sender, EventArgs e)
        {
            checkSurroundings++;
        }

        public bool MoveRight()
        {
            if (LockMovement)
                return false;
            Actor target = Owner.CloseNeighbours.Where(x => x.Position.X == Owner.Position.X + Owner.Size.X && x.Position.Y == Owner.Position.Y).FirstOrDefault();
            //player wants to move
            if (Owner.IsPlayer)
            {
                try
                {
                    //border in the way, cant move
                    if (target.HasComponent<BorderComponent>())
                        return false;
                    //movable object in the way
                    try
                    {
                        //collect object
                        target.GetComponent<CollectableComponent>().Collect();
                        //move
                        Owner.Position += new Vector2(Owner.Size.X, 0);
                        base.OnActionPerformed();
                        return true;
                    }
                    catch
                    {
                        //destroyable object in the way
                        try
                        {
                            //try to move
                            if (target.GetComponent<MovableComponent>().MoveRight())
                            {
                                //object moved, move player
                                Owner.Position += new Vector2(Owner.Size.X, 0);
                                base.OnActionPerformed();
                                return true;
                            }
                        }
                        catch
                        {
                            try
                            {
                                //destroy object
                                target.GetComponent<DestroyableComponent>().Destroy();
                                //move
                                Owner.Position += new Vector2(Owner.Size.X, 0);
                                base.OnActionPerformed();
                                return true;
                            }
                            catch
                            {
                                try
                                {
                                    if (target.GetComponent<ExitComponent>().IsOpen)
                                        target.GetComponent<ExitComponent>().OnEntered(Owner);
                                    Owner.Position += new Vector2(Owner.Size.X, 0);
                                    base.OnActionPerformed();
                                    return true;
                                }
                                catch(Exception e)
                                {
                                    throw (e);
                                }
                            }
                        }

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
            else if (Owner.HasComponent<GravityComponent>())
            { //object wants to move
                //no other entity in the way
                if(target == null)
                {
                    Owner.Position += new Vector2(Owner.Size.X, 0);
                    base.OnActionPerformed();
                    return true;
                }
            }
            return false;
        }

        public bool MoveLeft()
        {
            if (LockMovement)
                return false;
            Actor target = Owner.CloseNeighbours.Where(x => x.Position.X == Owner.Position.X - Owner.Size.X && x.Position.Y == Owner.Position.Y).FirstOrDefault();
            //player wants to move
            if (Owner.IsPlayer)
            {
                try
                {
                    //border in the way, cant move
                    if (target.HasComponent<BorderComponent>())
                        return false;
                    //movable object in the way
                    try
                    {
                        //collect object
                        target.GetComponent<CollectableComponent>().Collect();
                        //move
                        Owner.Position += new Vector2(-Owner.Size.X, 0);
                        base.OnActionPerformed();
                        return true;
                    }
                    catch
                    {
                        //destroyable object in the way
                        try
                        {
                            //try to move
                            if (target.GetComponent<MovableComponent>().MoveLeft())
                            {
                                //object moved, move player
                                Owner.Position += new Vector2(-Owner.Size.X, 0);
                                base.OnActionPerformed();
                                return true;
                            }
                        }
                        catch
                        {
                            try
                            {
                                //destroy object
                                target.GetComponent<DestroyableComponent>().Destroy();
                                //move
                                Owner.Position += new Vector2(-Owner.Size.X, 0);
                                base.OnActionPerformed();
                                return true;
                            }
                            catch
                            {
                                try
                                {
                                    if (target.GetComponent<ExitComponent>().IsOpen)
                                        target.GetComponent<ExitComponent>().OnEntered(Owner);
                                    Owner.Position += new Vector2(-Owner.Size.X, 0);
                                    base.OnActionPerformed();
                                    return true;
                                }
                                catch(Exception e)
                                {
                                    throw (e);
                                }
                            }
                        }
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
            else if (Owner.HasComponent<GravityComponent>())
            { //object wants to move
                //no other entity in the way
                if (target == null)
                {
                    Owner.Position += new Vector2(-Owner.Size.X, 0);
                    base.OnActionPerformed();
                    return true;
                }
            }
            return false;
        }

        public bool MoveUp()
        {
            if (LockMovement)
                return false;
            Actor target = Owner.CloseNeighbours.Where(x => x.Position.X == Owner.Position.X && x.Position.Y == Owner.Position.Y-Owner.Size.Y).FirstOrDefault();
            //player wants to move
            if (Owner.IsPlayer)
            {
                try
                {
                    //border in the way, cant move
                    if (target.HasComponent<BorderComponent>())
                        return false;
                    //destroyable object in the way
                    try
                    {
                        //destroy object
                        target.GetComponent<DestroyableComponent>().Destroy();
                        //move
                        Owner.Position += new Vector2(0, -Owner.Size.Y);
                        base.OnActionPerformed();
                        return true;
                    }
                    catch
                    {
                        try
                        {
                            //collect object
                            target.GetComponent<CollectableComponent>().Collect();
                            //move
                            Owner.Position += new Vector2(0, -Owner.Size.Y);
                            base.OnActionPerformed();
                            return true;
                        }
                        catch
                        {
                            try
                            {
                                if (target.GetComponent<ExitComponent>().IsOpen)
                                    target.GetComponent<ExitComponent>().OnEntered(Owner);
                                Owner.Position += new Vector2(0, -Owner.Size.Y);
                                base.OnActionPerformed();
                                return true;
                            }
                            catch(Exception e)
                            {
                                throw (e);
                            }
                        }
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
            if (LockMovement)
                return false;
            Actor target = Owner.CloseNeighbours.Where(x => x.Position.X == Owner.Position.X && x.Position.Y == Owner.Position.Y + Owner.Size.Y).FirstOrDefault();
            //player wants to move
            if (Owner.IsPlayer)
            {
                try
                {
                    //border in the way, cant move
                    if (target.HasComponent<BorderComponent>())
                        return false;
                    //destroyable object in the way
                    try
                    {
                        //destroy object
                        target.GetComponent<DestroyableComponent>().Destroy();
                        //move
                        Owner.Position += new Vector2(0, Owner.Size.Y);
                        base.OnActionPerformed();
                        return true;

                    }
                    catch
                    {
                        try
                        {
                            //collect object
                            target.GetComponent<CollectableComponent>().Collect();
                            //move
                            Owner.Position += new Vector2(0, Owner.Size.Y);
                            base.OnActionPerformed();
                            return true;
                        }
                        catch
                        {
                            try
                            {
                                if (target.GetComponent<ExitComponent>().IsOpen)
                                    target.GetComponent<ExitComponent>().OnEntered(Owner);
                                Owner.Position += new Vector2(0, Owner.Size.Y);
                                base.OnActionPerformed();
                                return true;
                            }
                            catch(Exception e)
                            {
                                throw (e);
                            }
                        }
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
            else if (Owner.HasComponent<GravityComponent>())
            {
                //no other entity in the way
                if (target == null)
                {
                    //move
                    Owner.Position += new Vector2(0, Owner.Size.Y);
                    base.OnActionPerformed();
                    return true;
                }
                else if(Owner.GetComponent<GravityComponent>().IsFalling && target.IsPlayer)
                {
                    target.GetComponent<PlayerComponent>().Kill();
                    Owner.Position += new Vector2(0, Owner.Size.Y);
                    base.OnActionPerformed();
                    return true;
                }
            }
            return false;
        }
    }
}