using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace habahabamall.Models
{
    /// <summary>
    /// Model for user cart.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class UserCart : INotifyPropertyChanged
    {
        private CartOrWishListProduct product;

        [DataMember(Name = "id")] public int ID { get; set; }

        [DataMember(Name = "userid")] public int? CustomerId { get; set; }

        [DataMember(Name = "productid")] public int? ProductId { get; set; }

        [DataMember(Name = "totalquantity")] public int Quantity { get; set; }

        [DataMember(Name = "product")]
        public CartOrWishListProduct Product
        {
            get => product;
            set
            {
                product = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}