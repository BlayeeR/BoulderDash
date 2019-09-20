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
using GameData.Sprites;
using GameShared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BoulderDash.Scenes
{
    public class EndingScene : IScene
    {
        public Text congratulationText, scoreText, mainMenuText;
        private Game1 game;
        private readonly int score;
        public EndingScene(Game1 game, int score)
        {
            this.game = game;
            this.score = score;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            scoreText.Draw(spriteBatch);
            mainMenuText.Draw(spriteBatch);
            congratulationText.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void LoadContent(ContentManager content)
        {
            congratulationText = new Text(Vector2.Zero, "Congratulations! You won!", Color.White);
            congratulationText.LoadContent(content);
            congratulationText.Position = new Vector2(game.GetScaledResolution().X / 2 - congratulationText.Size.X / 2, game.GetScaledResolution().Y * 0.25f);
            scoreText = new Text(Vector2.Zero, $"Your score: {score}", Color.White);
            scoreText.LoadContent(content);
            scoreText.Position = new Vector2(game.GetScaledResolution().X / 2 - scoreText.Size.X / 2, game.GetScaledResolution().Y * 0.50f);
            mainMenuText = new Text(Vector2.Zero, "Main menu", Color.White);
            mainMenuText.LoadContent(content);
            mainMenuText.Position = new Vector2(game.GetScaledResolution().X / 2 - mainMenuText.Size.X / 2, game.GetScaledResolution().Y * 0.75f);
            mainMenuText.Clicked += MainMenuText_Clicked;
            InputManager.Instance.OnBackButtonClicked += Instance_OnBackButtonClicked;
        }

        private void Instance_OnBackButtonClicked(object sender, EventArgs e)
        {
            if (SceneManager.Instance.CurrentScene == this)
                SceneManager.Instance.ChangeScene(new TitleScene(game));
        }

        private void MainMenuText_Clicked(object sender, EventArgs e)
        {
            if (SceneManager.Instance.CurrentScene == this)
                SceneManager.Instance.ChangeScene(new TitleScene(game));
        }

        public void UnloadContent()
        {
            scoreText.UnloadContent();
            mainMenuText.UnloadContent();
            congratulationText.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            scoreText.Update(gameTime);
            mainMenuText.Update(gameTime);
            congratulationText.Update(gameTime);
        }

        public void UpdateOrientation()
        {
            congratulationText.Position = new Vector2(game.GetScaledResolution().X / 2 - congratulationText.Size.X / 2, game.GetScaledResolution().Y * 0.25f);
            scoreText.Position = new Vector2(game.GetScaledResolution().X / 2 - scoreText.Size.X / 2, game.GetScaledResolution().Y * 0.50f);
            mainMenuText.Position = new Vector2(game.GetScaledResolution().X / 2 - mainMenuText.Size.X / 2, game.GetScaledResolution().Y * 0.75f);
        }
    }
}