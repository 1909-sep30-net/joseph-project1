using System;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// the container for holding product information
    /// </summary>
    public class Product
    {
        
        private int _id;                // product ID
        private string _name;           // name of the product
        private decimal _price;   //the price for one unit of this product

        /// <summary>
        /// property of the _id field
        /// throws an ArgumentException for ids less than 0
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
        /// property of the _name field
        /// throws an ArgumentException for empty names
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (value == string.Empty)
                    throw new ArgumentException("Name cannot be empty.", nameof(value));

                _name = value;
            }
        }

        /// <summary>
        /// property of the _costPerUnit field
        /// throws an ArgumentException for a price less than 0
        /// </summary>
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0.0M)
                    throw new ArgumentException("Price cannot be less than 0", nameof(value));

                _price = value;
            }
        }
    }
}
