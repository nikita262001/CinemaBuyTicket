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
    public partial class EditListMovie : ContentPage
    {
        List<Movie> movies = new List<Movie>();
        CollectionView list;
        public EditListMovie()
        {
            NavigationPage.SetHasBackButton(this, false);
            Label label = new TitleLabel { Text = "Список фильмов"};

            Button button = new EndButton {Text = "Добавить"};
            button.Clicked += AddBicycle;
            #region ListView
            list = new CollectionViewCustom { };
            list.SelectionChanged += List_ItemTapped;

            list.ItemTemplate = new DataTemplate(() =>
            {
                Label lIdAccount = new Label { FontSize = 14, };
                lIdAccount.SetBinding(Label.TextProperty, new Binding { Path = "IdMovie", StringFormat = "ID BuyMovie: {0}", });

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
                     Constraint.RelativeToParent((parent) => parent.Width * 0.075 + 150),
                     Constraint.RelativeToParent((parent) => 150));

                relative.Children.Add(lIdAccount,
                     Constraint.RelativeToView(image, (parent, view) => view.X + view.Width + 10),
                     Constraint.RelativeToView(image, (parent, view) => 5));

                relative.Children.Add(lName,
                     Constraint.RelativeToView(lIdAccount, (parent, view) => view.X),
                     Constraint.RelativeToView(lIdAccount, (parent, view) => view.Y + view.Height + 2));

                relative.Children.Add(lSurname,
                     Constraint.RelativeToView(lName, (parent, view) => view.X),
                     Constraint.RelativeToView(lName, (parent, view) => view.Y + view.Height + 2));

                relative.Children.Add(lPrice,
                     Constraint.RelativeToView(lSurname, (parent, view) => view.X),
                     Constraint.RelativeToView(lSurname, (parent, view) => view.Y + view.Height + 2));

                #endregion

                return new CustomFrameCollection { Content = relative };
            });
            #endregion

            Content = new StackLayout { Children = { label, list, button }, };
        }

        private void List_ItemTapped(object sender, SelectionChangedEventArgs e)
        {
            Navigation.PushAsync(new EditMovie { BindingContext = e.CurrentSelection.FirstOrDefault() as Movie });
        }

        private void AddBicycle(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditMovie { BindingContext = new Movie() });
        }

        protected async override void OnAppearing()
        {
            list.ItemsSource = await App.DBMovie.GetItemsAsync();
        }
    }
}