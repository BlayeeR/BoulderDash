﻿using Microsoft.Xna.Framework;
using GameShared.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BoulderDash
{ 
    public interface ICamera2D
    {
        Vector2 Position { get; set; }
        float MoveSpeed { get; set; }
        float Rotation { get; set; }
        Vector2 Origin { get; }
        float Scale { get; set; }
        Vector2 ScreenCenter { get; }
        Matrix Transform { get; }
        IFocusable Focus { get; set; }
        bool IsInView(Vector2 position, Texture2D texture);
    }

    public class Camera2D : GameComponent, ICamera2D
    {
        #region Fields
        private Vector2 position;
        protected float viewportHeight;
        protected float viewportWidth;
        private IFocusable focus;
        private Vector2 deadZone;
        private Vector2 focusSize;
        private Vector2 screenCenter;
        private float scale;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get => scale; set
            {
                scale = value;
            } }
        public Vector2 ScreenCenter
        {
            get => screenCenter; protected set
            {
                screenCenter = value;
                //if (Game.Window.CurrentOrientation == DisplayOrientation.LandscapeLeft || Game.Window.CurrentOrientation == DisplayOrientation.LandscapeRight)
                    screenCenter = new Vector2(screenCenter.X > screenCenter.Y ? screenCenter.X : screenCenter.Y, screenCenter.X > screenCenter.Y ? screenCenter.Y : screenCenter.X);
                //else
                //    screenCenter = new Vector2(screenCenter.X > screenCenter.Y ? screenCenter.Y : screenCenter.X, screenCenter.X > screenCenter.Y ? screenCenter.X : screenCenter.Y);
            }
        }
        public Matrix Transform { get; set; }
        public Vector2 MapSize { get; set; }
        public Vector2 DeadZone
        {
            get => new Vector2((float)Math.Round(deadZone.X / focusSize.X / Scale) * focusSize.X, (float)Math.Round(deadZone.Y / focusSize.Y / Scale) * focusSize.Y); set
            {
                deadZone = value;
                //if (Game.Window.CurrentOrientation == DisplayOrientation.LandscapeLeft || Game.Window.CurrentOrientation == DisplayOrientation.LandscapeRight)
                    deadZone = new Vector2(deadZone.X > deadZone.Y ? deadZone.X : deadZone.Y, deadZone.X > deadZone.Y ? deadZone.Y : deadZone.X);
                //else
                //    deadZone = new Vector2(deadZone.X > deadZone.Y ? deadZone.Y : deadZone.X, deadZone.X > deadZone.Y ? deadZone.X : deadZone.Y);
            }
        }
        public IFocusable Focus
        {
            get { return focus; }
            set
            {
                focus = value;
                UpdatePosition();
            }
        }
        public float MoveSpeed { get; set; }
        #endregion

        public Camera2D(Game1 game, Vector2 dimensions)
            : base(game)
        {
            viewportWidth = dimensions.X;
            viewportHeight = dimensions.Y;
        }

        private void UpdatePosition(GameTime gameTime = null)
        {
            float delta = 1;
            if (gameTime !=null)
                 delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (focus.Position.X < DeadZone.X)//left side of map(left deadzone)
                position.X = DeadZone.X + focusSize.X;
            else if (focus.Position.X > MapSize.X - DeadZone.X - focusSize.X)//right side of map(right deadzone)
                position.X = MapSize.X - DeadZone.X - focusSize.X;
            else if (focus.Position.X == MapSize.X - DeadZone.X - focusSize.X)//edge of right deadzone
                position.X += (Focus.Position.X - Position.X) * MoveSpeed * delta;
            else if (focus.Position.X == deadZone.X)//edge of left deadzone
                position.X += ((Focus.Position.X + focusSize.X) - Position.X) * MoveSpeed * delta;
            else//middle of map
                position.X += ((Focus.Position.X + (focusSize.X / 2)) - Position.X) * MoveSpeed * delta;


            if (focus.Position.Y < DeadZone.Y )//top side of map(top deadzone)
                position.Y = DeadZone.Y + focusSize.Y;
            else if (focus.Position.Y > MapSize.Y - DeadZone.Y - focusSize.Y)//bottom side of map(bottom deadzone
                position.Y = MapSize.Y - DeadZone.Y - focusSize.Y;
            else if (focus.Position.Y == MapSize.Y - DeadZone.Y - focusSize.Y)//edge of bottom deadzone
                position.Y += (Focus.Position.Y - Position.Y) * MoveSpeed * delta;
            else if (focus.Position.Y == DeadZone.Y)//edge of top deadzone
                position.Y += ((Focus.Position.Y + focusSize.Y) - Position.Y) * MoveSpeed * delta;
            else//middle of map
                position.Y += ((Focus.Position.Y + (focusSize.Y / 2)) - Position.Y) * MoveSpeed * delta;
        }

        public override void Initialize()
        {
            ScreenCenter = new Vector2((float)Math.Round(viewportWidth / 2), (float)Math.Round(viewportHeight / 2));
            //Game.Window.OrientationChanged += Window_OrientationChanged;
            Scale = 1;
            MoveSpeed = 10f;
            DeadZone = Vector2.Zero;
            MapSize = new Vector2(656, 368);
            base.Initialize();
        }

        private void Window_OrientationChanged(object sender, System.EventArgs e)
        {
            DeadZone = deadZone;
            ScreenCenter = screenCenter;
            UpdatePosition();
        }

        public void CalculateDeadZone(Vector2 mapSize, Vector2 tileSize)
        {
            MapSize = mapSize;
            focusSize = tileSize;
            DeadZone = new Vector2(((float)Math.Round(viewportWidth / focusSize.X / 2) - 1) * focusSize.X,
                                   ((float)Math.Round(viewportHeight / focusSize.Y / 2) - 1) * focusSize.Y);
        }

        public override void Update(GameTime gameTime)
        {
            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-(int)Position.X, -(int)Position.Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

            Origin = ScreenCenter / Scale;

            //var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if (Focus.Position.X > DeadZone.X && Focus.Position.X < MapSize.X - DeadZone.X)
            //    position.X += ((Focus.Position.X - (focusSize.X / 2)) - Position.X) * MoveSpeed * delta;
            //if (Focus.Position.Y > DeadZone.Y && Focus.Position.Y < MapSize.Y - DeadZone.Y)
            //    position.Y += ((Focus.Position.Y - (focusSize.Y / 2)) - Position.Y) * MoveSpeed * delta;
            UpdatePosition(gameTime);

            base.Update(gameTime);
        }

        public bool IsInView(Vector2 position, Texture2D texture)
        {
            if ((position.X + texture.Width) < (Position.X - Origin.X) || (position.X) > (Position.X + Origin.X))
                return false;
            if ((position.Y + texture.Height) < (Position.Y - Origin.Y) || (position.Y) > (Position.Y + Origin.Y))
                return false;
            return true;
        }
    }
}