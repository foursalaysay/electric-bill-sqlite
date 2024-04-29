
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class Database
    {
        SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Details>().Wait();
        }

        public Task<List<Details>> GetDetailsAsync()
        {
            return _database.Table<Details>().ToListAsync();
        }

        public Task<int> SaveDetailsAsync(Details details)
        {

            return _database.InsertAsync(details);
        }

        public Task<int> UpdateDetailsAsync(Details details)
        {
            return _database.UpdateAsync(details);
        }

        public Task<int> DeleteDetailsAsync(Details details)
        {
            return _database.DeleteAsync(details);
        }

        public Task<Details> GetDetailAsync(int meterN)
        {
            return _database.Table<Details>().Where(i => i.MeterNo == meterN).FirstOrDefaultAsync();
        }
    }
}