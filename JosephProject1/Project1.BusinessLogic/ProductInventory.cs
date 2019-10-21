using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLogic
{
    public class ProductInventory
    {
        private int _id;                // id of the product entry
        private int _locationId;        // the id of the location this product belongs to
        private int _quantity;          // the quantity of this product in inventory
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
        /// property of the _locationId
        /// throws ArgumentException for ids less than 0
        /// </summary>
        public int LocationId
        {
            get => _locationId;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Id cannot be less than 0", nameof(value));

                _locationId = value;
            }
        }

        public Product Product { get; set; }

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
                    throw new ArgumentException("Id cannot be less than 0", nameof(value));

                _quantity = value;
            }
        }
    }
}
