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
    public partial class EditAccount : ContentPage
    {
        List<string> elementsPickerImage = new List<string> // ссылки на картинки
        {
            Device.RuntimePlatform == Device.Android ? "Shiro.jpg" : "Images/Shiro.jpg"  ,
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

        Button button;
        Button buttonDel;
        Image imageMovie;
        Picker pImage;
        public EditAccount()
        {
            #region elements
            Label lID = new Label { };
            lID.SetBinding(Label.TextProperty, new Binding { Path = "IdAccount", StringFormat = "ID: {0}" });

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

            Label lMoney = new Label { Text = "Кол-во валюты: ", WidthRequest = 200, };
            Entry eMoney = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            eMoney.SetBinding(Entry.TextProperty, new Binding { Path = "Money" });
            StackLayout sMoney = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lMoney, eMoney } };

            Label lAdmin = new Label { Text = "Администратор: ", WidthRequest = 200, };
            Switch eAdmin = new Switch { HorizontalOptions = LayoutOptions.FillAndExpand, };
            eAdmin.SetBinding(Switch.IsToggledProperty, new Binding { Path = "Admin" });
            StackLayout sAdmin = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lAdmin, eAdmin } };

            Label lImage = new Label { Text = "Аватарка: ", WidthRequest = 200, };
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
                Content = new StackLayout { Children = { lID, sLogin, sPassword, sName, sSurname, sPatronymic, sMoney, sAdmin, sImage, imageMovie } },
                CornerRadius = 20,
                BackgroundColor = Color.LightSkyBlue,
                Margin = 20,
            };

            buttonDel = new EndButton {Text = "Удалить", IsVisible = false };
            buttonDel.Clicked += DeleteBicycle;
            button = new EndButton { };
            StackLayout sButtons = new StackLayout { Children = { buttonDel, button }, VerticalOptions = LayoutOptions.EndAndExpand };

            StackLayout mainStack = new StackLayout { Children = { frame, sButtons } };

            Content = new ScrollView { Content = mainStack };
        }

        private void DeleteBicycle(object sender, EventArgs e)
        {
            App.DBAccount.DeleteItemAsync(BindingContext as Account);
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

        protected override void OnAppearing()
        {
            if ((BindingContext as Account).IdAccount == 0)
            {
                button.Text = "Добавить";
                button.Clicked += AddBicycleDB;
            }
            else
            {
                imageMovie.IsVisible = true;
                buttonDel.IsVisible = true;
                button.Text = "Редактировать";
                button.Clicked += EditBicycleDB;
            }
        }

        private void EditBicycleDB(object sender, EventArgs e)
        {
            App.DBAccount.EditItemAsync(BindingContext as Account);
            Navigation.PopAsync();
        }

        private void AddBicycleDB(object sender, EventArgs e)
        {
            App.DBAccount.SaveItemAsync(BindingContext as Account);
            Navigation.PopAsync();
        }
    }
}