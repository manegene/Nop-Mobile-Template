using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace habahabamall.Models
{
    /// <summary>
    /// Model for review list.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class ProductReviewm
    {
        [DataMember(Name = "id")] public int ID { get; set; }

        // [DataMember(Name = "profileimage")]
        /* public string ProfileImage
         {
             get => App.BaseImageUrl + "logo.png";
             set => profileImage = value;
         }*/
        //temporary assifned to title. will be updated to correct customer name field
        [DataMember(Name = "Title")] public string Title { get; set; }

        [DataMember(Name = "createdOnUtc")]
        /* public string Date
         {
             get => CreatedOnUtc;
             set
             {
                 CreatedOnUtc = value;
                 if (!string.IsNullOrEmpty(CreatedOnUtc)) reviewDate = DateTime.Parse(CreatedOnUtc);
             }
         }*/
        public int? CustomerId { get; set; }


        public DateTime CreatedOnUtc { get; set; }

        public int ProductId { get; set; }

        //  public string StringDate { get; set; }

        [DataMember(Name = "rating")] public int Rating { get; set; }

        [DataMember(Name = "reviewText")] public string ReviewText { get; set; }

        [DataMember(Name = "reviewimages")] public List<ReviewImage> ReviewImages { get; set; }

        #region Field

        // private string profileImage;

        //private DateTime reviewDate;

        #endregion
    }
}