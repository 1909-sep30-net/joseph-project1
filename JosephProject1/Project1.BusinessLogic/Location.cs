using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Container for holding location information
    /// </summary>
    public class Location
    {
        private int _id;        // locations ID
        private string _name;   // locations name

        /// <summary>
        /// property of the _id field
        /// throws an ArgumentEception for ids less than 0
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
        /// throws an ArgumentEception for names that are empty
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
        /// list of all products for sale at this location
        /// </summary>
        public List<ProductInventory> Inventory { get; set; } = new List<ProductInventory>();

        /// <summary>
        /// list of all orders placed at this location
        /// </summary>
        public List<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// generated the totol sales made by this location
        /// based on the oders made
        /// </summary>
        public decimal Sales
        {
            get
            {
                if (Orders?.Count > 0)
                {
                    return Orders.Sum(p => p.Total);
                }

                return 0.00M;
            }
        }

        /// <summary>
        /// validates the products befor adding placing it
        /// all products must in an order must be in the inventoy and having enough in stock
        /// </summary>
        /// <param name="order">the order to be verifide</param>
        public void ValidateOrder(Order order)
        {
            foreach (ProductOrder po in order.ProductOrders)
            {
                int index = Inventory
                    .IndexOf(Inventory.Where(p => p.Product.Id == po.Product.Id).FirstOrDefault());

                if (index < 0)
                    throw new ArgumentException($"Location {Name} does not have the product {po.Product.Name}");

                if (Inventory[index].Quantity < po.Quantity)
                    throw new ArgumentException($"Location {Name} does not have enough of product {po.Product.Name}");
            }
        }

        /// <summary>
        /// vilidates the order and places it into the locations history
        /// reduces the inventory quantitys
        /// </summary>
        /// <param name="order">order to be placed at location</param>
        public void PlaceOrder(Order order)
        {
            ValidateOrder(order);

            foreach (ProductOrder po in order.ProductOrders)
            {
                int index = Inventory.IndexOf(Inventory.Where(p => p.Product.Id == po.Product.Id).FirstOrDefault());

                Inventory[index].Quantity -= po.Quantity;
            }

            order.Date = DateTime.Now;
            Orders.Add(order);
        }

        /// <summary>
        /// adds a product to the locations inventory
        /// </summary>
        /// <param name="product">product to be add to location inventory</param>
        public void AddProduct(ProductInventory productInventory)
        {
            int index = Inventory.IndexOf(Inventory.Where(p => p.Product.Id == productInventory.Id).FirstOrDefault());
            if (index != -1)
            {
                Inventory[index].Quantity += productInventory.Quantity;
            }
            else
            {
                Inventory.Add(productInventory);
            }
        }

        public void RemoveProduct(ProductInventory productInventory)
        {
            int index = Inventory.IndexOf(Inventory.Where(p => p.Product.Id == productInventory.Id).FirstOrDefault());
            
            if (index != -1)
            {
                Inventory.RemoveAt(index);
            }
            else
            {
                throw new ArgumentException($"ProductInventory {productInventory} was not found in location {Name} inventory");
            }
        }
    }
}
