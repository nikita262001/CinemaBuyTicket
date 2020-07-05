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
    public partial class EditListAccount : ContentPage
    {
        List<Account> accounts = new List<Account>();
        ListView list;
        public EditListAccount()
        {
            NavigationPage.SetHasBackButton(this, false);
            Label label = new TitleLabel { Text = "Список аккаунтов" };

            Button button = new EndButton {Text = "Добавить"};
            button.Clicked += AddBicycle;
            #region ListView
            list = new ListView { RowHeight = 220, };
            list.ItemTapped += List_ItemTapped;

            list.ItemTemplate = new DataTemplate(() =>
            {
                Label lIdAccount = new Label { FontSize = 14, };
                lIdAccount.SetBinding(Label.TextProperty, new Binding { Path = "IdAccount", StringFormat = "ID: {0}", });

                Label lName = new Label { FontSize = 14, };
                lName.SetBinding(Label.TextProperty, new Binding { Path = "Name", StringFormat = "Имя: {0}", });

                Label lSurname = new Label { FontSize = 14, };
                lSurname.SetBinding(Label.TextProperty, new Binding { Path = "Surname", StringFormat = "Фамилия: {0}", });

                Label lPrice = new Label { FontSize = 14, };
                lPrice.SetBinding(Label.TextProperty, new Binding { Path = "Patronymic", StringFormat = "Отчество: {0}", });

                Label lMoney = new Label { FontSize = 14, };
                lMoney.SetBinding(Label.TextProperty, new Binding { Path = "Money", StringFormat = "Кол-во валюты: {0}", });

                Label lAdmin = new Label { FontSize = 14, };
                lAdmin.SetBinding(Label.TextProperty, new Binding { Path = "Admin", StringFormat = "Администратор: {0}", });

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

                relative.Children.Add(lMoney,
                     Constraint.RelativeToView(lPrice, (parent, view) => view.X),
                     Constraint.RelativeToView(lPrice, (parent, view) => view.Y + view.Height + 2));

                relative.Children.Add(lAdmin,
                     Constraint.RelativeToView(lMoney, (parent, view) => view.X),
                     Constraint.RelativeToView(lMoney, (parent, view) => view.Y + view.Height + 2));
                #endregion

                return new ViewCell { View = new Frame { Margin = 15, Content = relative, CornerRadius = 20, BackgroundColor = Color.LightSkyBlue, HeightRequest = 70 } };
            });
            #endregion

            Content = new StackLayout { Children = { label, list, button }, };
        }

        private void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new EditAccount { BindingContext = (Account)e.Item });
        }

        private void AddBicycle(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditAccount { BindingContext = new Account() });
        }

        protected async override void OnAppearing()
        {
            list.ItemsSource = await App.DBAccount.GetItemsAsync();
        }
    }
}