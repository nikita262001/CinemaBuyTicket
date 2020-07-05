using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaNick
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutTheCreator : ContentPage
    {
        public AboutTheCreator()
        {
            this.BackgroundColor = Color.LightSteelBlue;
            StackLayout stack = new StackLayout();

            Image image = new Image { Source = Device.RuntimePlatform == Device.Android ? "Shiro.jpg" : "Images/Shiro.jpg", Aspect = Aspect.AspectFit, Margin = 15 };
            Label label = new Label
            {
                Text = "Место обучения: МЦК-КТИТС" +
                "\nФИО:Волков Никита Валерьевич" +
                "\nГруппа: 321п" +
                "\nНаименование приложения: CinemaNick" +
                "\nДата создания: 16.05.2020",
                FontSize = 15,
                Margin = 15
            };

            stack.Children.Add(image);
            stack.Children.Add(label);

            Content = stack;
        }
    }
}