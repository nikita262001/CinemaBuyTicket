using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaNick
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMovie : ContentPage
    {
        List<string> elementsPickerImage = new List<string> // ссылки на картинки
        {
            Device.RuntimePlatform == Device.Android ? "film1.jpg" : "Images/film1.jpg",
            Device.RuntimePlatform == Device.Android ? "film2.jpg" : "Images/film2.jpg",
            Device.RuntimePlatform == Device.Android ? "film3.jpg" : "Images/film3.jpg",
            Device.RuntimePlatform == Device.Android ? "film4.jpg" : "Images/film4.jpg",
            Device.RuntimePlatform == Device.Android ? "film5.jpg" : "Images/film5.jpg",
            Device.RuntimePlatform == Device.Android ? "film6.jpg" : "Images/film6.jpg",
            Device.RuntimePlatform == Device.Android ? "film7.jpg" : "Images/film7.jpg",
            Device.RuntimePlatform == Device.Android ? "film8.jpg" : "Images/film8.jpg",
            Device.RuntimePlatform == Device.Android ? "film9.jpg" : "Images/film9.jpg",
            Device.RuntimePlatform == Device.Android ? "film10.jpg" : "Images/film10.jpg",
        };

        Button button;
        Button buttonDel;
        Image imageMovie;
        Picker pImage;
        public EditMovie()
        {
            #region elements
            Label lID = new Label { };
            lID.SetBinding(Label.TextProperty, new Binding { Path = "IdMovie", StringFormat = "ID фильма: {0}" });

            Label lName = new Label { Text = "Название: ", WidthRequest = 200, };
            Entry eName = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            eName.SetBinding(Entry.TextProperty, new Binding { Path = "Name" });
            StackLayout sName = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lName, eName } };

            Label lDateCreate = new Label { Text = "Дата выхода фильма: ", WidthRequest = 200, };
            DatePicker eDateCreate = new DatePicker { HorizontalOptions = LayoutOptions.StartAndExpand, };
            eDateCreate.SetBinding(DatePicker.DateProperty, new Binding { Path = "DateCreate" });
            StackLayout sDateCreate = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lDateCreate, eDateCreate } };

            Label lPrice = new Label { Text = "Цена: ", WidthRequest = 200, };
            Entry ePrice = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            ePrice.SetBinding(Entry.TextProperty, new Binding { Path = "Price" });
            StackLayout sPrice = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lPrice, ePrice } };

            Label lImage = new Label { Text = "Картинка: ", WidthRequest = 200, };
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

            Frame frame = new CustomFrame
            {
                Content = new StackLayout { Children = { lID, sName, sDateCreate, sPrice, sImage, imageMovie } },
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
            App.DBMovie.DeleteItemAsync(BindingContext as Movie);
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
            if ((BindingContext as Movie).IdMovie == 0)
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
            App.DBMovie.EditItemAsync(BindingContext as Movie);
            Navigation.PopAsync();
        }

        private void AddBicycleDB(object sender, EventArgs e)
        {
            App.DBMovie.SaveItemAsync(BindingContext as Movie);
            Navigation.PopAsync();
        }
    }
}