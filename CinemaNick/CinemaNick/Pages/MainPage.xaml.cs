using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaNick
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Account account = new Account();
        List<Movie> movies = new List<Movie>();
        public MainPage()
        {
            Master = new AboutTheCreator() { Title = "О создателе" };
            Detail = new NavigationPage(new Menu(this));

            MasterBehavior = MasterBehavior.Popover;
        }
        public Account GetAccount { get => account; set => account = value; }
        public List<Movie> GetBasketMovies { get => movies; set => movies = value; }
    }
}