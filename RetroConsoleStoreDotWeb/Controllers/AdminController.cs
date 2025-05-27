using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStoreDotWeb.Models.Articol;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStore.BusinessLogic;
using System.IO;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class AdminController : BaseController
    {   
        private readonly BusinessLogic businessLogic;
        private readonly IProductBL _productBL;
        private readonly IAccountBL _accountBL;
        private readonly IAdmin _adminBL;
        private const string UploadPath = "~/Content/images/Products/";
        // GET: Admin
        public AdminController()
        {
            businessLogic = new BusinessLogic();
            _productBL = businessLogic.GetProductBL();
            _accountBL = businessLogic.GetAccountAPI();
            _adminBL = businessLogic.GetAdminBl();
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
                //

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
            ViewBag.Message = _productBL.AddProduct(ProductModelBack);
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
        [HttpGet]
        public ActionResult AdminUserPage()
        {
            var userModel = _accountBL.GetUsersBL();
            
            return View(userModel);
        }
        [HttpPost]
        public ActionResult BanUser(int userId, string reason, int days)
        {
            BanReport model = new BanReport
            {
                UserID = userId,
                reason = reason,
                UserIp = Request.UserHostAddress,
                adminB = GetCurrentUser(),
                numberOfDays = days
            };

            _adminBL.BanUserBl(model);
            return RedirectToAction("AdminUserPage", "Admin");
        }
        public ActionResult UnBanUser(int userId)
        {
            UnbanMessage unbanMessage = new UnbanMessage
            {
                UserID = userId,
                adminB = GetCurrentUser()
            };
            _adminBL.UnbanUserBL(unbanMessage);
            return RedirectToAction("AdminUserPage", "Admin");
        }
    }
}