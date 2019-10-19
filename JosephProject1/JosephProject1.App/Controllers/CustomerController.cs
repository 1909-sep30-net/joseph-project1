using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Project1.BusinessLogic;
using Project1.DataAccess;
using Project1.DataAccess.Entities;

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
            IEnumerable<Customer> customers = _data.GetCustomers();

            Customer customer = customers.Where(c => c.Id == id).FirstOrDefault();

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
            IEnumerable<Customer> customers = _data.GetCustomers();

            Customer customer = customers.Where(c => c.Id == id).FirstOrDefault();
            var modelView = new CustomerViewModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Orders = customer.Orders.Select(o => new OrdersViewModel
                {
                    Id = o.Id,
                    LocationId = o.LocationId,
                    CustomerId = o.CustomerId,
                    Total = o.Total,
                    ProductOrders = o.ProductOrders.Select(po => new ProductOrderViewModel
                    {
                        Id = po.Id,
                        Name = po.Product.Name,
                        Quantity = po.Quantity,
                    }),
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}