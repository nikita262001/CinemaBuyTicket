using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CinemaNick
{
    class ButtonPanel : Button
    {
        public ButtonPanel()
        {
            Margin = 10;
            HeightRequest = 60;
            CornerRadius = 20;
            FontSize = 20;
            BackgroundColor = Color.Red;
            TextColor = Color.LightSkyBlue;
        }
    }
}
