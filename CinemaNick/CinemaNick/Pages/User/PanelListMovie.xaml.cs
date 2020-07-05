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
    public partial class PanelListMovie : ContentPage
    {
        CollectionView listMovie;
        MainPage main;
        public PanelListMovie(MainPage _main)
        {
            main = _main;
            NavigationPage.SetHasBackButton(this, false);
            Label labelTitle = new TitleLabel { Text = "Список фильмов" };
            listMovie = new CollectionViewCustom();
            listMovie.ItemTemplate = new DataTemplate(() =>
            {
                Label lName = new Label { FontSize = 14, };
                lName.SetBinding(Label.TextProperty, new Binding { Path = "Name", StringFormat = "Наименование фильма: {0}", });

                Label lSurname = new Label { FontSize = 14, };
                lSurname.SetBinding(Label.TextProperty, new Binding { Path = "DateCreate", StringFormat = "Дата выпуска фильма: {0:dd/MM/yyyy}", });

                Label lPrice = new Label { FontSize = 14, };
                lPrice.SetBinding(Label.TextProperty, new Binding { Path = "Price", StringFormat = "Цена: {0}", });

                Image image = new Image { Aspect = Aspect.AspectFit };
                image.SetBinding(Image.SourceProperty, new Binding { Path = "ImageLink" });

                #region RelativeLayout
                RelativeLayout relative = new RelativeLayout { };

                relative.Children.Add(image,
                     Constraint.RelativeToParent((parent) => -5),
                     Constraint.RelativeToParent((parent) => -5),
                     Constraint.RelativeToParent((parent) => parent.Width * 0.125 + 75),
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

                return new CustomFrameCollection { Content = relative} ;
            });

            listMovie.SelectionChanged += ListSpecial_ItemTapped;

            RelativeLayout mainR = new RelativeLayout { };

            #region mainR
            mainR.Children.Add(labelTitle,
                Constraint.RelativeToParent((parent) => 0),
                    Constraint.RelativeToParent((parent) => 0),
                    Constraint.RelativeToParent((parent) => parent.Width),
                    Constraint.RelativeToParent((parent) => parent.Height * 0.08));

            mainR.Children.Add(listMovie,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToView(labelTitle, (parent, view) => view.Height + view.Y),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.92));
            #endregion
            Content = mainR;
        }
        protected async override void OnAppearing()
        {
            listMovie.ItemsSource = await App.DBMovie.GetItemsAsync();
        }
        private async void ListSpecial_ItemTapped(object sender, SelectionChangedEventArgs e)
        {
            var answer = await DisplayAlert("Уведомление", "Вы уверены что хотите добавить этот фильм в корзину", "Да", "Нет");
            if (answer)
            {
                main.GetBasketMovies.Add(e.CurrentSelection.FirstOrDefault() as Movie);
            }
        }
    }
}