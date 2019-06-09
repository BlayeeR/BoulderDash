using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GameData;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.ActorComponents
{
    public abstract class ActorComponent : IUpdate
    {
        private double timer = 0;
        private bool fireEvent = false;
        [ContentSerializerIgnore]
        public Actor Owner { get; private set; }
        public virtual void Update(GameTime gameTime)
        {
            timer = (timer >= 400) ? 0 : timer + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer == 0)
            {
                if (fireEvent)
                {
                    fireEvent = false;
                    Owner.Neighbours.Where(x => x.Components.OfType<GravityComponent>().Any()).ToList().ForEach(x =>
                    {
                        if (!x.Components.OfType<GravityComponent>().FirstOrDefault().MoveDown())
                        {//if cant
                            //check neighbour in bottom left
                            Actor temp = x.Neighbours.Where(y => y.Position.X == x.Position.X - x.Size.X && y.Position.Y == x.Position.Y + x.Size.Y).FirstOrDefault();
                            //if there is any try to move left
                            if (!(temp == null && x.Components.OfType<GravityComponent>().FirstOrDefault().MoveLeft()))
                            {//cant move left, entity in the way
                             //check right bottom
                                temp = x.Neighbours.Where(y => y.Position.X == x.Position.X + x.Size.X && y.Position.Y == x.Position.Y + x.Size.Y).FirstOrDefault();
                                //empty
                                if (temp == null)
                                    //try move
                                    x.Components.OfType<GravityComponent>().FirstOrDefault().MoveRight();
                            }

                        }
                    });
                }
            }
        }
        public event EventHandler ActionPerformed;
        public virtual void Initialize(ContentManager content, Actor owner)
        {
            this.Owner = owner;
        }

        protected void OnActionPerformed()
        {
            fireEvent = true;
            this.ActionPerformed?.Invoke(Owner, null);
        }
    }
}