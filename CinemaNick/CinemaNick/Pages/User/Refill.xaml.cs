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
    public partial class Refill : ContentPage
    {
        MainPage main;
        Entry entryMoney;
        public Refill(MainPage _main) // пополнение счета
        {
            main = _main;

            Image image = new Image { Aspect = Aspect.AspectFit,WidthRequest = 100 };
            image.SetBinding(Image.SourceProperty, new Binding { Path = "ImageLink" });

            Label lName = new Label { FontSize = 14, Margin = new Thickness(10, 0) };
            lName.SetBinding(Label.TextProperty, new Binding { Path = "Name", StringFormat = "Имя: {0}", });

            Label lSurname = new Label { FontSize = 14, Margin = new Thickness(10, 0) };
            lSurname.SetBinding(Label.TextProperty, new Binding { Path = "Surname", StringFormat = "Фамилия: {0}", });

            Label lPatronymic = new Label { FontSize = 14, Margin = new Thickness(10, 0) };
            lPatronymic.SetBinding(Label.TextProperty, new Binding { Path = "Patronymic", StringFormat = "Отчество: {0}", });

            Label lMoney = new Label { FontSize = 14, Margin = new Thickness(10, 0) };
            lMoney.SetBinding(Label.TextProperty, new Binding { Path = "Money", StringFormat = "Денег на счету: {0}", });

            Entry entryCardNumber = new Entry { Placeholder = "Номер карты", Margin = new Thickness(10,2)};
            Entry entryCardCode = new Entry { Placeholder = "Защитный код", Margin = new Thickness(10, 2) };
            entryMoney = new Entry { Placeholder = "Кол-во денег на пополнение счета", Margin = new Thickness(10, 2) };
            Button button = new EndButton { Text = "Пополнить счет"};
            button.Clicked += Button_Clicked;

            StackLayout stack = new StackLayout
            {
                Children =
                {
                    new CustomFrame
                    {
                        Content = new StackLayout
                        {
                            Children = { image, new StackLayout { Children = { lName, lSurname, lPatronymic, lMoney } }  },
                            Orientation = StackOrientation.Horizontal,
                        },
                    },
                    entryCardNumber,
                    entryCardCode,
                    entryMoney,
                    button
                }
            };
            Content = new ScrollView { Content = stack };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var account = BindingContext as Account;
            int money;
            try
            {
                money = Convert.ToInt32(entryMoney.Text);
            }
            catch (Exception)
            {
                DisplayAlert("Уведомление","Не правильно введено количество пополняемой валюты","Окей");
                return;
            }
            account.Money += money;
            App.DBAccount.EditItemAsync(account);
            (main.Detail as NavigationPage).PopAsync();
            main.Master = new UserPanelMaster(main) { Title = "Панель пользователя", BindingContext = account };
        }
    }
}