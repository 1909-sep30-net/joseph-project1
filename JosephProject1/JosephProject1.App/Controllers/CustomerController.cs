using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Project1.BusinessLogic;
using Serilog;
using JosephProject1.App.Models;

namespace JosephProject1.App.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IDataAccess _data;

        public CustomerController(IDataAccess data)
        {
            _data = data;
        }

        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = _data.GetCustomers();

            var modelView = customers.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                TotalPurchases = c.TotalPurchases,
            });

            return View(modelView);
        }

        // GET: Customer/Details/5
        public ActionResult OrdersDetails(int id)
        {
            Customer customer = _data.GetCustomers(id: id).First();

            var modelView = new CustomerViewModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                TotalPurchases = customer.TotalPurchases,
                Orders = customer.Orders.Select(o => new OrdersViewModel
                {
                    Id = o.Id,
                    LocationId = o.LocationId,
                    CustomerId = o.CustomerId,
                    Total = o.Total,
                }),
            };

            return View(modelView);
        }

        // GET: Customer/Details/5
        public ActionResult ProductDetails(int id)
        {
            Order order = _data.GetOrderById(id);

            var modelView = new OrdersViewModel
            {
                Id = order.Id,
                LocationId = order.LocationId,
                CustomerId = order.CustomerId,
                Total = order.Total,
                ProductOrders = order.ProductOrders.Select(po => new ProductOrderViewModel
                {
                    Id = po.Product.Id,
                    Name = po.Product.Name,
                    Quantity = po.Quantity,
                    Total = po.Quantity * po.Product.Price,
                }),
            };


            return View(modelView);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = new Customer
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                    };

                    _data.AddCustomer(customer);
                    _data.Save();
                }

                Log.Information("Customer {FullName} added", viewModel.FullName);
                return RedirectToAction(nameof(Index));
            }
            catch(InvalidOperationException ex)
            {
                Log.Information("Customer {FullName} added failed: {Message}", viewModel.FullName, ex.Message);
                return View(viewModel);
            }
            catch (ArgumentException ex)
            {
                Log.Information("Customer {FullName} added failed: {Message}", viewModel.FullName, ex.Message);
                if (viewModel.FirstName == null)
                    ModelState.AddModelError("FirstName", ex.Message);
                else
                    ModelState.AddModelError("LastName", ex.Message);

                return View(viewModel);
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer = _data.GetCustomerById(id);

            var viewModel = new CustomerViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
            };

            return View(viewModel);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerInfoViewModel viewModel)
        {
            try
            {
                Customer customer = _data.GetCustomerById(id);

                customer.FirstName = viewModel.FirstName;
                customer.LastName = viewModel.LastName;

                _data.UpdateCustomer(customer);
                _data.Save();

                Log.Information("Customer id {id} edited", id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Log.Information("Customer id {id} added failed", id);
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            Customer customer = _data.GetCustomerById(id);

            var viewModel = new CustomerViewModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                TotalPurchases = customer.TotalPurchases,
            };

            return View(viewModel);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CustomerViewModel viewModel)
        {
            try
            {
                _data.DeleteCustomerById(id);
                _data.Save();

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                Log.Warning("Delete customer Error: {Message}", ex.Message);
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
            catch (ArgumentException ex)
            {
                Log.Warning("Delete customer Error: {Message}", ex.Message);
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }
    }
}