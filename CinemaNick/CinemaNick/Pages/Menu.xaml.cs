using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaNick
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // жирные кнопки и кнопку назад убрать
    public partial class Menu : ContentPage
    {
        List<Account> accounts;
        Entry loginE, pasE;
        MainPage main;
        public Menu(MainPage _main)
        {
            main = _main;
            this.BackgroundColor = Color.LightSteelBlue;
            this.BackgroundColor = Color.LightSkyBlue;

            var size30 = Device.RuntimePlatform == Device.Android ? 14 : 30;
            Image imageLogotype = new Image { Source = "CinemaNick.png", Aspect = Aspect.AspectFit, HorizontalOptions = LayoutOptions.Center };

            loginE = new Entry { Placeholder = "Введите логин", HorizontalOptions = LayoutOptions.FillAndExpand, /*Text = "Nick" данные чтобы не вводить при тестировании,*/ FontSize = size30 };

            pasE = new Entry { Placeholder = "Введите пароль", IsPassword = true, HorizontalOptions = LayoutOptions.FillAndExpand, /*Text = "Volk" данные чтобы не вводить при тестировании,*/ FontSize = size30 }; // Exception if IsPassword = true, быстро выделить и изменить

            Button button = new Button { CornerRadius = 15, Text = "Войти", BackgroundColor = Color.Red };
            button.Clicked += CheckAndPushPage;
            Button buttonReg = new Button { CornerRadius = 15, Text = "Регистрация", BackgroundColor = Color.Red };
            buttonReg.Clicked += Reg;

            #region mainRelative
            RelativeLayout mainRelative = new RelativeLayout();

            RelativeLayout relativeMenu = new RelativeLayout();

            mainRelative.Children.Add(relativeMenu,
                Constraint.RelativeToParent((parent) => parent.Width / 6),
                Constraint.RelativeToParent((parent) => parent.Height / 9),
                Constraint.RelativeToParent((parent) => parent.Width * 4 / 6),
                Constraint.RelativeToParent((parent) => parent.Height * 3 / 5));

            relativeMenu.Children.Add(imageLogotype,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.05),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.4));

            relativeMenu.Children.Add(loginE,
                Constraint.RelativeToParent((parent) => parent.Width * 0.1),
                Constraint.RelativeToView(imageLogotype, (parent, view) => view.Y + view.Height + 5),
                Constraint.RelativeToParent((parent) => parent.Width * 0.85),
                Constraint.RelativeToParent((parent) => parent.Height * 0.15));

            relativeMenu.Children.Add(pasE,
                Constraint.RelativeToParent((parent) => parent.Width * 0.1),
                Constraint.RelativeToView(loginE, (parent, view) => view.Y + view.Height + 5),
                Constraint.RelativeToView(loginE, (parent, view) => view.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.15));

            relativeMenu.Children.Add(button,
                Constraint.RelativeToParent((parent) => parent.Width * 0.1),
                Constraint.RelativeToParent((parent) => parent.Height * 0.8),
                Constraint.RelativeToView(loginE, (parent, view) => view.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.175));

            relativeMenu.Children.Add(buttonReg,
                Constraint.RelativeToView(button, (parent, view) => view.X),
                Constraint.RelativeToView(button, (parent, view) => view.Y + view.Height + 7),
                Constraint.RelativeToView(button, (parent, view) => view.Width),
                Constraint.RelativeToView(button, (parent, view) => view.Height));
            #endregion

            Content = mainRelative;

        }

        private void Reg(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PanelRegistration() { BindingContext = new Account() });
        }

        protected async override void OnAppearing()
        {
            accounts = await App.DBAccount.GetItemsAsync();
        }

        private async void CheckAndPushPage(object sende, EventArgs e)
        {
            foreach (var item in accounts.Where((itemW) => loginE.Text == itemW.Login && pasE.Text == itemW.Password))
            {
                if (item.Admin)
                {
                    main.GetAccount = item;
                    main.Master = new AdminPanelMaster(main) { Title = "Панель администратора", BindingContext = item };
                    await Navigation.PushAsync(new EditListMovie());
                    return;
                }
                else
                {
                    main.GetAccount = item;
                    main.Master = new UserPanelMaster(main) { Title = "Панель пользователя", BindingContext = item };
                    await Navigation.PushAsync(new PanelListMovie(main));
                    return;
                }
            }
            await DisplayAlert("Уведомление", "Ввели не правильно логин или пароль", "Окей");
        }
    }
}