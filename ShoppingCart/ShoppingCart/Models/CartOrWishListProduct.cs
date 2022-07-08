using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace habahabamall.Models
{
    /// <summary>
    /// Model for pages with product.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class CartOrWishListProduct : INotifyPropertyChanged
    {
        #region Event

        /// <summary>
        /// The declaration of property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Fields
        private bool isDiscountPositive;

        private bool isFavourite;
        private bool hasAttributes;

        private List<PreviewImage> image;
        private List<ProductAttributes> attributes;
        private List<ProductAttributes> attributeValue;

        private int totalQuantity;

        #endregion

        #region Properties

        [DataMember(Name = "id")]
        public int ID { get; set; }

        [DataMember(Name = "name")]
        public string Pname { get; set; }

        //used to share product link
        public string SeoPname { get; set; }

        [DataMember(Name = "summary")]
        public string ShortDescription { get; set; }

        [DataMember(Name = "description")] public string FullDescription { get; set; }

        [DataMember(Name = "oldprice")] public double OldPrice { get; set; }

        [DataMember(Name = "actualprice")] public double Price { get; set; }

        [DataMember(Name = "discountpercent")] public double DiscountAppliedToProducts => OldPrice - Price;

        [DataMember(Name = "ApprovedRatingSum")] public double ApprovedRatingSum { get; set; }

        public string Currency { get; set; }
        [DataMember(Name = "totalquantity")]
        public int TotalQuantity
        {
            get => totalQuantity;
            set
            {
                totalQuantity = value;
                NotifyPropertyChanged("TotalQuantity");
            }
        }

        [DataMember(Name = "subcategoryid")] public int SubCategoryId { get; set; }

        [DataMember(Name = "productReview")] public List<ProductReviewm> Reviews { get; set; }

        [DataMember(Name = "images")]
        public List<PreviewImage> Images
        {
            get => image;
            set
            {
                image = value;
                NotifyPropertyChanged(nameof(Images));
            }
        }
        //public List<PreviewImage> Images;

        public string Category { get; set; }
        public double DiscountPrice => (DiscountAppliedToProducts / 100);


        [DataMember(Name = "previewimage")]
        public string Previewmage { get; set; }

        public string Previewimage => App.BaseImageUrl + Previewmage;

        public bool IsFavourite
        {
            get => isFavourite;
            set
            {
                isFavourite = value;
                NotifyPropertyChanged("IsFavourite");
            }
        }
        public bool HasAttributes
        {
            get => hasAttributes;
            set
            {
                hasAttributes = value;
                NotifyPropertyChanged("HasAttributes");
            }
        }
        public bool IsDiscountPositive
        {
            get => isDiscountPositive;
            set
            {
                isDiscountPositive = value;
                NotifyPropertyChanged("IsDiscountPositive");
            }
        }

        public string Seller { get; set; }

        private List<object> quantities;

        public List<object> Quantities
        {
            get => quantities;
            set
            {
                quantities = value;
                NotifyPropertyChanged("Quantities");
            }
        }

        public List<string> SizeVariants { get; set; }

        //combine the Usercartentity properties here.
        //the connection with the usercartentity does not work

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the total quantity in cart/wislist
        /// </summary>
        public int Quantity { get; set; }

        public bool ShowOnHomepage { get; set; }

        //product attributes
        //to be removed..no longer used
        public List<ProductAttributes> Attributes
        {
            get => attributes;
            set
            {
                attributes = value;
                NotifyPropertyChanged("Attributes");
            }
        }

        // used and working correctly
        public List<ProductAttributes> Attributevalue
        {
            get => attributeValue;
            set
            {
                attributeValue = value;
                NotifyPropertyChanged("AttributeValue");
            }
        }

        #endregion
    }

}