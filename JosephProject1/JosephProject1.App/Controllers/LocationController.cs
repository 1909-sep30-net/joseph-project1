using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Project1.BusinessLogic;
using Project1.DataAccess;
using Project1.DataAccess.Entities;

using JosephProject1.App.Models;

namespace JosephProject1.App.Controllers
{
    public class LocationController : Controller
    {
        private readonly IDataAccess _data;

        public LocationController(IDataAccess data)
        {
            _data = data;
        }

        // GET: Location
        public ActionResult Index()
        {
            IEnumerable<Location> locations = _data.GetLocations();

            var modelView = locations.Select(l => new LocationViewModel
            {
                Id = l.Id,
                Name = l.Name,
                Sales = l.Sales,
            });

            return View(modelView);
        }

        // GET: Location/Inventor Details
        public ActionResult InventoryDetails(int id)
        {
            IEnumerable<Location> locations = _data.GetLocations();
            Location location = locations.Where(l => l.Id == id).FirstOrDefault();

            var modelView = new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Sales = location.Sales,
                Inventory = location.Inventory.Select(pi => new InventoryViewModel
                {
                    Quantity = pi.Quantity,
                    Name = pi.Product.Name,
                    Price = pi.Product.Price,
                }),

            };

            return View(modelView);
        }


        // GET: Location/Inventor Details
        public ActionResult OrdersDetails(int id)
        {
            IEnumerable<Location> locations = _data.GetLocations();

            Location location = locations.Where(l => l.Id == id).FirstOrDefault();

            var modelView = new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Sales = location.Sales,
                Orders = location.Orders.Select(o => new OrdersViewModel
                {
                    Id = o.Id,
                    LocationId = o.LocationId,
                    CustomerId = o.CustomerId,
                    Total = o.Total,
                }),

            };

            return View(modelView);
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
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

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Location/Edit/5
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

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Location/Delete/5
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