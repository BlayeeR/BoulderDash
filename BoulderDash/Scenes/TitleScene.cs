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
    public class TitleScene : IScene
    {
        public Camera2D Camera { get; private set; }
        private readonly Game1 game;
        private ActorMap map;

        public TitleScene(Game1 game)
        {
            this.game = game;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp,transformMatrix: SceneManager.Instance.CurrentScene.Camera.Transform);
            map.Draw(spriteBatch);
            spriteBatch.End();
            
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            map.Update(gameTime);
        }

        public void LoadContent(ContentManager content)
        {
            InputManager.Instance.OnBackButtonClicked += InputManager_OnBackButtonClicked;
            map = content.Load<ActorMap>("Maps/Cave1");
            map.LoadContent(content);
            Camera = new Camera2D(game);
            Camera.Initialize();
            Camera.CalculateDeadZone(map.Size, map.Tiles.TileDimensions);
            Camera.Focus = map.Tiles.Actors.Where(x => x.Components.OfType<PlayerComponent>().Any()).FirstOrDefault();
        }

        private void InputManager_OnBackButtonClicked(object sender, EventArgs e)
        {
            Game1.Stop = true;
        }

        public void UnloadContent()
        {
            map.UnloadContent();
        }
    }
}