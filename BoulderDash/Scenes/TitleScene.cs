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
using GameData.Sprites;
using GameShared;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BoulderDash.Scenes
{
    public class TitleScene : IComponent
    {
        public List<Sprite> Sprites = new List<Sprite>();
        public List<Actor> Actors = new List<Actor>();
        private Camera camera;
        private Game1 game;

        public TitleScene(Game1 game)
        {
            this.game = game;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.Transform);
            Sprites.ForEach(x => x.Draw(spriteBatch));
            Actors.ForEach(x => x.Draw(spriteBatch));
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            camera.Follow(Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault());
            Sprites.ForEach(x => x.Update(gameTime));
            Actors.ForEach(x => x.Update(gameTime));
        }

        public void LoadContent(ContentManager content)
        {
            InputManager.Instance.OnBackButtonClicked += InputManager_OnBackButtonClicked;
            camera = new Camera();
            game.Window.OrientationChanged += Window_OrientationChanged;
            Sprites.Add(new Text()
            {
                ResourcePath = "Fonts/DefaultFont",
                Color = Color.White,
                Position = new Vector2(200, 200),
                Size = new Vector2(0.5f),
                String = "Boulder Dash"


            });
            Sprites.ForEach(x => x.LoadContent(content));
            Actors.Add(content.Load<Actor>("Actors/Player"));
            Actors.ForEach(x => x.LoadContent(content));
            Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault().Components.OfType<PlayerComponent>().FirstOrDefault().OnPlayerMoved += TitleScene_OnPlayerMoved;
        }

        private void Window_OrientationChanged(object sender, EventArgs e)
        {
            camera.ChangeOrientation(game.Window.CurrentOrientation);
        }

        private void TitleScene_OnPlayerMoved(object sender, EventArgs e)
        {
            
        }

        private void InputManager_OnBackButtonClicked(object sender, EventArgs e)
        {
            Game1.Stop = true;
        }

        public void UnloadContent()
        {
        }
    }
}