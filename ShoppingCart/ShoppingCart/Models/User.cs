using Xamarin.Forms.Internals;

namespace habahabamall.Models
{
    /// <summary>
    /// Model for user.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Confirmpassword { get; set; }
        public int AddressId { get; set; }
        public string Phone { get; set; }
        public int BusinessShortCode { get; set; }
        public string Timestamp { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
        public long PartyA { get; set; }
        public int PartyB { get; set; }
        public string CallBackURL { get; set; }
        public string AccountReference { get; set; }
        public string TransactionDesc { get; set; }
    }
}