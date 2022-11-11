using MoneyTracking.Models;
using MoneyTracking.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking.Data
{
    public class UserDataBase
    {
       public readonly SQLiteAsyncConnection database;

        public UserDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<RegUserTable>().Wait();
        }

        public Task<List<RegUserTable>> GetUsersAsync()
        {
            //Get all Users.
            return database.Table<RegUserTable>().ToListAsync();
        }


      


        public Task<int> SaveUserAsync(RegUserTable User)
        {
        
                // Save a new User.
                return database.InsertAsync(User);
            
        }

        public Task<int> DeleteUserAsync(RegUserTable User)
        {
            // Delete a User.
            return database.DeleteAsync(User);
        }
    }
}
