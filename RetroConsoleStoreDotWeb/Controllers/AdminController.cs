using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStoreDotWeb.Models.Articol;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStore.BusinessLogic;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class AdminController : Controller
    {   private readonly BusinessLogic _businessLogic;
        // GET: Admin
        public AdminController()
        {
            _businessLogic = new BusinessLogic();
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product model)
        {   
            var ProductModelBack = new ProductModelBack();
            ProductModelBack.Name = model.Name;
            ProductModelBack.Description = model.Description;
            ProductModelBack.ImagePath = model.ImagePath;
            ProductModelBack.Price = model.Price;
            ProductModelBack.StockQuantity = model.StockQuantity;
            ProductModelBack.Brand = model.Brand;
            ProductModelBack.YearReleased = model.YearReleased;
            ProductModelBack.Status = model.Status;
            ViewBag.Message = _businessLogic.GetProductBL().AddProduct(ProductModelBack);
            return View(ProductModelBack);
        }
        public ActionResult EditProduct()
        {
            return View();
        }
        public ActionResult ManageUsers()
        {
            return View();
        }
        public ActionResult ManageRoles()
        {
            return View();
        }
        public ActionResult AdminUserPage()
        {
            return View();
        }
    }
}