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
    public class PauseScene : IScene
    {
        public Text resumeText, mainMenuText;
        private Game1 game;
        public PauseScene(Game1 game)
        {
            this.game = game;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            resumeText.Draw(spriteBatch);
            mainMenuText.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void LoadContent(ContentManager content)
        {
            resumeText = new Text(Vector2.Zero, "Resume", Color.White);
            resumeText.LoadContent(content);
            resumeText.Position = new Vector2(game.GetScaledResolution().X / 2 - resumeText.Size.X / 2, game.GetScaledResolution().Y * 0.3f);
            resumeText.Clicked += ResumeText_Clicked;
            mainMenuText = new Text(Vector2.Zero, "Main menu", Color.White);
            mainMenuText.LoadContent(content);
            mainMenuText.Position = new Vector2(game.GetScaledResolution().X / 2 - mainMenuText.Size.X / 2, game.GetScaledResolution().Y * 0.6f);
            mainMenuText.Clicked += MainMenuText_Clicked;
            InputManager.Instance.OnBackButtonClicked += Instance_OnBackButtonClicked;
        }

        private void Instance_OnBackButtonClicked(object sender, EventArgs e)
        {
            if (SceneManager.Instance.CurrentScene == this)
                SceneManager.Instance.RemoveScene();
        }

        private void MainMenuText_Clicked(object sender, EventArgs e)
        {
            if (SceneManager.Instance.CurrentScene == this)
                SceneManager.Instance.ChangeScene(new TitleScene(game));
        }

        private void ResumeText_Clicked(object sender, EventArgs e)
        {
            if (SceneManager.Instance.CurrentScene == this)
                SceneManager.Instance.RemoveScene();
        }

        public void UnloadContent()
        {
            resumeText.UnloadContent();
            mainMenuText.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            resumeText.Update(gameTime);
            mainMenuText.Update(gameTime);
        }

        public void UpdateOrientation()
        {
            resumeText.Position = new Vector2(game.GetScaledResolution().X / 2 - resumeText.Size.X / 2, game.GetScaledResolution().Y * 0.3f);
            mainMenuText.Position = new Vector2(game.GetScaledResolution().X / 2 - mainMenuText.Size.X / 2, game.GetScaledResolution().Y * 0.6f);
        }
    }
}