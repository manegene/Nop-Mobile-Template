namespace habahabamall.Models
{
    public class FirebaseUser
    {
        public string email { get; } = "webapi@habahabamall.com";
        public string password { get; } = "test123";

        public bool returnSecureToken { get; } = true;

    }
}
