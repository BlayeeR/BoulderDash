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
using GameShared.Interfaces;

namespace BoulderDash.Scenes
{
    public interface IScene : IComponent
    {
        Camera Camera { get; }
    }
}