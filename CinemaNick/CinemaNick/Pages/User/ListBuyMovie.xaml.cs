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
    public partial class ListBuyMovie : ContentPage
    {
        MainPage main;
        CollectionView listMovie;
        public ListBuyMovie(MainPage _main)
        {
            main = _main;
            NavigationPage.SetHasBackButton(this, false);
            Label label = new TitleLabel { Text = "Купленные фильмы" };
            listMovie = new CollectionViewCustom();
            listMovie.ItemTemplate = new DataTemplate(() =>
            {
                Label lName = new Label { FontSize = 14, };
                lName.SetBinding(Label.TextProperty, new Binding { Path = "Name", StringFormat = "Наименование фильма: {0}", });

                Label lDateCreate = new Label { FontSize = 14, };
                lDateCreate.SetBinding(Label.TextProperty, new Binding { Path = "DateCreate", StringFormat = "Дата выпуска фильма: {0:dd/MM/yyyy}", });

                Label lPrice = new Label { FontSize = 14, };
                lPrice.SetBinding(Label.TextProperty, new Binding { Path = "Price", StringFormat = "Цена: {0}", });

                Label lDateBuy = new Label { FontSize = 14, };
                lDateBuy.SetBinding(Label.TextProperty, new Binding { Path = "DateBuy", StringFormat = "Дата покупки фильма: {0:dd/MM/yyyy}", });

                Image image = new Image { Aspect = Aspect.AspectFit };
                image.SetBinding(Image.SourceProperty, new Binding { Path = "ImageLink" });

                #region RelativeLayout
                RelativeLayout relative = new RelativeLayout { };

                relative.Children.Add(image,
                     Constraint.RelativeToParent((parent) => -5),
                     Constraint.RelativeToParent((parent) => -5),
                     Constraint.RelativeToParent((parent) => parent.Width * 0.075 + 150),
                     Constraint.RelativeToParent((parent) => 150));

                relative.Children.Add(lName,
                     Constraint.RelativeToView(image, (parent, view) => view.X + view.Width + 10),
                     Constraint.RelativeToView(image, (parent, view) => 5));

                relative.Children.Add(lDateCreate,
                     Constraint.RelativeToView(lName, (parent, view) => view.X),
                     Constraint.RelativeToView(lName, (parent, view) => view.Y + view.Height + 2));

                relative.Children.Add(lPrice,
                     Constraint.RelativeToView(lDateCreate, (parent, view) => view.X),
                     Constraint.RelativeToView(lDateCreate, (parent, view) => view.Y + view.Height + 2));

                relative.Children.Add(lDateBuy,
                     Constraint.RelativeToView(lPrice, (parent, view) => view.X),
                     Constraint.RelativeToView(lPrice, (parent, view) => view.Y + view.Height + 2));

                #endregion

                return new CustomFrameCollection { Content = relative };
            });

            Content = new StackLayout { Children = { label, listMovie } };
        }
        protected async override void OnAppearing()
        {
            List<BuyClass> buys = new List<BuyClass>();
            var listBuyMovie = (await App.DBBuyMovie.GetItemsAsync()).Where((item) => item.IdAccount == main.GetAccount.IdAccount);
            foreach (var item in listBuyMovie)
            {
                Movie movie = await App.DBMovie.GetItemAsync(item.IdMovie);
                BuyClass buy = new BuyClass 
                {
                    IdBuyMovie = item.IdBuyMovie, DateBuy = item.DateBuy , // данные которые относятся к самой покупки
                    IdAccount = main.GetAccount.IdAccount, Login = main.GetAccount.Login, // данные аккаунта купившего эту покупку
                    DateCreate = movie.DateCreate,Name = movie.Name , Price = movie.Price , ImageLink = movie.ImageLink // данные фильма
                };
                buys.Add(buy);
            }
            listMovie.ItemsSource = buys;
        }
    }
}