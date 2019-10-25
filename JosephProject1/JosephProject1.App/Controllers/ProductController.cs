﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Project1.BusinessLogic;
using Serilog;

using JosephProject1.App.Models;

namespace JosephProject1.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDataAccess _data;

        public ProductController(IDataAccess data)
        {
            _data = data;
        }

        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<Product> products = _data.GetProducts();

            var modelView = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
            });

            return View(modelView);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel viewModel)
        {
            try
            {
                Product product = new Product
                {
                    Id = 0,
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                };

                _data.AddProduct(product);
                _data.Save();

                Log.Information("Product {Name} added", viewModel.Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Log.Warning("Product {Name} failed", viewModel.Name);
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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