using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CinemaNick
{
    public class MovieDB
    {
        private List<Movie> _startData = new List<Movie> // стартовые фильмы
        {
            new Movie{ Name = "Спутник", DateCreate = new DateTime(2017,1,1),Price = 150, ImageLink = Device.RuntimePlatform == Device.Android ? "film1.jpg" : "Images/film1.jpg"},
            new Movie{ Name = "Эспен", DateCreate = new DateTime(2017,2,1),Price = 200, ImageLink = Device.RuntimePlatform == Device.Android ? "film2.jpg" : "Images/film2.jpg" },
            new Movie{ Name = "Холоп", DateCreate = new DateTime(2017,3,1),Price = 250, ImageLink = Device.RuntimePlatform == Device.Android ? "film3.jpg" : "Images/film3.jpg" },
            new Movie{ Name = "Джуманджи", DateCreate = new DateTime(2017,4,1) ,Price = 350, ImageLink = Device.RuntimePlatform == Device.Android ? "film4.jpg" : "Images/film4.jpg"},
            new Movie{ Name = "Волна", DateCreate = new DateTime(2017,5,1) ,Price = 450, ImageLink = Device.RuntimePlatform == Device.Android ? "film5.jpg" : "Images/film5.jpg"},
            new Movie{ Name = "Домовой", DateCreate = new DateTime(2018,1,1),Price = 125, ImageLink = Device.RuntimePlatform == Device.Android ? "film6.jpg" : "Images/film6.jpg" },
            new Movie{ Name = "Пусшки акимбо", DateCreate = new DateTime(2018,2,1),Price = 175, ImageLink = Device.RuntimePlatform == Device.Android ? "film7.jpg" : "Images/film7.jpg" },
            new Movie{ Name = "FREE GUY", DateCreate = new DateTime(2019,6,1) ,Price = 225, ImageLink = Device.RuntimePlatform == Device.Android ? "film8.jpg" : "Images/film8.jpg"},
            new Movie{ Name = "Разлом", DateCreate = new DateTime(2020,4,1) ,Price = 275, ImageLink = Device.RuntimePlatform == Device.Android ? "film9.jpg" : "Images/film9.jpg"},
            new Movie{ Name = "REDBAD", DateCreate = new DateTime(2021,5,1),Price = 999, ImageLink = Device.RuntimePlatform == Device.Android ? "film10.jpg" : "Images/film10.jpg" },
        };
        readonly SQLiteAsyncConnection _database;
        public MovieDB(SQLiteAsyncConnection database)
        {
            _database = database;

            if (_database.Table<Movie>().CountAsync().Result == 0)
            {
                IntializeData(_startData);
            }
        }
        private void IntializeData(IEnumerable<Movie> startData)
        {
            _database.InsertAllAsync(startData);
        }

        public Task<List<Movie>> GetItemsAsync()
        {
            return _database.Table<Movie>().ToListAsync();
        }
        public Task<int> SaveItemAsync(Movie item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> EditItemAsync(Movie item)
        {
            return _database.UpdateAsync(item);
        }
        public Task<int> DeleteItemAsync(Movie item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<Movie> GetItemAsync(int id)
        {
            return _database.Table<Movie>().Where(i => i.IdMovie == id).FirstOrDefaultAsync();
        }
    }
}
