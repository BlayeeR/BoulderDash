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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;


namespace BoulderDash
{
    public class InputManager : IGameComponent
    {
        TouchCollection touchCollection;
        GestureSample gesture;
        private static InputManager instance;
        public event EventHandler FlickLeft, FlickRight, FlickUp, FlickDown;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                    instance.Initialize();
                }
                return instance;
            }
        }

        public void Initialize()
        {
            gesture = default(GestureSample);
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.Flick;
        }
        public void Update(GameTime gameTime)
        {
            touchCollection = TouchPanel.GetState();
            while (TouchPanel.IsGestureAvailable)
            {
                gesture = TouchPanel.ReadGesture();
                switch (gesture.GestureType)
                {
                    case GestureType.Flick:
                        {
                            if (gesture.Delta.X > 0 && gesture.Delta.Y > 0)//topright
                            {
                                if (gesture.Delta.X < gesture.Delta.Y)
                                    FlickUp(this, new EventArgs());
                                else
                                    FlickRight(this, new EventArgs());

                            }
                            else if (gesture.Delta.X > 0 && gesture.Delta.Y < 0)//bottomright
                            {
                                if (gesture.Delta.X < -gesture.Delta.Y)
                                    FlickDown(this, new EventArgs());
                                else
                                    FlickRight(this, new EventArgs());
                            }
                            else if (gesture.Delta.X < 0 && gesture.Delta.Y < 0)//bottomleft
                            {
                                if (-gesture.Delta.X < -gesture.Delta.Y)
                                    FlickDown(this, new EventArgs());
                                else
                                    FlickLeft(this, new EventArgs());
                            }
                            else if (gesture.Delta.X < 0 && gesture.Delta.Y > 0)//topleft
                            {
                                if (-gesture.Delta.X < gesture.Delta.Y)
                                    FlickUp(this, new EventArgs());
                                else
                                    FlickLeft(this, new EventArgs());
                            }
                            break;
                        }
                }
            }
        }
    }
}