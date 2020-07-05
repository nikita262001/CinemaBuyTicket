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
    public partial class PanelRegistration : ContentPage
    {
        List<string> elementsPickerImage = new List<string> // ссылки на картинки
        {
            Device.RuntimePlatform == Device.Android ? "image1.jpg" : "Images/image1.jpg"  ,
            Device.RuntimePlatform == Device.Android ? "image2.jpg" : "Images/image2.jpg"  ,
            Device.RuntimePlatform == Device.Android ? "image3.jpg" : "Images/image3.jpg"  ,
            Device.RuntimePlatform == Device.Android ? "image4.jpg" : "Images/image4.jpg"  ,
            Device.RuntimePlatform == Device.Android ? "image5.jpg" : "Images/image5.jpg"  ,
            Device.RuntimePlatform == Device.Android ? "image6.jpg" : "Images/image6.jpg"  ,
            Device.RuntimePlatform == Device.Android ? "image7.jpg" : "Images/image7.jpg"  ,
            Device.RuntimePlatform == Device.Android ? "image8.jpg" : "Images/image8.jpg"  ,
            Device.RuntimePlatform == Device.Android ? "image9.jpg" : "Images/image9.jpg"  ,
            Device.RuntimePlatform == Device.Android ? "image10.jpg" : "Images/image10.jpg"
        };


        Image imageMovie;
        Picker pImage;
        public PanelRegistration()
        {
            #region elements
            Label lLogin = new Label { Text = "Логин: ", WidthRequest = 200, };
            Entry eLogin = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            eLogin.SetBinding(Entry.TextProperty, new Binding { Path = "Login" });
            StackLayout sLogin = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lLogin, eLogin } };

            Label lPassword = new Label { Text = "Пароль: ", WidthRequest = 200, };
            Entry ePassword = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            ePassword.SetBinding(Entry.TextProperty, new Binding { Path = "Password" });
            StackLayout sPassword = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lPassword, ePassword } };

            Label lName = new Label { Text = "Имя: ", WidthRequest = 200, };
            Entry eName = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            eName.SetBinding(Entry.TextProperty, new Binding { Path = "Name" });
            StackLayout sName = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lName, eName } };

            Label lSurname = new Label { Text = "Фамилия: ", WidthRequest = 200, };
            Entry eSurname = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            eSurname.SetBinding(Entry.TextProperty, new Binding { Path = "Surname" });
            StackLayout sSurname = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lSurname, eSurname } };

            Label lPatronymic = new Label { Text = "Отчество: ", WidthRequest = 200, };
            Entry ePatronymic = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            ePatronymic.SetBinding(Entry.TextProperty, new Binding { Path = "Patronymic" });
            StackLayout sPatronymic = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lPatronymic, ePatronymic } };

            Label lImage = new Label { Text = "Аватарка:", WidthRequest = 200, };
            pImage = new Picker { HorizontalOptions = LayoutOptions.StartAndExpand, };
            pImage.SelectedIndexChanged += PImage_SelectedIndexChanged;
            foreach (var item in elementsPickerImage)
            {
                pImage.Items.Add(item);
            }
            pImage.SetBinding(Picker.SelectedItemProperty, new Binding { Path = "ImageLink" });
            StackLayout sImage = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lImage, pImage } };

            imageMovie = new Image
            {
                HeightRequest = 300,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFit,
                IsVisible = false,
            };

            #endregion

            Frame frame = new Frame
            {
                Content = new StackLayout { Children = { sLogin, sPassword, sName, sSurname, sPatronymic, sImage, imageMovie } },
                CornerRadius = 20,
                BackgroundColor = Color.LightSkyBlue,
                Margin = 20,
            };

            Button button = new Button
            {
                HeightRequest = 75,
                FontSize = 20,
                TextColor = Color.LightSkyBlue,
                BackgroundColor = Color.Red,
                Text = "Добавить",
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            button.Clicked += AddAccount;

            StackLayout mainStack = new StackLayout { Children = { frame, button } };

            Content = new ScrollView { Content = mainStack };
        }

        private void AddAccount(object sender, EventArgs e)
        {
            App.DBAccount.SaveItemAsync(BindingContext as Account);
            Navigation.PopAsync();
        }

        private void PImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pImage.SelectedIndex != -1)
            {
                imageMovie.IsVisible = true;
                imageMovie.Source = elementsPickerImage[pImage.SelectedIndex];
            }
        }
    }
}