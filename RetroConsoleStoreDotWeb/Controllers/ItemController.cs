using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStoreDotWeb.Models.Articol;
using RetroConsoleStoreDotDomain.Model.Product;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class ItemController : Controller
    {
        private readonly BusinessLogic _businessLogic;
        // GET: Item
        public ItemController()
        {
            _businessLogic = new BusinessLogic();
        }
        [HttpGet]
        public ActionResult Catalog()
        {  /*
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
            },   
                
          
            }; */
            
          List<ProductModelBack> products = _businessLogic.GetProductBL().GetProductModelBacks();


            List<Product> product2 = new List<Product> { };

            foreach (var item in products)
            { 
                Product product = new Product();
                product.Name = item.Name;
                product.TimeCreated = item.TimeCreated;
                product.Price = item.Price;
                product.ImagePath = item.ImagePath;
                product.Id = item.Id;  
                product.Brand = item.Brand;
                product.StockQuantity = item.StockQuantity;
                product.Status = item.Status;
                product.YearReleased = item.YearReleased;
                
                product2.Add(product);
            }



            return View(product2);
        }

        public ActionResult Product(int id)
        { /*
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
            */
           ProductModelBack prod =  _businessLogic.GetProductBL().GetProduct(id);
            var product = new Product();

                product.Id = prod.Id;
                product.Name = prod.Name;
                product.Description = prod.Description;
            if (prod.ImagePath != null)
            {
                product.ImagePath = prod.ImagePath;
            }
            else
            { 
                product.ImagePath = "/Content/images/missing-picture-page-website-design-600nw-1552421075";
            }
                product.Price = prod.Price;
                product.Brand = prod.Brand;
                product.YearReleased = prod.YearReleased;
                product.StockQuantity = prod.StockQuantity;



            return View(product);
        }
        public ActionResult TradeIn()
        {
            return View();
        }
    }
}