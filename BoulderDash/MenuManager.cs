using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using BoulderDash.Scenes;
using GameData;
using GameShared.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BoulderDash
{
    public class MenuManager : IComponent
    {
        private static MenuManager instance;
        private ContentManager content;
        private Stack<Menu> menus = new Stack<Menu>();

        public static MenuManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuManager();
                }
                return instance;
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
        }

        public void AddMenu(Menu menu)
        {
            menus.Push(menu);
            if (content != null)
            {
                menus.Peek().LoadContent(content);
                menus.Peek().MenuItemTapped += MenuManager_MenuItemTapped;
            }
        }

        private void MenuManager_MenuItemTapped(object sender, EventArgs e)
        {
            switch((sender as MenuItem).LinkType)
            {
                //case "Scene":
                //    {
                //        ClearMenus();
                //        SceneManager.Instance.ChangeScene(content.Load<Scene>($"Scenes/{(sender as MenuItem).LinkID}"));
                //        break;
                //    }
                case "Menu":
                    {
                        if (menus.Skip(1).First().ID.Equals((sender as MenuItem).LinkID))
                            RemoveMenu();
                        else
                            AddMenu(content.Load<Menu>($"Menus/{(sender as MenuItem).LinkID}"));
                        break;
                    }
            }
        }

        public int CountMenus()
        {
            return menus.Count;
        }

        public void RemoveMenu()
        {
            if (menus.Count > 0)
            {
                menus.Peek().MenuItemTapped -= MenuManager_MenuItemTapped;
                menus.Pop();
            }
        }

        public void ClearMenus()
        {
            while (menus.Count > 0)
            {
                menus.Peek().MenuItemTapped -= MenuManager_MenuItemTapped;
                menus.Pop();
            }
        }

        public void ChangeMenu(Menu menu)
        {
            ClearMenus();
            AddMenu(menu);
        }

        public void Update(GameTime gameTime)
        {
            if (menus.Count > 0)
            {
                menus.Peek().Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (menus.Count > 0)
            {
                menus.Peek().Draw(spriteBatch);
            }
        }

        public void UnloadContent()
        {
            foreach (Menu menu in menus)
            {
                menu.UnloadContent();
            }
        }
    }
}