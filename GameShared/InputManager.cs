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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;


namespace GameShared
{
    public class InputManager : IGameComponent
    {
        TouchCollection touchCollection;
        GestureSample gesture;
        private static InputManager instance;
        public event EventHandler OnFlickLeft, OnFlickRight, OnFlickUp, OnFlickDown, OnBackButtonClicked, OnTap;

        public Vector2 ScaledResolution = Vector2.Zero;

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
                                    OnFlickDown?.Invoke(gesture, null);
                                else
                                    OnFlickRight?.Invoke(gesture, null);

                            }
                            else if (gesture.Delta.X > 0 && gesture.Delta.Y < 0)//bottomright
                            {
                                if (gesture.Delta.X < -gesture.Delta.Y)
                                    OnFlickUp?.Invoke(gesture, null);
                                else
                                    OnFlickRight?.Invoke(gesture, null);
                            }
                            else if (gesture.Delta.X < 0 && gesture.Delta.Y < 0)//bottomleft
                            {
                                if (-gesture.Delta.X < -gesture.Delta.Y)
                                    OnFlickUp?.Invoke(gesture, null);
                                else
                                    OnFlickLeft?.Invoke(gesture, null);
                            }
                            else if (gesture.Delta.X < 0 && gesture.Delta.Y > 0)//topleft
                            {
                                if (-gesture.Delta.X < gesture.Delta.Y)
                                    OnFlickDown?.Invoke(gesture, null);
                                else
                                    OnFlickLeft?.Invoke(gesture, null);
                            }
                            break;
                        }
                    case GestureType.Tap:
                        {
                            if(ScaledResolution != Vector2.Zero)
                                OnTap?.Invoke(new Vector2(gesture.Position.X/TouchPanel.DisplayWidth*ScaledResolution.X, gesture.Position.Y/TouchPanel.DisplayHeight*ScaledResolution.Y), null);
                            
                            break;
                        }
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                OnBackButtonClicked?.Invoke(this, null);
        }
    }
}