using Android.Provider;
using Gamedata.ActorComponents;
using GameData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        List<Actor> actors = new List<Actor>();

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
            InputManager.Instance.FlickDown += PlayerMoveDown;
            InputManager.Instance.FlickUp += PlayerMoveUp;
            InputManager.Instance.FlickLeft += PlayerMoveLeft;
            InputManager.Instance.FlickRight += PlayerMoveRight;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = new Texture2D(graphics.GraphicsDevice, 1, 1);
            test.SetData<Color>(new Color[] { Color.Black });
            player = new Rectangle(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2-10, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2-10, 20, 20);
            actors.Add(Content.Load<Actor>("Actors/player"));
            //Actor2D actor = new Actor2D();
            //ActorComponent component = new PhysicsComponent();
            //component.Owner = actor;
            //actor.AddComponent(component);
            //actor.AddComponent(new TestComponent());
            ////actorFactory.SerializeActor2D(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),"actor.xml"),actor);
            //string teststr = actorFactory.SerializeActor2DToString( actor);
            //Actor2D actor2 = actorFactory.CreateActor2DFromString(teststr);
            actors.ForEach(x => x.Initialize(this));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Instance.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            actors.ForEach(x => x.Update(gameTime));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            actors.ForEach(x => x.Draw(spriteBatch));
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void PlayerMoveRight(object sender, EventArgs e)
        {
            actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).ToList().ForEach(x => x.Components.OfType<PlayerComponent>().FirstOrDefault().MoveRight());
        }

        private void PlayerMoveLeft(object sender, EventArgs e)
        {
            actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).ToList().ForEach(x => x.Components.OfType<PlayerComponent>().FirstOrDefault().MoveLeft());
        }

        private void PlayerMoveUp(object sender, EventArgs e)
        {
            actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).ToList().ForEach(x => x.Components.OfType<PlayerComponent>().FirstOrDefault().MoveUp());
        }

        private void PlayerMoveDown(object sender, EventArgs e)
        {
            actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).ToList().ForEach(x => x.Components.OfType<PlayerComponent>().FirstOrDefault().MoveDown());
        }
    }
}
