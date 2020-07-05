using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CinemaNick
{
    class CustomFrame :Frame
    {
        public CustomFrame()
        {
            BackgroundColor = Color.LightSkyBlue;
            CornerRadius = 20;
            Margin = 10;
            BorderColor = Color.Red;
        }
    }
}
