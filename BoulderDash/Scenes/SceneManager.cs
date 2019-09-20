using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using GameData;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BoulderDash.Scenes
{
    public class SceneManager : IComponent
    {
        private static SceneManager instance;
        private ContentManager content;
        private Stack<IScene> scenes = new Stack<IScene>();
        public IScene CurrentScene { get { return scenes.Peek(); } }

        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                }
                return instance;
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
        }

        public void AddScene(IScene scene)
        {
            scenes.Push(scene);
            if (content != null)
            {
                scenes.Peek().LoadContent(content);
            }
        }


        public void RemoveScene()
        {
            if (scenes.Count > 0)
            {
                var screen = scenes.Peek();
                scenes.Pop();
            }
        }

        public void UpdateSceneOrientation()
        {
            if (scenes.Count > 0)
            {
                scenes.Peek().UpdateOrientation();
            }
        }

        public void ClearScenes()
        {
            while (scenes.Count > 0)
            {
                scenes.Pop();
            }
        }

        public void ChangeScene(IScene scene)
        {
            ClearScenes();
            AddScene(scene);
        }

        public void Update(GameTime gameTime)
        {
            if (scenes.Count > 0)
            {
                scenes.Peek().Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (scenes.Count > 0)
            {
                scenes.Peek().Draw(spriteBatch);
            }
        }

        public void UnloadContent()
        {
            foreach (IScene scene in scenes)
            {
                scene.UnloadContent();
            }
        }
    }
}