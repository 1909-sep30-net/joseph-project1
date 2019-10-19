using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// container for holding an order made
    /// </summary>
    public class Order
    {
        private int _id;            // the id of the order
        private int _locationId;    // location this order was placed at
        private int _customerId;    // customer id for this order

        /// <summary>
        /// property of the _id field
        /// throws ArgumentEception for id less than 0
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                    throw new ArgumentException("ID cannot be less than 0", nameof(value));

                _id = value;
            }
        }

        /// <summary>
        /// property of the _locationId
        /// throws ArgumentException for ids less than 0
        /// </summary>
        public int LocationId
        {
            get => _locationId;
            set
            {
                if (value < 0)
                    throw new ArgumentException("ID cannot be < 0", nameof(value));

                _locationId = value;
            }
        }

        /// <summary>
        /// property of the _customerId field
        /// throws ArgumentException for ids less than 0
        /// </summary>
        public int CustomerId
        {
            get => _customerId;
            set
            {
                if (value < 0)
                    throw new ArgumentException("ID cannot be < 0", nameof(value));

                _customerId = value;
            }
        }

        /// <summary>
        /// the date and time the order was placed at a location
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// list of all products in the order
        /// </summary>
        public List<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

        /// <summary>
        /// generates the total price of the order based on the products orders
        /// </summary>
        public decimal Total
        {
            get
            {
                if (ProductOrders.Count > 0)
                {
                    return ProductOrders.Sum(p => p.Price);
                }

                return 0.00M;
            }
        }

        /// <summary>
        /// adds a new product order to the order
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(ProductOrder product)
        {
            int index = ProductOrders.IndexOf(product);
            if (index < 0)
            {
                ProductOrders.Add(product);
            }
            else
            {
                ProductOrders[index].Quantity += product.Quantity;
            }
        }
    }
}
