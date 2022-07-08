using System;
using Xamarin.Forms.Internals;

namespace habahabamall.Models
{
    /// <summary>
    /// Model for address.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class Address
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }

        public string PostalCode { get; set; }
        public string Address1 { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
    }
}