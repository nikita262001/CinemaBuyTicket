using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CinemaNick
{
    class EndButton : Button
    {
        public EndButton()
        {
            VerticalOptions = LayoutOptions.EndAndExpand;
            HeightRequest = 50;
            FontSize = 25;
            BackgroundColor = Color.LightSkyBlue;
            TextColor = Color.Red;
        }
    }
}
