using System;
using Xunit;
using Project1.BusinessLogic;

namespace Project1.Test
{
    public class CustomerTest
    {
        private readonly Customer customer = new Customer()
        {
            Id = 1,
            FirstName = "firstName",
            LastName = "lastName",
        };

        // ------ test Id ------
        [Fact]
        public void Id_Less_Than_0_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => customer.Id = -1);
        }

        [Fact]
        public void Id_Returns_Correctly()
        {
            int id = 1;
            customer.Id = id;

            Assert.Equal(id, customer.Id);
        }

        // ------ test First Name ------
        [Fact]
        public void First_Name_Empty_Throws_ArgumentException()
        {
            Assert.ThrowsAny<ArgumentException>(() => customer.FirstName = string.Empty);
        }

        [Fact]
        public void First_Name_Property_Returns_Corectly()
        {
            string firstName = "Joseph";
            customer.FirstName = firstName;

            Assert.Equal(firstName, customer.FirstName);
        }

        // ------ test Last Name ------
        [Fact]
        public void Last_Name_Empty_Throws_ArgumentException()
        {
            Assert.ThrowsAny<ArgumentException>(() => customer.LastName = string.Empty);
        }

        [Fact]
        public void Last_Name_Property_Returns_Corectly()
        {
            string lastName = "Mohrbacher";
            customer.LastName = lastName;

            Assert.Equal(lastName, customer.LastName);
        }
    }
}
