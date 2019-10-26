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
        /// adds a new Customer to the data base
        /// </summary>
        /// <param name="customer">Customer object to add to the database</param>
        public void AddCustomer(Customer customer)
        {
            IEnumerable<Customer> c = GetCustomers(firstName: customer.FirstName, lastName: customer.LastName);
            if (c.Count() != 0)
            {
                throw new ArgumentException("Customer allready exists in database");
            }

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
                        .ThenInclude(p => p.Product).AsNoTracking();

            if (firstName != null)
                items = items.Where(c => c.FirstName == firstName);
            if (lastName != null)
                items = items.Where(c => c.LastName == lastName);
            if (id != null)
                items = items.Where(c => c.Id == id);

            return items.Select(Mapper.MapCustomerToOrders);
        }

        public Customer GetCustomerById(int id)
        {
            Customers entity = _context.Customers
                .Include(o => o.Orders)
                    .ThenInclude(po => po.ProductOrdered)
                        .ThenInclude(p => p.Product).AsNoTracking()
                .First(c => c.Id == id);

            return Mapper.MapCustomerToOrders(entity);
        }

        /// <summary>
        /// updates a customer and order history in the database
        /// </summary>
        /// <param name="customer">customer object to update the database with</param>
        public void UpdateCustomer(Customer customer)
        {
            Customers currentEntity = _context.Customers
                .Include(o => o.Orders)
                    .ThenInclude(po => po.ProductOrdered)
                .First(c => c.Id == customer.Id);

            Customers newEntity = Mapper.MapCustomerToOrders(customer);

            Log.Information("Updated cutomer {FirstName} {LastName}", customer.FirstName, customer.LastName);
            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// delete a customer from the database
        /// </summary>
        /// <param name="customer">customer to be deleted</param>
        public void DeleteCustomerById(int id)
        {
            Customers entity = _context.Customers
                .Include(o => o.Orders)
                    .ThenInclude(po => po.ProductOrdered)
                .First(c => c.Id == id);

            Log.Information("Removing customer with Id: {id}", id);
            _context.Remove(entity);
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

            Locations entity = Mapper.MapLocationToInvetoryToOrders(location);
            _context.Add(entity);
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
                .Include(pi => pi.ProductInventory)
                    .ThenInclude(pe => pe.Product)
                .Include(o => o.Orders)
                    .ThenInclude(po => po.ProductOrdered)
                        .ThenInclude(p => p.Product).AsNoTracking();

            if (name != null)
                items = items.Where(n => n.Name == name);
            if (id != null)
                items = items.Where(i => i.Id == id);

            return items.Select(Mapper.MapLocationToInvetoryToOrders);
        }

        public Location GetLocationById(int id)
        {
            Locations entity = _context.Locations
                .Include(pi => pi.ProductInventory)
                    .ThenInclude(p => p.Product)
                .Include(o =>o.Orders)
                    .ThenInclude(po => po.ProductOrdered)
                        .ThenInclude(p => p.Product).AsNoTracking()
                 .First(l => l.Id == id);

            return Mapper.MapLocationToInvetoryToOrders(entity);
        }

        /// <summary>
        /// updates a customer, inventory, and order history in the database
        /// </summary>
        /// <param name="location">location object to update the database with</param>
        public void UpdateLocation(Location location)
        {
            Locations currentEntity = _context.Locations
                .Include(pi => pi.ProductInventory)
                    .ThenInclude(p => p.Product)
                .Include(o => o.Orders)
                    .ThenInclude(po => po.ProductOrdered)
                .First(l => l.Id == location.Id);

            Locations newEntity = Mapper.MapLocationToInvetoryToOrders(location);

            Log.Information("Updated location {Name}", location.Name);
            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// delete a location from the database
        /// </summary>
        /// <param name="location">location to be deleted</param>
        public void DeleteLocationById(int id)
        {
            Locations entity = _context.Locations
                .Include(pi => pi.ProductInventory)
                .Include(o => o.Orders)
                    .ThenInclude(po => po.ProductOrdered)
                .First(l => l.Id == id);

            Log.Information("Removing location with id: {id}", id);
            _context.Remove(entity);
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

        /// <summary>
        /// gets all the products in the database
        /// can be used to search by product name and id
        /// </summary>
        /// <param name="name">name of the product to search for</param>
        /// <param name="id">id of the product to search for</param>
        /// <returns>list of products</returns>
        public IEnumerable<Product> GetProducts(string name = null, int? id = null)
        {
            IQueryable<Products> items = _context.Products.Include(p => p);

            if (name != null)
                items = items.Where(p => p.Name == name);
            if (id != null)
                items = items.Where(p => p.Id == id);

            return items.Select(Mapper.MapProduct);
        }

        public Product GetProductById(int id)
        {
            return Mapper.MapProduct(_context.Products.Find(id));
        }

        public void UpdateProduct(Product product)
        {
            Products currentEntity = _context.Products.Find(product.Id);

            Products newEntity = Mapper.MapProduct(product);

            Log.Information("Updated product {Name} {Price}", product.Name, product.Price);
            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        public void DeleteProductById(int id)
        {
            Products entity = _context.Products.Find(id);

            _context.Remove(entity);
        }

        public Order GetOrderById(int id)
        {
            Orders entity = _context.Orders
                .Include(po => po.ProductOrdered)
                    .ThenInclude(p => p.Product)
                .First(o => o.Id == id);

            return Mapper.MapOrder(entity);
        }

        public void UpdateProductInventory(BusinessLogic.ProductInventory productInventory)
        {
            Entities.ProductInventory pi = Mapper.MapProductInventory(productInventory);

            _context.Update(pi);
        }

        public BusinessLogic.ProductInventory GetProductInventoryById(int id)
        {
            Entities.ProductInventory entity = _context.ProductInventory
                .Include(p => p.Product).First(pi => pi.Id == id);

            return Mapper.MapProductInventory(entity);
        }

        public void AddOrder(Order order)
        {

            if (order.Id != 0)
            {
                Log.Information("Added order id {id} to database allreay exists", order.Id);
                throw new ArgumentException("order allready exists in database");
            }
            else
            {
                Orders entity = Mapper.MapOrder(order);

                _context.Add(entity);
            }
        }

        /// <summary>
        /// save all changes made to the database
        /// </summary>
        public void Save()
        {
            Log.Information("Saving changes to database");
            _context.SaveChanges();
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
