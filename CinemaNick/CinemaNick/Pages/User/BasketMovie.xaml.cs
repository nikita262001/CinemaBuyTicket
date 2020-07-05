using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaNick
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketMovie : ContentPage
    {
        MainPage main;
        CollectionView list;
        Label labelSum;
        public BasketMovie(MainPage _main)
        {
            main = _main;
            NavigationPage.SetHasBackButton(this, false);

            Label label = new TitleLabel { Text = "Корзина" };

            labelSum = new TitleLabel { HorizontalTextAlignment = TextAlignment.End , BackgroundColor = Color.Transparent};

            Button button = new EndButton { Text = "Купить" };
            button.Clicked += Buy;
            #region ListView
            list = new CollectionViewCustom { };
            list.ItemsSource = main.GetBasketMovies;
            list.SelectionChanged += ListSpecial_ItemTapped;

            list.ItemTemplate = new DataTemplate(() =>
            {
                Label lName = new Label { FontSize = 14, };
                lName.SetBinding(Label.TextProperty, new Binding { Path = "Name", StringFormat = "Название: {0}", });

                Label lSurname = new Label { FontSize = 14, };
                lSurname.SetBinding(Label.TextProperty, new Binding { Path = "Surname", StringFormat = "Дата выхода: {0}", });

                Label lPrice = new Label { FontSize = 14, };
                lPrice.SetBinding(Label.TextProperty, new Binding { Path = "Price", StringFormat = "Цена: {0}", });

                Image image = new Image { Aspect = Aspect.AspectFit };
                image.SetBinding(Image.SourceProperty, new Binding { Path = "ImageLink" });

                #region RelativeLayout
                RelativeLayout relative = new RelativeLayout { };

                relative.Children.Add(image,
                     Constraint.RelativeToParent((parent) => -5),
                     Constraint.RelativeToParent((parent) => -5),
                     Constraint.RelativeToParent((parent) => parent.Width * 0.25),
                     Constraint.RelativeToParent((parent) => 150));

                relative.Children.Add(lName,
                     Constraint.RelativeToView(image, (parent, view) => view.X + view.Width + 10),
                     Constraint.RelativeToView(image, (parent, view) => 5));

                relative.Children.Add(lSurname,
                     Constraint.RelativeToView(lName, (parent, view) => view.X),
                     Constraint.RelativeToView(lName, (parent, view) => view.Y + view.Height + 2));

                relative.Children.Add(lPrice,
                     Constraint.RelativeToView(lSurname, (parent, view) => view.X),
                     Constraint.RelativeToView(lSurname, (parent, view) => view.Y + view.Height + 2));

                #endregion

                return new CustomFrameCollection { Content = relative } ;
            });
            #endregion

            Content = new StackLayout { Children = { label, list, labelSum, button }, };

        }
        protected override void OnAppearing()
        {
            list.ItemsSource =  main.GetBasketMovies;
            ResetSum();
        }

        private async void Buy(object sender, EventArgs e)
        {
            Account account = main.GetAccount;
            int sum = 0;
            foreach (var item in main.GetBasketMovies)
            {
                sum += item.Price;
            }
            var answer = await DisplayAlert("Уведомление", "Вы уверены что хотите купить фильмы", "Да","Нет");
            if (answer)
            {
                if (account.Money >= sum)
                {
                    account.Money -= sum;
                    main.GetAccount = account;
                    await App.DBAccount.EditItemAsync(account);
                    List<BuyMovie> buyMovies = new List<BuyMovie>();
                    foreach (var item in main.GetBasketMovies)
                    {
                        BuyMovie buy = new BuyMovie { IdAccount = account.IdAccount, IdMovie = item.IdMovie, DateBuy = DateTime.Now };
                        buyMovies.Add(buy);
                    }
                    await App.DBBuyMovie.SaveItemsAsync(buyMovies);
                    main.GetBasketMovies.Clear();
                    list.ItemsSource = null;
                    (main.Detail as NavigationPage).PushAsync(new PanelListMovie(main));
                    main.Master = new UserPanelMaster(main) { Title = "Панель пользователя", BindingContext = account };
                }
                else
                {
                    await DisplayAlert("Уведомление", "На вашем аккаунте не хватает денег", "Окей");
                }
            }
        }

        private async void ListSpecial_ItemTapped(object sender, SelectionChangedEventArgs e) // удалить фильм из корзины
        {
            var answer = await DisplayAlert("Уведомление","Вы уверены что хотите удалить из списка этот фильм","Да","Нет");
            if (answer)
            {
                var item = e.CurrentSelection.FirstOrDefault() as Movie;
                var listMovie = (main.GetBasketMovies.Where((selectItem) => selectItem.IdMovie != item.IdMovie)).ToList();
                main.GetBasketMovies = listMovie;
                list.ItemsSource = listMovie;
                ResetSum();
            }
        }
        
        private void ResetSum()
        {
            int sum = 0;
            foreach (var item in main.GetBasketMovies)
            {
                sum += item.Price;
            }
            labelSum.Text = $"Сумма: {sum}\t";
        }
    }
}