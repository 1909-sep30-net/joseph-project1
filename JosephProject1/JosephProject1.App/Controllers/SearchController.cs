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
    public class SearchController : Controller
    {
        private readonly IDataAccess _data;

        public SearchController(IDataAccess data)
        {
            _data = data;
        }

        // GET: Search
        public ActionResult Index()
        {
            var viewModel = new SearchCustomer();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SearchCustomer viewModel)
        {
            IEnumerable<Customer> customers = _data.GetCustomers(viewModel.FirstName, viewModel.LastName, viewModel.Id);

            viewModel.customers = customers.Select(c => new CustomerInfoViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
            });

            return View(viewModel);
        }

        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
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

        // GET: Search/Edit/5
        public ActionResult Search(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
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

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
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