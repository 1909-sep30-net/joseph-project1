using System;
using Xunit;
using System.Collections.Generic;
using System.Text;

using Project1.BusinessLogic;

namespace Project1.Test
{
    public class LocationTest
    {
        private readonly Location location = new Location();

        private readonly ProductInventory productInventory = new ProductInventory
        {
            Id = 1,
            LocationId = 1,
            Quantity = 1,
            Product = new Product
            {
                Id = 1,
                Name = "Product Name",
                Price = 1.00M,
            }
        };

        private readonly ProductOrder productOrder = new ProductOrder
        {
            Id = 1,
            OrderId = 1,
            Quantity = 1,
            Product = new Product
            {
                Id = 1,
                Name = "Product Name",
                Price = 1.00M,
            }
        };


        [Fact]
        public void Id_Less_Than_0_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => location.Id = -1);
        }

        [Fact]
        public void Id_Returns_Correctly()
        {
            int id = 1;
            location.Id = id;

            Assert.Equal(id, location.Id);
        }

        [Fact]
        public void Name_Empty_Throws_ArgumentException()
        {
            Assert.ThrowsAny<ArgumentException>(() => location.Name = string.Empty);
        }

        [Fact]
        public void Name_Returns_Correctly()
        {
            string name = "store name";
            location.Name = name;

            Assert.Equal(name, location.Name);
        }

        [Fact]
        public void ValidateOrder_Invalid_Order_Throws_ArgumentException()
        {
            Order order = new Order();
            order.AddProduct(productOrder);

            location.PlaceOrder(order);

            Assert.Throws<ArgumentException>(() => location.ValidateOrder(order));
        }

        [Fact]
        public void PlaceOrder_Invalid_Order_Throws_ArgumentException()
        {
            Order order = new Order();
            order.AddProduct(productOrder);

            location.AddProduct(productInventory);
            location.PlaceOrder(order);

            Assert.Throws<ArgumentException>(() => location.PlaceOrder(order));
        }
    }
}
