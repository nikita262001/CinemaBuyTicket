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
    public partial class UserPanelMaster : ContentPage
    {
        MainPage main;
        public UserPanelMaster(MainPage _main)
        {
            main = _main;
            Image image = new Image { Aspect = Aspect.AspectFit, Margin = 15 };
            image.SetBinding(Image.SourceProperty, new Binding { Path = "ImageLink" });

            Label lName = new Label { FontSize = 14, Margin = new Thickness(10, 0) };
            lName.SetBinding(Label.TextProperty, new Binding { Path = "Name", StringFormat = "Имя: {0}", });

            Label lSurname = new Label { FontSize = 14, Margin = new Thickness(10, 0) };
            lSurname.SetBinding(Label.TextProperty, new Binding { Path = "Surname", StringFormat = "Фамилия: {0}", });

            Label lPatronymic = new Label { FontSize = 14, Margin = new Thickness(10, 0) };
            lPatronymic.SetBinding(Label.TextProperty, new Binding { Path = "Patronymic", StringFormat = "Отчество: {0}", });

            Label lMoney = new Label { FontSize = 14, Margin = new Thickness(10, 0) };
            lMoney.SetBinding(Label.TextProperty, new Binding { Path = "Money", StringFormat = "Денег на счету: {0}", });

            Button buttonListMovie = new ButtonPanel { Text = "Список фильмов" };
            buttonListMovie.Clicked += PageListMovie;
            Button buttonBasket = new ButtonPanel { Text = "Корзина" };
            buttonBasket.Clicked += PageBasket;
            Button buttonBuyMovie = new ButtonPanel { Text = "Купленные фильмы" };
            buttonBuyMovie.Clicked += PageBuyMovie;
            Button buttonRefill = new ButtonPanel { Text = "Пополнение счета" };
            buttonRefill.Clicked += PageRefill;
            Button buttonMenu = new ButtonPanel { Text = "Перейти на главное меню" };
            buttonMenu.Clicked += PageMenu;


            StackLayout stack = new StackLayout
            {
                Children =
                {
                    new Frame
                    {
                        Content = new StackLayout
                        {
                            Children = { image, lName, lSurname, lPatronymic, lMoney }
                        },
                        BackgroundColor = Color.LightSkyBlue,
                        CornerRadius = 20,
                        Margin = 10,
                    },
                    buttonListMovie, 
                    buttonBasket, 
                    buttonBuyMovie, 
                    buttonRefill, 
                    buttonMenu
                }
            };
            Content = new ScrollView { Content = stack };
        }

        private void PageListMovie(object sender, EventArgs e)
        {
            (main.Detail as NavigationPage).PushAsync(new PanelListMovie(main));
        }
        private void PageBasket(object sender, EventArgs e)
        {
            (main.Detail as NavigationPage).PushAsync(new BasketMovie(main));
        }
        private void PageBuyMovie(object sender, EventArgs e)
        {
            (main.Detail as NavigationPage).PushAsync(new ListBuyMovie(main));
        }
        private void PageRefill(object sender, EventArgs e)
        {
            (main.Detail as NavigationPage).PushAsync(new Refill(main) { BindingContext = main.GetAccount});
        }
        private void PageMenu(object sender, EventArgs e)
        {
            (main.Detail as NavigationPage).PopToRootAsync();
            main.Master = new AboutTheCreator() { Title = "О создателе" };
        }
    }
}