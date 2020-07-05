using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CinemaNick
{
    public class AccountDB
    {
        private List<Account> _startData = new List<Account> // стартовые аккаунты админов
        {
            new Account
            {
                Login ="Nick",
                Password = "Volk",
                Name = "Никита",
                Surname = "Волков",
                Patronymic = "Валерьевич",
                Admin = true,
                ImageLink =Device.RuntimePlatform == Device.Android ? "Shiro.jpg" : "Images/Shiro.jpg"
            },
            new Account
            {
                Login ="Admin",
                Password = "Admin",
                Name = "(Пусто)",
                Surname = "(Пусто)",
                Patronymic = "(Пусто)",
                Admin = true,
                ImageLink =Device.RuntimePlatform == Device.Android ? "image10.jpg" : "Images/image10.jpg"
            },
        };

        readonly SQLiteAsyncConnection _database;
        public AccountDB(SQLiteAsyncConnection database)
        {
            _database = database;

            if (_database.Table<Account>().CountAsync().Result == 0)
            {
                IntializeData(_startData);
            }
        }
        private void IntializeData(IEnumerable<Account> startData)
        {
            _database.InsertAllAsync(startData);
        }

        public Task<List<Account>> GetItemsAsync()
        {
            return _database.Table<Account>().ToListAsync();
        }
        public Task<int> SaveItemAsync(Account item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> EditItemAsync(Account item)
        {
            return _database.UpdateAsync(item);
        }
        public Task<int> DeleteItemAsync(Account item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<Account> GetItemAsync(int id)
        {
            return _database.Table<Account>().Where(i => i.IdAccount == id).FirstOrDefaultAsync();
        }
    }
}
