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
using GameData;
using GameData.ActorComponents;
using GameData.Maps;
using GameData.Sprites;
using GameShared;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BoulderDash.Scenes
{
    public class MainScene : IScene
    {
        public Camera2D Camera { get; private set; }
        private readonly Game1 game;
        private ActorMap map;
        private float guiSize = 0.0764f;
        private RenderTarget2D guiWindow;
        private RenderTarget2D mapWindow;
        private ContentManager content;
        private double timer = 0;
        private Text guiText;
        private readonly int firstMap = 1;

        public MainScene(Game1 game, int mapID)
        {
            this.game = game;
            this.firstMap = mapID;
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
            game.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(guiWindow, new Rectangle(0, 0, (int)game.GetScaledResolution().X, (int)(game.GetScaledResolution().Y*guiSize)), Color.White);
            spriteBatch.Draw(mapWindow, new Rectangle(0, (int)(game.GetScaledResolution().Y * guiSize), (int)game.GetScaledResolution().X, (int)(game.GetScaledResolution().Y*(1-guiSize))), Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            guiText.Update(gameTime);
            map.Update(gameTime);
            Camera.Update(gameTime);
            timer = (timer >= 100) ? 0 : timer + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer == 0)
            {
                if(map.DiamondsRequired > map.DiamondsCollected)
                    guiText.String =  $"[{map.DiamondsRequired}]/{map.DiamondValue} [{map.DiamondsCollected}] {map.Time} {map.Score}";
                else
                    guiText.String = $"///{map.BonusDiamondValue} [{map.DiamondsCollected}] {map.Time} {map.Score}";
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            InputManager.Instance.OnBackButtonClicked += InputManager_OnBackButtonClicked;
            using (ContentManager c = new ContentManager(content.ServiceProvider, content.RootDirectory))
                try
                {
                    map = c.Load<ActorMap>($"Maps/{firstMap}");
                }
                catch
                {
                    map = c.Load<ActorMap>($"Maps/1");
                }
            map.LoadContent(content);
            map.PlayerKilled += Map_PlayerKilled;
            map.MapCompleted += Map_MapCompleted;
            Camera = new Camera2D(game, new Vector2(game.GetScaledResolution().X, game.GetScaledResolution().Y * (1 - guiSize)));
            Camera.Initialize();
            Camera.CalculateDeadZone(map.Size, map.TileDimensions);
            Camera.Focus = map.Player;
            guiText = new Text(new Vector2(game.GetScaledResolution().X*0.06f, game.GetScaledResolution().Y*guiSize /4), $"[{map.DiamondsRequired}]/{map.DiamondValue} [{map.DiamondsCollected}] {map.Time} {map.Score}", Color.White);
            guiText.LoadContent(content);
        }

        private void Map_MapCompleted(object sender, EventArgs e)
        {
            int id = map.ID, oldScore = map.Score;
            id++;
            Camera.Focus = new Actor() { Position = Vector2.Zero };
            map.UnloadContent();
            map.MapCompleted -= Map_MapCompleted;
            map.PlayerKilled -= Map_PlayerKilled;
            try
            {
                using (ContentManager c = new ContentManager(content.ServiceProvider, content.RootDirectory))
                    map = c.Load<ActorMap>($"Maps/{id}");
            }
            catch
            {
                if(SceneManager.Instance.CurrentScene == this)
                    SceneManager.Instance.ChangeScene(new EndingScene(game, oldScore));
            }
            map.LoadContent(content);
            map.Score = oldScore; 
            Camera.Focus = map.Player;
            map.MapCompleted += Map_MapCompleted;
            map.PlayerKilled += Map_PlayerKilled;
        }

        private void Map_PlayerKilled(object sender, EventArgs e)
        {
            if (SceneManager.Instance.CurrentScene == this)
                SceneManager.Instance.ChangeScene(new DeathScene(game, map.Score));
        }

        private void InputManager_OnBackButtonClicked(object sender, EventArgs e)
        {
            if(SceneManager.Instance.CurrentScene == this)
                SceneManager.Instance.AddScene(new PauseScene(game));
        }

        public void UnloadContent()
        {
            guiText.UnloadContent();
            map.UnloadContent();
        }

        public void UpdateOrientation()
        {
            if (game.Window.CurrentOrientation == DisplayOrientation.LandscapeLeft || game.Window.CurrentOrientation == DisplayOrientation.LandscapeRight)
                guiSize = 0.0764f;
            else
                guiSize = 0.0912f;
            //TODO: Add multiline text
            guiWindow = new RenderTarget2D(game.GraphicsDevice, (int)game.GetScaledResolution().X, (int)(game.GetScaledResolution().Y * guiSize));
            mapWindow = new RenderTarget2D(game.GraphicsDevice, (int)game.GetScaledResolution().X, (int)(game.GetScaledResolution().Y * (1 - guiSize)));
            Camera.Viewport = new Vector2(mapWindow.Width, mapWindow.Height);
        }
    }
}