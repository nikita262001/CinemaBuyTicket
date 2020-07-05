using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNick
{
    public class BuyMovieDB
    {
        readonly SQLiteAsyncConnection _database;
        public BuyMovieDB(SQLiteAsyncConnection database)
        {
            _database = database;
        }

        public Task<int> SaveItemsAsync(IEnumerable<BuyMovie> listDate)
        {
            return _database.InsertAllAsync(listDate);
        }

        public Task<List<BuyMovie>> GetItemsAsync()
        {
            return _database.Table<BuyMovie>().ToListAsync();
        }
        public Task<int> SaveItemAsync(BuyMovie item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> EditItemAsync(BuyMovie item)
        {
            return _database.UpdateAsync(item);
        }
        public Task<int> DeleteItemAsync(BuyMovie item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<BuyMovie> GetItemAsync(int id)
        {
            return _database.Table<BuyMovie>().Where(i => i.IdBuyMovie == id).FirstOrDefaultAsync();
        }
    }
}
