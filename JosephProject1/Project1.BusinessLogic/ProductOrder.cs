using System;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// handles all the logic for a production order
    /// </summary>
    public class ProductOrder
    {
        private int _id;                // id of the product order
        private int _orderId;           // the id of the oder this product belongs to
        private int _quantity;          // the quantity of this product ordered

        /// <summary>
        /// property of the _id field
        /// throws ArgumentException when name is empty
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Id cannot be less than 0", nameof(value));

                _id = value;
            }
        }

        /// <summary>
        /// property of the _orderId
        /// throws ArgumentException for ids less than 0
        /// </summary>
        public int OrderId
        {
            get => _orderId;
            set
            {
                if (value < 0)
                    throw new ArgumentException("OrderId cannot be less than 0", nameof(value));

                _orderId = value;
            }
        }

        /// <summary>
        /// property for the _quantity field
        /// throws ArgumentException for quantities less than 0
        /// </summary>
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be less than 0", nameof(value));

                _quantity = value;
            }
        }

        public Product Product { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Price
        {
            get => Product.Price * Quantity;
        }
    }
}
