using MoneyTracking.Models;
using MoneyTracking.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking.Data
{
    public class DataBase
    {
       public readonly SQLiteAsyncConnection database;

        public DataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<RegUserTable>().Wait();
            database.CreateTableAsync<SpendingTable>().Wait();
        }

        public Task<List<RegUserTable>> GetUsersAsync()
        {
            //Get all Users.
            return database.Table<RegUserTable>().ToListAsync();
        }
            

        public Task<int> InsertUserAsync(RegUserTable User)
        {
        
                // Save a new User.
                return database.InsertAsync(User);
            
        }
        public Task<List<SpendingTable>> GetSpendingAsync()
        {
            //Get all Users.
            return database.Table<SpendingTable>().ToListAsync();
        }

        public Task<int> InsertSpendingAsync(SpendingTable Spending)
        {

            // Save a new User.
            return database.InsertAsync(Spending);

        }


        public Task<int> DeleteUserAsync(RegUserTable User)
        {
            // Delete a User.
            return database.DeleteAsync(User);
        }
    }
}
