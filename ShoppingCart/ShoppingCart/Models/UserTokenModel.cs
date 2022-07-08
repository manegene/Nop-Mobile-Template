using SQLite;

namespace habahabamall.Models
{
    public class UserTokenModel
    {

        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public string IdToken { get; set; }
        public long Expiresin { get; set; }

    }
}
