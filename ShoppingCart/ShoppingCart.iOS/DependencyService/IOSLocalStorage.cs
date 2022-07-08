using habahabamall.DataService;
using habahabamall.iOS.DependencyService;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSLocalStorage))]

namespace habahabamall.iOS.DependencyService
{
    public class IOSLocalStorage : ILocalStorage
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "ShoppingKart.db";
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}