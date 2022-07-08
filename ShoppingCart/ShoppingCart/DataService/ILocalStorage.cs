using SQLite;

namespace habahabamall.DataService
{
    public interface ILocalStorage
    {
        SQLiteConnection GetConnection();
    }
}