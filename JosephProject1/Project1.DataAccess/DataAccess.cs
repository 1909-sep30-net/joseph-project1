using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Serilog;

using Project1.DataAccess.Entities;
using Project1.BusinessLogic;

namespace Project1.DataAccess
{
    public class DataAccess : IDataAccess, IDisposable
    {
        /// <summary>
        /// Dbcontext for accessing the database
        /// </summary>
        private readonly Project0Context _context;

        /// <summary>
        /// constructor for the project0 database access
        /// </summary>
        /// <param name="context">Dbcontext for accessing the database</param>
        public DataAccess(Project0Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// save all changes made to the database
        /// </summary>
        public void Save()
        {
            Log.Information("Saving changes to database");
            _context.SaveChanges();
        }

        /// <summary>
        /// gets a list of all customers in the database
        /// includes order history
        /// can be used to search by cusotmer first name, last name, and id
        /// </summary>
        /// <param name="firstName">first name of customer to search for</param>
        /// <param name="lastName">last name of customer to search for</param>
        /// <param name="id">id of the customer to search for</param>
        /// <returns>a list of all Cusotmer objects found</returns>
        public IEnumerable<Customer> GetCustomers(string firstName = null, string lastName = null, int? id = null)
        {
            IQueryable<Customers> items = _context.Customers
                .Include(r => r.Orders)
                .ThenInclude(po => po.ProductOrdered)
                .ThenInclude(p => p.Product);

            if (firstName != null)
                items = items.Where(c => c.FirstName == firstName);
            if (lastName != null)
                items = items.Where(c => c.LastName == lastName);
            if (id != null)
                items = items.Where(c => c.Id == id);

            return items.Select(Mapper.MapCustomerToOrders);
        }

        /// <summary>
        /// gets a list of all locations in the database
        /// each location includes invintory and order history
        /// can be used to search by location name and id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Location> GetLocations(string name = null, int? id = null)
        {
            IQueryable<Locations> items = _context.Locations
                .Include(pe => pe.ProductInventory)
                .ThenInclude(p => p.Product)
                .Include(o => o.Orders)
                .ThenInclude(po => po.ProductOrdered)
                .ThenInclude(p => p.Product);

            if (name != null)
                items = items.Where(n => n.Name == name);
            if (id != null)
                items = items.Where(i => i.Id == id);

            return items.Select(Mapper.MapLocationsToInvetoryToOrders);
        }

        /// <summary>
        /// gets all the products in the database
        /// can be used to search by product name and id
        /// </summary>
        /// <param name="name">name of the product to search for</param>
        /// <param name="id">id of the product to search for</param>
        /// <returns>list of products</returns>
        public IEnumerable<Product> GetProducts(string name = null, int? id = null)
        {
            IQueryable<Products> items = _context.Products.Include(r => r);

            if (name != null)
                items = items.Where(p => p.Name == name);
            if (id != null)
                items = items.Where(p => p.Id == id);

            return items.Select(Mapper.MapProduct);
        }

        /// <summary>
        /// adds a new Customer to the data base
        /// </summary>
        /// <param name="customer">Customer object to add to the database</param>
        public void AddCustomer(Customer customer)
        {
            if (customer.Id != 0)
            {
                Log.Warning("Customer allready exist in database allreay exists", customer.Id);
                throw new ArgumentException("Customer allready exists in database");
            }
            else
            {
                Customers entity = Mapper.MapCustomerToOrders(customer);
                _context.Add(entity);

                Log.Information("Added {FirstName} {LastName} to database", customer.FirstName, customer.LastName);
            }
        }

        /// <summary>
        /// adds a new location to the database
        /// </summary>
        /// <param name="location">Location object to add to the database</param>
        public void AddLocation(Location location)
        {
            if (location.Id != 0)
            {
                Log.Information("Added {Name} to database allreay exists", location.Name);
                throw new ArgumentException("Location allready exists in database");
            }

            Locations entity = Mapper.MapLocationsToInvetoryToOrders(location);
            _context.Add(entity);
        }

        /// <summary>
        /// adds a new product to the database
        /// </summary>
        /// <param name="product">product object to add to the database</param>
        public void AddProduct(Product product)
        {
            if (product.Id != 0)
            {
                Log.Information("Added {Name} to database allreay exists", product.Name);
                throw new ArgumentException("Location allready exists in database");
            }

            Products entity = Mapper.MapProduct(product);
            _context.Add(entity);
        }

        public void AddProductInventory(BusinessLogic.ProductInventory productInventory)
        {
            Entities.ProductInventory entity = Mapper.MapProductInventory(productInventory);
            _context.Add(entity);
        }

        /// <summary>
        /// adds a new order and productOrders to the database
        /// </summary>
        /// <param name="order">order object to add to the database</param>
        public void AddOrder(Order order)
        {
            Orders entity = Mapper.MapOrder(order);

            _context.Add(entity);
        }

        /// <summary>
        /// updates a productOrder in th database
        /// </summary>
        /// <param name="productOrder">product order to update</param>
        public void AddProductOrder(ProductOrder productOrder)
        {
            Entities.ProductOrdered entity = new Entities.ProductOrdered()
            {
                Id = 0,
                OrderId = productOrder.OrderId,
                ProductId = productOrder.Product.Id,
                Quantity = productOrder.Quantity,
            };

            _context.Add(entity);
        }

        

        /// <summary>
        /// updates a customer and order history in the database
        /// </summary>
        /// <param name="customer">customer object to update the database with</param>
        public void UpdateCustomer(Customer customer)
        {
            Customers currentEntity = _context.Customers.Find(customer.Id);
            Customers newEntity = Mapper.MapCustomerToOrders(customer);

            Log.Information("Updated cutomer {FirstName} {LastName}", customer.FirstName, customer.LastName);
            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// updates a customer, inventory, and order history in the database
        /// </summary>
        /// <param name="location">location object to update the database with</param>
        public void UpdateLocation(Location location)
        {
            Locations currentEntity = _context.Locations.Find(location.Id);
            Locations newEntity = Mapper.MapLocationsToInvetoryToOrders(location);

            Log.Information("Updated location {Name}", location.Name);
            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productEntery"></param>
        public void UpdateProductInventory(BusinessLogic.ProductInventory productInventory)
        {
            Entities.ProductInventory currentEntity = _context.ProductInventory.Find(productInventory.Id);
            Entities.ProductInventory newEntity = Mapper.MapProductInventory(productInventory);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// delete a customer from the database
        /// </summary>
        /// <param name="customer">customer to be deleted</param>
        public void DeleteCustomer(Customer customer)
        {
            Log.Information("Removing customer {FirstName} {LastName}", customer.FirstName, customer.LastName);

            DeleteOrders(customer.Orders);

            Customers entity = _context.Customers.Find(customer.Id);
            _context.Remove(entity);
        }

        /// <summary>
        /// delete a location from the database
        /// </summary>
        /// <param name="location">location to be deleted</param>
        public void DeleteLocation(Location location)
        {
            Log.Information("Removing location {Name}", location.Name);

            foreach (BusinessLogic.ProductInventory pi in location.Inventory)
                DeleteProductInventory(pi);

            DeleteOrders(location.Orders);

            Locations entity = _context.Locations.Find(location.Id);
            _context.Remove(entity);
        }

        /// <summary>
        /// delete a product entry in the database
        /// </summary>
        /// <param name="locationId">product order to be deleted</param>
        public void DeleteProductInventory(BusinessLogic.ProductInventory productInventory)
        {
            Entities.ProductInventory entity = _context.ProductInventory.Find(productInventory.Id);
            _context.Remove(entity);
        }

        /// <summary>
        /// delete an order from the database
        /// </summary>
        /// <param name="orders">order to be deleted</param>
        public void DeleteOrders(IEnumerable<Order> orders)
        {
            Log.Information("Removing all Orders");

            foreach (Order o in orders)
                DeleteOrder(o);
        }

        public void DeleteOrder(BusinessLogic.Order order)
        {
            foreach (ProductOrder po in order.ProductOrders)
                DeleteProductOrder(po);

            Orders entity = _context.Orders.Find(order.Id);
            _context.Remove(entity);
        }

        /// <summary>
        /// delete a product order from the database
        /// </summary>
        /// <param name="orderId">id of the product order to delete</param>
        public void DeleteProductOrder(ProductOrder productOrder)
        {
            Log.Information("Removing all ProductOrders with order Id {Id}", productOrder.Id);

            ProductOrdered entity = _context.ProductOrdered.Find(productOrder.Id);
            _context.Remove(entity);
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
