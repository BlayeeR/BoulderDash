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
using BoulderDash.Scenes;

namespace BoulderDash
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static int ScreenWidth, ScreenHeight;
        public static bool Stop = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight | DisplayOrientation.Portrait | DisplayOrientation.PortraitDown;
        }

        protected override void Initialize()
        {
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SceneManager.Instance.LoadContent(Content);
            SceneManager.Instance.AddScene(new TitleScene(this));
        }

        protected override void UnloadContent()
        {
            SceneManager.Instance.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Stop)
                Exit();
            InputManager.Instance.Update(gameTime);
            SceneManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SceneManager.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }

        
    }
}
