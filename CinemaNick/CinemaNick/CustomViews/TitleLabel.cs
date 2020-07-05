using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CinemaNick
{
    class TitleLabel : Label
    {
        public TitleLabel()
        {
            VerticalOptions = LayoutOptions.StartAndExpand;
            FontSize = 25;
            HeightRequest = 50;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalTextAlignment = TextAlignment.Center;
            BackgroundColor = Color.LightSkyBlue;
        }
    }
}
