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
using GameData.Sprites;
using GameShared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoulderDash
{
    public class Camera
    {
        private Game1 game;
        public Matrix Transform { get; private set; }
        private DisplayOrientation orientation = DisplayOrientation.Default;

        public Camera(Game1 game)
        {
            this.game = game;
        }

        public void Follow(Actor target)
        {
            Matrix position = position = Matrix.CreateTranslation(
                  -target.Position.X - (target.Size.X / 2),
                  -target.Position.Y - (target.Size.Y / 2),
                  0);
            Matrix offset;
            if (orientation == DisplayOrientation.LandscapeLeft || orientation == DisplayOrientation.LandscapeRight)
            {
                offset = Matrix.CreateTranslation(
                game.GetScaledResolution().X / 2,
                game.GetScaledResolution().Y / 2,
                0);
            }
            else
            {
                offset = Matrix.CreateTranslation(
                game.GetScaledResolution().X / 2,
                game.GetScaledResolution().Y / 2,
                0);
            }

            

            Transform = position * offset ;
            
        }
        public void ChangeOrientation(DisplayOrientation orientation)
        {
            this.orientation = orientation;
        }
    }
}