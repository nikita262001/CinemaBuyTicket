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
    public partial class AdminPanelMaster : ContentPage
    {
        MainPage main;
        public AdminPanelMaster(MainPage _main)
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

            Button buttonListMovie = new ButtonPanel { Text = "Список фильмов" };
            buttonListMovie.Clicked += PageListMoview;
            Button buttonListAccount = new ButtonPanel { Text = "Список аккаунтов" };
            buttonListAccount.Clicked += PageListAccount;
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
                            Children = { image, lName, lSurname, lPatronymic }
                        },
                        BackgroundColor = Color.LightSkyBlue,
                        CornerRadius = 20,
                        Margin = 10,
                    },
                    buttonListMovie,
                    buttonListAccount,
                    buttonMenu
                }
            };
            Content = new ScrollView { Content = stack };
        }

        private void PageMenu(object sender, EventArgs e)
        {
            (main.Detail as NavigationPage).PopToRootAsync();
            main.Master = new AboutTheCreator() { Title = "О создателе" };
        }

        private void PageListAccount(object sender, EventArgs e)
        {
            (main.Detail as NavigationPage).PushAsync(new EditListAccount());
        }

        private void PageListMoview(object sender, EventArgs e)
        {
            (main.Detail as NavigationPage).PushAsync(new EditListMovie());
        }
    }
}