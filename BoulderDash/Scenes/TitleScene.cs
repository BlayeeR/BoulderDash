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
using GameData.Sprites;
using GameShared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BoulderDash.Scenes
{
    public class TitleScene : IScene
    {
        private Image logo;
        private Text levelText, caveText, levelNumber, caveNumber, playText;
        private Song theme;
        private readonly int currentLevel = 0;
        private int currentCave = 0;
        private readonly string[] levels = { "1", "2", "3", "4", "5" };
        private string[] caves = { "Intro", "Rooms", "Maze" };

        //"Guards" };//,     "Firefly dens", "Amoeba",     "Enchanted wall",
        //"Greed",      "Tracks",       "Crowd",      "Walls",\
        //"Apocalypse", "Zigzag",       "Funnel",     "Enchanted boxes",
        //"Interval 1", "Interval 2", "Interval 3", "Interval 4"};
        private Game1 game;

        public TitleScene(Game1 game)
        {
            this.game = game;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            logo.Draw(spriteBatch);
            //levelText.Draw(spriteBatch);
            //levelNumber.Draw(spriteBatch);
            caveText.Draw(spriteBatch);
            caveNumber.Draw(spriteBatch);
            playText.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void LoadContent(ContentManager content)
        {
            InputManager.Instance.OnBackButtonClicked += Instance_OnBackButtonClicked;
            float x = game.GetScaledResolution().X, y = game.GetScaledResolution().Y;
            logo = new Image(new Vector2(x*0.1f,0), new Vector2(x*0.8f, y*0.8f), "Textures/Logo", Color.White, Vector2.Zero);
            logo.LoadContent(content);
            levelText = new Text(Vector2.Zero, "Level:", Color.White);
            levelText.LoadContent(content);
            levelNumber = new Text(Vector2.Zero, levels[currentLevel], Color.Blue);
            levelNumber.LoadContent(content);
            levelNumber.Clicked += LevelNumber_Clicked;
            levelText.Position = new Vector2((x - (levelText.Size.X + levelNumber.Size.X)) / 2, y * 0.72f);
            levelNumber.Position = new Vector2(levelText.Position.X + levelText.Size.X, levelText.Position.Y);

            caveText = new Text(Vector2.Zero, "Cave:", Color.White);
            caveText.LoadContent(content);
            caveNumber = new Text(Vector2.Zero, caves[currentCave], Color.Blue);
            caveNumber.LoadContent(content);
            caveNumber.Clicked += CaveNumber_Clicked;
            caveText.Position = new Vector2((x - (caveText.Size.X + caveNumber.Size.X)) / 2, y * 0.82f);
            caveNumber.Position = new Vector2(caveText.Position.X + caveText.Size.X, caveText.Position.Y);

            playText = new Text(Vector2.Zero, "Start", Color.Blue);
            playText.LoadContent(content);
            playText.Position = new Vector2((x - playText.Size.X) / 2, y * 0.92f);
            playText.Clicked += PlayText_Clicked;

            theme = content.Load<Song>("Sounds/Theme");
            MediaPlayer.Play(theme);
            MediaPlayer.IsRepeating = true;
        }

        private void Instance_OnBackButtonClicked(object sender, EventArgs e)
        {
            if(SceneManager.Instance.CurrentScene == this)
                Game1.Stop = true;
        }

        private void PlayText_Clicked(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
            MediaPlayer.IsRepeating = false;
            if (SceneManager.Instance.CurrentScene == this)
                SceneManager.Instance.ChangeScene(new MainScene(game, currentCave+1));
        }

        private void CaveNumber_Clicked(object sender, EventArgs e)
        {
            caveNumber.String = caves[++currentCave >= caves.Length ? currentCave = 0 : currentCave];
            caveText.Position = new Vector2((game.GetScaledResolution().X - (caveText.Size.X + caveNumber.Size.X)) / 2, game.GetScaledResolution().Y * 0.82f);
            caveNumber.Position = new Vector2(caveText.Position.X + caveText.Size.X, caveText.Position.Y);
        }

        private void LevelNumber_Clicked(object sender, EventArgs e)
        {
            //levelNumber.String = levels[++currentLevel >= levels.Length ? currentLevel = 0 : currentLevel];
            //levelText.Position = new Vector2((game.GetScaledResolution().X - (levelText.Size.X + levelNumber.Size.X)) / 2, game.GetScaledResolution().Y * 0.72f);
            //levelNumber.Position = new Vector2(levelText.Position.X + levelText.Size.X, levelText.Position.Y);
        }

        public void UnloadContent()
        {
            logo.UnloadContent();
            levelText.UnloadContent();
            levelNumber.UnloadContent();
            caveText.UnloadContent();
            caveNumber.UnloadContent();
            playText.UnloadContent();
            caveNumber.Clicked -= CaveNumber_Clicked;
            levelNumber.Clicked -= LevelNumber_Clicked;
            MediaPlayer.Stop();
        }

        public void Update(GameTime gameTime)
        {
            logo.Update(gameTime);
            levelText.Update(gameTime);
            levelNumber.Update(gameTime);
            caveText.Update(gameTime);
            caveNumber.Update(gameTime);
            playText.Update(gameTime);
        }

        public void UpdateOrientation()
        {
            float x = (int)game.GetScaledResolution().X, y = (int)game.GetScaledResolution().Y;
            levelText.Position = new Vector2((x - (levelText.Size.X + levelNumber.Size.X)) / 2, y * 0.72f);
            levelNumber.Position = new Vector2(levelText.Position.X + levelText.Size.X, levelText.Position.Y);
            caveText.Position = new Vector2((x - (caveText.Size.X + caveNumber.Size.X)) / 2, y * 0.82f);
            caveNumber.Position = new Vector2(caveText.Position.X + caveText.Size.X, caveText.Position.Y);
            playText.Position = new Vector2((x - playText.Size.X) / 2, y * 0.92f);
            logo.Position = new Vector2(x * 0.1f, 0);
            logo.Size = new Vector2(x * 0.8f, y * 0.8f);
        }
    }
}