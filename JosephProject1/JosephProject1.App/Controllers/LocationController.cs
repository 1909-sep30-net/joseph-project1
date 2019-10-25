using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Project1.BusinessLogic;
using Serilog;
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
            Location location = _data.GetLocationById(id);

            var modelView = new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Sales = location.Sales,
                Inventory = location.Inventory.Select(i => new ProductInventoryViewModel
                {
                    Id = i.Id,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                }),
            };
            return View(modelView);
        }


        // GET: Location/Inventor Details
        public ActionResult OrdersDetails(int id)
        {
            Location location = _data.GetLocationById(id);

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

        // GET: Location/Inventor Details
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

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationViewModel viewModel)
        {
            try
            {
                Location location = new Location
                {
                    Name = viewModel.Name,
                };

                Log.Information("Added a new location {Name}", location.Name);
                _data.AddLocation(location);
                _data.Save();

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                Log.Warning("Adding new location {Name} failed: {Message}", viewModel.Name, ex.Message);
                ViewBag.Error = "Error: " + ex.Message;
                return View(viewModel);
            }
            catch (ArgumentException ex)
            {
                Log.Warning("Adding new location {Name} failed: {Message}", viewModel.Name, ex.Message);
                ViewBag.Error = "Error: Location name can not be empty";
                ModelState.AddModelError("FirstName", ex.Message);

                return View(viewModel);
            }
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            Location location = _data.GetLocationById(id);

            var viewModel = new LocationViewModel
            {
                Name = location.Name,
            };

            return View(viewModel);
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Location location = _data.GetLocationById(id);

                location.Name = collection["Name"];
                _data.UpdateLocation(location);
                _data.Save();

                Log.Information("Location {id} edited", id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Log.Information("Location {id} edited failed", id);
                return View();
            }
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            Location location = _data.GetLocationById(id);

            var viewModel = new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Sales = location.Sales,
            };

            return View(viewModel);
        }

        // POST: Location/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(LocationViewModel viewModel)
        {
            try
            {
                _data.DeleteLocationById(viewModel.Id);
                _data.Save();

                Log.Information("Location for id {id} deleted", viewModel.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                Log.Information("Location for id {id} deleted failed {Message}", viewModel.Id, ex.Message);
                ViewBag.Error = "Error: " + ex.Message;
                return View(viewModel);
            }
            catch (ArgumentException ex)
            {
                Log.Information("Location for id {id} deleted failed", viewModel.Id);
                ViewBag.Error = "Error: " + ex.Message;
                return View(viewModel);
            }
        }

        // GET: Product/Create
        public ActionResult PlaceOrder(int id)
        {
            Location location = _data.GetLocationById(id);
            IEnumerable<Customer> customers = _data.GetCustomers();

            var viewModel = new OrderBasketViewModel
            {
                LocationId = location.Id,
            };

            foreach (var c in customers)
            {
                viewModel.CustomersInfo.Add(new CustomerInfoViewModel {Id = c.Id, FirstName = c.FirstName, LastName = c.LastName});
            }

            foreach (var pi in location.Inventory)
            {
                viewModel.OrderInfo.Add(new ProductInventoryViewModel
                {
                    Id = pi.Id,
                    ProductId = pi.Product.Id,
                    Name = pi.Product.Name,
                    Quantity = 0,
                    Price = pi.Product.Price,
                    MaxQuantity = pi.Quantity});
            }

            return View(viewModel);
    }

    // POST: Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult PlaceOrder(OrderBasketViewModel viewModel)
    {
            Location location = _data.GetLocationById(viewModel.LocationId);
            Customer customer = _data.GetCustomerById(viewModel.CustomerId);

            IEnumerable<Customer> customers = _data.GetCustomers();

            foreach (var c in customers)
            {
                viewModel.CustomersInfo.Add(new CustomerInfoViewModel { Id = c.Id, FirstName = c.FirstName, LastName = c.LastName });
            }

            try
        {
                Order order = new Order
                {
                    Id = 0,
                    LocationId = location.Id,
                    CustomerId = customer.Id,
                };

                List<ProductOrder> productOrders = new List<ProductOrder>();
                foreach (var item in viewModel.OrderInfo)
                {
                    if (item.Quantity > 0)
                    {
                        order.AddProduct(new ProductOrder
                        {
                            Id = 0,
                            OrderId = 0,
                            Quantity = item.Quantity,
                            Product = new Product
                            {
                                Id = item.ProductId,
                                Name = item.Name,
                                Price = item.Price,
                            },
                        }); 
                    }
                }

                location.PlaceOrder(order);
                customer.Orders.Add(order);

                _data.AddOrder(order);
                _data.Save();

                foreach (var item in location.Inventory)
                {
                    _data.UpdateProductInventory(item);
                }


                _data.UpdateLocation(location);
                _data.UpdateCustomer(customer);
                _data.Save();
            
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            Log.Information("order placed failed {Message}", ex.Message);
            ViewBag.Error = "Error: " + ex.Message;
            return View(viewModel);
        }
        catch (ArgumentException ex)
        {
            Log.Information("order placed failed {Message}", ex.Message);
            ViewBag.Error = "Error: " + ex.Message;
            return View(viewModel);
        }
        catch
        {
            Log.Information("order placed failed");
            return View(viewModel);
        }
    }
}
}