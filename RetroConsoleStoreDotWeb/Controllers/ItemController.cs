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
using RetroConsoleStoreDotBusinessLogic.Attributes;
using Microsoft.Ajax.Utilities;
using RetroConsoleStoreDotWeb.ViewModel;

namespace RetroConsoleStoreDotWeb.Controllers
{
    [User]
    public class ItemController : BaseController
    {
        private readonly BusinessLogic businessLogic;
        private readonly IProductBL _product;
        private readonly IStatistics _statistics;
        private readonly IReview _review;
        // GET: Item
        public ItemController()
        {
            businessLogic = new BusinessLogic();
            _product = businessLogic.GetProductBL();
            _statistics = businessLogic.GetStatsBL();
            _review = businessLogic.GetReviewBl();
        }
        [HttpGet]
        public ActionResult Catalog(string searchTerm, string sortBy = "Price", bool ascending = true)
        {
            IEnumerable<ProductModelBack> products = new List<ProductModelBack>(); 
            if (searchTerm.IsNullOrWhiteSpace())
            {
                 products = _product.GetProductModelBacks();
            }
            else
            {
                products = _product.Search(searchTerm);
            }
           products = _product.SortProductsBL(sortBy, ascending,products);
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
            ViewBag.SearchTerm = searchTerm;
            return View(product2);
        }

        public ActionResult Product(int id)
        { 
           ProductModelBack prod =  _product.GetProduct(id);
            _statistics.UserVisitedPage(GetCurrentUser(),id);
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

            ProductPageViewModel productPageViewModel = new ProductPageViewModel();
            productPageViewModel.product = prod;
            productPageViewModel.reviews = _review.GetReviewsForProudctBL(id);
            productPageViewModel.ReviewMessage = new ReviewMessage();

            return View(productPageViewModel);
        }
        public ActionResult TradeIn()
        {
            return View();
        }
        public ActionResult Review(ProductPageViewModel model)
        {
            model.ReviewMessage.UserId = GetCurrentUser().Id;
            _review.ReviewProduct(model.ReviewMessage);

            return RedirectToAction("Product", new { id = model.ReviewMessage.ProductId }); ;
        }
      
    }
}