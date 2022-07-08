using habahabamall.Models;
using SQLite;
using System;
using System.Threading.Tasks;

namespace habahabamall.DataService
{
    /// <summary>
    /// This class is used to maintain the data in local.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SQLiteDatabase
    {
        private readonly SQLiteAsyncConnection database;

        /// <summary>
        /// Create and initialize the local database connection.
        /// Create table.
        /// </summary>
        public SQLiteDatabase(string dbpath)
        {
            database = new SQLiteAsyncConnection(dbpath);
            database.CreateTableAsync<UserInfo>().Wait();
            database.CreateTableAsync<UserTokenModel>().Wait();
            Console.WriteLine("tables created");
            Initialized = true;
        }

        private SQLiteConnection Connection { get; set; }

        public static bool Initialized { get; private set; }




        #region

        /// <summary>
        /// To get user logged details from local database.
        /// </summary>
        public Task<UserInfo> GetUserInfo()
        {

            Task<UserInfo> record = database.Table<UserInfo>().FirstOrDefaultAsync();
            return record;

        }
        public Task<UserTokenModel> GetUserTokenInfo()
        {

            Task<UserTokenModel> record = database.Table<UserTokenModel>().FirstOrDefaultAsync();
            return record;

        }

        /// <summary>
        /// Insert user details.
        /// </summary>
        public Task<int> ManegeUserDetail(UserInfo userInfo)
        {


            if (userInfo.ID != 0)
            {
                // Update an existing user record.
                return database.UpdateAsync(userInfo);
            }
            else
            {
                // Save a new user record.
                return database.InsertAsync(userInfo);
            }
        }

        /// <summary>
        /// Update user details.
        /// </summary>
        public Task<int> ManageUserTokenDetail(UserTokenModel userTokenInfo)
        {
            if (userTokenInfo.ID > 0)
            {
                // Update an existing token record.
                return database.UpdateAsync(userTokenInfo);
            }
            else
            {
                // Save a new token.
                return database.InsertAsync(userTokenInfo);
            }
        }

        #endregion
    }
}