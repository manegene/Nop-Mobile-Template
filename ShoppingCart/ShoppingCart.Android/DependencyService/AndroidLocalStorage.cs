using habahabamall.DataService;
using habahabamall.Droid.DependencyService;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidLocalStorage))]

namespace habahabamall.Droid.DependencyService
{
    public class AndroidLocalStorage : ILocalStorage
    {
        public SQLiteConnection GetConnection()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "ShoppingKart.db3");
            SQLiteConnection connection = new SQLiteConnection(path);
            return connection;
        }
    }
}