using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaNick
{
    public partial class App : Application
    {
        static string pathBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CinemaNick.db"); // ссылка на БД
        static SQLiteAsyncConnection database; //  база данных

        static AccountDB dbAccount; // обращение к каждой таблице
        static MovieDB dbMovie;
        static BuyMovieDB dbBuyMovie;
        public App()
        {
            Task.Run(async () =>
            {
                database = new SQLiteAsyncConnection(pathBD); // подключается и не создается если существует
                await database.CreateTableAsync<Account>(); // не создается если существует
                await database.CreateTableAsync<Movie>();
                await database.CreateTableAsync<BuyMovie>();
                var startDBAccount = DBAccount; // для инициализации начальных данных
                var startDBMovie = DBMovie;
            }).Wait();
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        public static AccountDB DBAccount
        {
            get
            {
                if (dbAccount == null)
                {
                    dbAccount = new AccountDB(database);
                }
                return dbAccount;
            }
        }
        public static MovieDB DBMovie
        {
            get
            {
                if (dbMovie == null)
                {
                    dbMovie = new MovieDB(database);
                }
                return dbMovie;
            }
        }
        public static BuyMovieDB DBBuyMovie
        {
            get
            {
                if (dbBuyMovie == null)
                {
                    dbBuyMovie = new BuyMovieDB(database);
                }
                return dbBuyMovie;
            }
        }
    }
}
