using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStoreDotWeb.Models.Articol;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStore.BusinessLogic;
using System.IO;
using RetroConsoleStoreDotBusinessLogic.Attributes;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class AdminController : BaseController
    {   
        private readonly BusinessLogic _businessLogic;
        private const string UploadPath = "~/Content/images/Products/";
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
        public ActionResult AddProduct(Product model, HttpPostedFileBase imageFile)
        {
            var ProductModelBack = new ProductModelBack();
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string uploadDir = Server.MapPath(UploadPath);
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }
                string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                string extension = Path.GetExtension(imageFile.FileName);

                fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;

                string path = Path.Combine(uploadDir, fileName);
                imageFile.SaveAs(path);

                ProductModelBack.ImagePath = UploadPath.Replace("~", "") + fileName;
            }
            else
            {
                ProductModelBack.ImagePath = UploadPath.Replace("~", "") + "missing-picture-page-website-design-600nw-1552421075.webp";
            }
            ProductModelBack.Name = model.Name;
            ProductModelBack.Description = model.Description;
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