using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStoreDotWeb.Models.Articol;
using RetroConsoleStoreDotDomain.Model.Product;
using System.Web.UI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class ItemController : BaseController
    {
        private readonly BusinessLogic businessLogic;
        private readonly IProductBL _product;
        // GET: Item
        public ItemController()
        {
            businessLogic = new BusinessLogic();
            _product = businessLogic.GetProductBL();
        }
        [HttpGet]
        public ActionResult Catalog()
        {  
            
          List<ProductModelBack> products = _product.GetProductModelBacks();


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

                if (product.ImagePath == null)
                {
                    product.ImagePath = "/content/images/Products/missing-picture-page-website-design-600nw-1552421075.webp";
                }
                product2.Add(product);
            }



            return View(product2);
        }

        public ActionResult Product(int id)
        { 
           ProductModelBack prod =  _product.GetProduct(id);
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