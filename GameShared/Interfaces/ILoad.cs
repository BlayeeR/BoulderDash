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
using Microsoft.Xna.Framework.Content;

namespace GameShared.Interfaces
{
    public interface ILoad
    {
        void LoadContent(ContentManager content);
        void UnloadContent();
    }
}