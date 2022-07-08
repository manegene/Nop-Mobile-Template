using System;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the UserCarrEntity.
    /// </summary>
    public class UserCartEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the total quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the added datetime.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted enabled  or not.
        /// </summary>
        //public bool IsDeleted { get; set; }
        public bool IsDeleted { get; set; }


        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>
        public virtual ProductEntity Product { get; set; }
    }
}