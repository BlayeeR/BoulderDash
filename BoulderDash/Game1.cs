using Android.Provider;
using BoulderDash.ActorComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.IO;

namespace BoulderDash
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D test;
        Rectangle player;
        List<Actor2D> actors;
        ActorManager actorFactory;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        protected override void Initialize()
        {
            TouchPanel.EnabledGestures = GestureType.HorizontalDrag | GestureType.VerticalDrag;

            actorFactory = new ActorManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = new Texture2D(graphics.GraphicsDevice, 1, 1);
            test.SetData<Color>(new Color[] { Color.Black });
            player = new Rectangle(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2-10, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2-10, 20, 20);
            Actor2D actor = new Actor2D();
            ActorComponent component = new PhysicsComponent();
            component.Owner = actor;
            actor.AddComponent(component);
            actor.AddComponent(new TestComponent());
            //actorFactory.SerializeActor2D(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),"actor.xml"),actor);
            string teststr = actorFactory.SerializeActor2DToString( actor);
            Actor2D actor2 = actorFactory.CreateActor2DFromString(teststr);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            var gesture = default(GestureSample);

            while (TouchPanel.IsGestureAvailable)
            {
                gesture = TouchPanel.ReadGesture();

                if (gesture.GestureType == GestureType.VerticalDrag)
                {
                    if (gesture.Delta.Y < 0)
                        player.Offset(0, -1);
                    if (gesture.Delta.Y > 0)
                        player.Offset(0, 1);
                }

                if (gesture.GestureType == GestureType.HorizontalDrag)
                {
                    if (gesture.Delta.X < 0)
                        player.Offset(-1, 0);
                    if (gesture.Delta.X > 0)
                        player.Offset(1, 0);
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(test, player, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
