using Microsoft.Xna.Framework;
using GameShared.Interfaces;
using Microsoft.Xna.Framework.Graphics;

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
        private Vector2 position;
        protected float viewportHeight;
        protected float viewportWidth;

        public Camera2D(Game1 game)
            : base(game)
        { }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public Vector2 ScreenCenter { get; protected set; }
        public Matrix Transform { get; set; }
        public IFocusable Focus { get; set; }
        public float MoveSpeed { get; set; }

        public override void Initialize()
        {
            viewportWidth = (Game as Game1).GetScaledResolution().X;
            viewportHeight = (Game as Game1).GetScaledResolution().Y;

            ScreenCenter = new Vector2(viewportWidth / 2, viewportHeight / 2);
            Scale = 2;
            MoveSpeed = 10f;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

            Origin = ScreenCenter / Scale;

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position.X += (Focus.Position.X - Position.X) * MoveSpeed * delta;
            position.Y += (Focus.Position.Y - Position.Y) * MoveSpeed * delta;

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