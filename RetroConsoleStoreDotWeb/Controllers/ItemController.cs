using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStoreDotWeb.Models.Articol;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Catalog()
        {
            var products = new List<Product>
            {
                 new Product {
                Id = 1,
                Name = "Nintendo NES",
                Description = "Classic 8-bit console from the 1980s",
                ImagePath = "/Content/images/nintendo-famicom.jpg",
                Price = 149.99m
            },
            new Product {
                Id = 2,
                Name = "Sega Genesis",
                Description = "16-bit console with iconic games",
                ImagePath = "/Content/images/sega-genesis.jpg",
                Price = 129.99m
            },
            new Product {
                Id = 3,
                Name = "PlayStation 1",
                Description = "Sony's groundbreaking 32-bit console",
                ImagePath = "/Content/images/playstation-1.jpg",
                Price = 179.99m
            }
            };
            return View(products);
        }

        public ActionResult Product()
        {
            return View();
        }
        public ActionResult TradeIn()
        {
            return View();
        }
    }
}