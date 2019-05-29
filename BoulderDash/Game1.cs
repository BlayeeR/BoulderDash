using Android.Provider;
using GameData.ActorComponents;
using GameData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using GameShared;
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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<Actor> actors = new List<Actor>();
        private Scene scene;
        private Menu menu;

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            actors.Add(Content.Load<Actor>("Actors/Player"));
            actors.ForEach(x => x.Initialize(this));
            scene = Content.Load<Scene>("Scenes/TitleScene");
            scene.LoadContent(Content);
            menu = Content.Load<Menu>("Menus/TitleMenu");
            menu.LoadContent(Content);
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
            scene.Draw(spriteBatch);
            menu.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        
    }
}
