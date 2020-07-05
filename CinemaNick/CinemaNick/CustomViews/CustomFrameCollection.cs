using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CinemaNick
{
    class CustomFrameCollection: Frame
    {
        public CustomFrameCollection()
        {
            CornerRadius = 20;
            BackgroundColor = Color.LightSkyBlue;
            HeightRequest = 150;
        }
    }
}
