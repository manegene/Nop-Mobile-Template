using SQLite;

namespace habahabamall.Models
{
    public class UserInfo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsNewUser { get; set; }
        /* public string Access_token { get; set; }
         public long Expires_in { get; set; }*/
        public string Error_description { get; set; }
    }
}