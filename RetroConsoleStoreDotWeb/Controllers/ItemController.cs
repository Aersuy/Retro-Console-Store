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

        public ActionResult Product(int id)
        {
            var product = new Product
            {
                Id = 1,
                Name = "Nintendo NES",
                Description = "The Nintendo Entertainment System (NES) is an 8-bit home video game console developed and manufactured by Nintendo. It was released in Japan in 1983 as the Family Computer (FC), and was redesigned as the NES for release in North America in 1985.",
                ImagePath = "/Content/images/nintendo-famicom.jpg",
                Price = 149.99m,
                Brand = "Nintendo",
                YearReleased = 1985,
                StockQuantity = 5
            };
            return View(product);
        }
        public ActionResult TradeIn()
        {
            return View();
        }
    }
}