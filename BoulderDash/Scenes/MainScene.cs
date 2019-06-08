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
using GameData;
using GameData.ActorComponents;
using GameData.Maps;
using GameData.Sprites;
using GameShared;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BoulderDash.Scenes
{
    public class MainScene : IScene
    {
        public Camera2D Camera { get; private set; }
        private readonly Game1 game;
        private ActorMap map;
        private float guiSize = 0.0764f;
        private RenderTarget2D guiWindow, mapWindow;
        private Text guiText;

        public MainScene(Game1 game)
        {
            this.game = game;
            guiWindow = new RenderTarget2D(game.GraphicsDevice, (int)game.GetScaledResolution().X, (int)(game.GetScaledResolution().Y * guiSize));
            mapWindow = new RenderTarget2D(game.GraphicsDevice, (int)game.GetScaledResolution().X, (int)(game.GetScaledResolution().Y*(1-guiSize)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            RenderTargetBinding[] temp = game.GraphicsDevice.GetRenderTargets();

            game.GraphicsDevice.SetRenderTarget(guiWindow);
            game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            guiText.Draw(spriteBatch);
            spriteBatch.End();

            game.GraphicsDevice.SetRenderTarget(mapWindow);
            game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp,transformMatrix: Camera.Transform);
            map.Draw(spriteBatch);
            spriteBatch.End();

            game.GraphicsDevice.SetRenderTargets(temp);
            game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(guiWindow, new Rectangle(0, 0, (int)game.GetScaledResolution().X, (int)(game.GetScaledResolution().Y*guiSize)), Color.White);
            spriteBatch.Draw(mapWindow, new Rectangle(0, (int)(game.GetScaledResolution().Y * guiSize), (int)game.GetScaledResolution().X, (int)(game.GetScaledResolution().Y*(1-guiSize))), Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            map.Update(gameTime);
            guiText.Update(gameTime);
        }

        public void LoadContent(ContentManager content)
        {
            InputManager.Instance.OnBackButtonClicked += InputManager_OnBackButtonClicked;
            map = content.Load<ActorMap>("Maps/Cave1");
            map.LoadContent(content);
            Camera = new Camera2D(game, new Vector2(game.GetScaledResolution().X, game.GetScaledResolution().Y * (1 - guiSize)));
            Camera.Initialize();
            Camera.CalculateDeadZone(map.Size, map.Tiles.TileDimensions);
            Camera.Focus = map.Tiles.Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault();
            guiText = new Text(new Vector2(game.GetScaledResolution().X*0.06f, game.GetScaledResolution().Y*guiSize /4), "[12]/10 [00] 127 000000", Color.White);
            guiText.LoadContent(content);
        }

        private void InputManager_OnBackButtonClicked(object sender, EventArgs e)
        {
            Game1.Stop = true;
        }

        public void UnloadContent()
        {
            guiText.UnloadContent();
            map.UnloadContent();
        }
    }
}