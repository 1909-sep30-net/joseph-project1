using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Project1.BusinessLogic;

namespace Project1.DataAccess
{
    /// <summary>
    /// static class that contains all the mapper methods
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// convert an entity customer to an object customer
        /// </summary>
        /// <param name="customer">customer object to be converted</param>
        /// <returns>cusotmer object</returns>
        public static Customer MapCustomerToOrders(Entities.Customers customer)
        {
            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Orders = customer.Orders.Select(MapOrder).ToList(),
            };
        }

        /// <summary>
        /// convert an object customer to an entity customer
        /// </summary>
        /// <param name="customer">customer object to be converted</param>
        /// <returns>customer entity</returns>
        public static Entities.Customers MapCustomerToOrders(Customer customer)
        {
            return new Entities.Customers
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Orders = customer.Orders.Select(MapOrder).ToList(),
            };
        }

        /// <summary>
        /// converts an entity location to an object location
        /// adds in the locations inventory and order history
        /// </summary>
        /// <param name="location">location entity to be converted</param>
        /// <returns>location object</returns>
        public static Location MapLocationToInvetoryToOrders(Entities.Locations location)
        {
            return new Location
            {
                Id = location.Id,
                Name = location.Name,
                Inventory = location.ProductInventory.Select(MapProductInventory).ToList(),
                Orders = location.Orders.Select(MapOrder).ToList(),
            };

        }

        /// <summary>
        /// converts a location object to a location entity
        /// </summary>
        /// <param name="location">location object to be converted</param>
        /// <returns>location entity</returns>
        public static Entities.Locations MapLocationToInvetoryToOrders(Location location)
        {
            return new Entities.Locations
            {
                Id = location.Id,
                Name = location.Name,
                ProductInventory = location.Inventory.Select(MapProductInventory).ToList(),
                Orders = location.Orders.Select(MapOrder).ToList(),
            };
        }

        /// <summary>
        /// converts a production entery into a production object
        /// </summary>
        /// <param name="product">production entery to be converted</param>
        /// <returns>production object</returns>
        public static ProductInventory MapProductInventory(Entities.ProductInventory productInventory)
        {
            return new ProductInventory
            {
                Id = productInventory.Id,
                LocationId = productInventory.LocationId,
                Product = MapProduct(productInventory.Product),
                Quantity = productInventory.Quantity,
            };
        }

        /// <summary>
        /// converts a production entry object into a production entry entity
        /// </summary>
        /// <param name="product">production entry object to be converted</param>
        /// <returns>protuction entry entity</returns>
        public static Entities.ProductInventory MapProductInventory(ProductInventory productInventory)
        {
            return new Entities.ProductInventory
            {
                Id = productInventory.Id,
                LocationId = productInventory.LocationId,
                ProductId = productInventory.Product.Id,
                Quantity = productInventory.Quantity,
            };

        }

        /// <summary>
        /// converts an order entity into an order object
        /// </summary>
        /// <param name="order">oder entity to be converted</param>
        /// <returns>order object</returns>
        public static Order MapOrder(Entities.Orders order)
        {
            return new Order
            {
                Id = order.Id,
                LocationId = order.LocationId,
                CustomerId = order.CustomerId,
                Date = order.Date,
                ProductOrders = order.ProductOrdered.Select(MapProductOrder).ToList(),
            };
        }

        /// <summary>
        /// converts an order object into a order entity
        /// </summary>
        /// <param name="order">order object to be converted</param>
        /// <returns>oder entity</returns>
        public static Entities.Orders MapOrder(Order order)
        {
            return new Entities.Orders
            {
                Id = order.Id,
                LocationId = order.LocationId,
                CustomerId = order.CustomerId,
                Date = order.Date,
                ProductOrdered = order.ProductOrders.Select(MapProductOrder).ToList(),
            };
        }

        /// <summary>
        /// converts a prodution order entity into a production order object
        /// </summary>
        /// <param name="product">production order entity to be converted</param>
        /// <returns>production order object</returns>
        public static ProductOrder MapProductOrder(Entities.ProductOrdered productOrdered)
        {
            return new ProductOrder
            {
                Id = productOrdered.Id,
                OrderId = productOrdered.OrderId,
                Product = MapProduct(productOrdered.Product),
                Quantity = productOrdered.Quantity,
            };

        }

        /// <summary>
        /// converts a production order object into a production order entity
        /// </summary>
        /// <param name="order">product order to be converted</param>
        /// <returns>production order entity</returns>
        public static Entities.ProductOrdered MapProductOrder(BusinessLogic.ProductOrder ProductOrder)
        {
            return new Entities.ProductOrdered
            {
                Id = ProductOrder.Id,
                OrderId = ProductOrder.OrderId,
                ProductId = ProductOrder.Product.Id,
                Quantity = ProductOrder.Quantity,
            };
        }

        /// <summary>
        /// converts a product entity into a product object
        /// </summary>
        /// <param name="product">product entity to be converted</param>
        /// <returns>product object</returns>
        public static BusinessLogic.Product MapProduct(Entities.Products product)
        {
            return new BusinessLogic.Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
            };
        }

        /// <summary>
        /// converts a product object into a product entity
        /// </summary>
        /// <param name="product">product object to be converted</param>
        /// <returns>product entity</returns>
        public static Entities.Products MapProduct(BusinessLogic.Product product)
        {
            return new Entities.Products
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
