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
        public static bool Stop = false;
        private RenderTarget2D renderTarget;
        private float renderScale = 1.5f;
        private const int renderScreenHeight = 480;

        public float AspectRatio
        {
            get
            {
                return (float)GraphicsDevice.PresentationParameters.BackBufferWidth / GraphicsDevice.PresentationParameters.BackBufferHeight;
            }
        }

        public Vector2 GetScaledResolution()
        {
            var scaledHeight = (float)renderScreenHeight / renderScale;
            if(AspectRatio<1f)
                return new Vector2(AspectRatio * scaledHeight, scaledHeight);
            else
                return new Vector2(scaledHeight, scaledHeight/AspectRatio);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight | DisplayOrientation.Portrait | DisplayOrientation.PortraitDown;
        }

        protected override void Initialize()
        {
            renderTarget = new RenderTarget2D(GraphicsDevice, (int)GetScaledResolution().X, (int)GetScaledResolution().Y);
            Window.OrientationChanged += Window_OrientationChanged;
            base.Initialize();
        }

        private void Window_OrientationChanged(object sender, EventArgs e)
        {
            renderTarget = new RenderTarget2D(GraphicsDevice, (int)GetScaledResolution().Y, (int)GetScaledResolution().X);
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
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Black);
            SceneManager.Instance.Draw(spriteBatch);
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.Default,
                RasterizerState.CullNone);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        
    }
}
