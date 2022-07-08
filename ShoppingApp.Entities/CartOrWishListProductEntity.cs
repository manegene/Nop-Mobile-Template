using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the ProductEntity.
    /// </summary>
    public class CartOrWishListProductEntity
    {
        /// <summary>
        /// Initializes the ProductEntity.
        /// </summary>
        public CartOrWishListProductEntity()
        {
            Reviews = new List<ReviewEntity>();
            Images = new List<PreviewImageEntity>();
            Attributes = new List<ProductAttributesEntity>();
            Attributevalue = new List<ProductAttributesEntity>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonPropertyName("id")] public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Pname { get; set; }
        public string SeoPname { get; set; }
        public string Previewmage { get; set; }


        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// Gets or sets the actual price.
        /// </summary>
        public double Price { get; set; }

        public double OldPrice { get; set; }
        /// <summary>
        /// Gets or sets the discount price.
        /// </summary>
        public double DiscountPercent { get; set; }

        /// <summary>
        /// Gets or sets the overall rating.
        /// </summary>
        public double ApprovedRatingSum { get; set; }

        /// <summary>
        /// Gets or sets the total quantity.
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// Gets or sets the subcategory id.
        /// </summary>
        public int SubCategoryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted enabled or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        public string Currency { get; set; }


        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets the subcategory.
        /// </summary>
        public virtual SubCategoryEntity SubCategory { get; set; }

        /// <summary>
        /// Gets or sets the reviews..
        /// </summary>
        public virtual ICollection<ReviewEntity> Reviews { get; set; }

        /// <summary>
        /// Gets or sets the preview images.
        /// </summary>
        public virtual ICollection<PreviewImageEntity> Images { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the total quantity.
        /// </summary>
        public int Quantity { get; set; }

        public bool ShowOnHomepage { get; set; }

        //load product attributes
        public virtual ICollection<ProductAttributesEntity> Attributes { get; set; }

        public virtual ICollection<ProductAttributesEntity> Attributevalue { get; set; }

    }
}