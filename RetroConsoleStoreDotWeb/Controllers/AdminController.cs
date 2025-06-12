using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStoreDotWeb.Models.Articol;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStore.BusinessLogic;
using System.IO;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotBusinessLogic.Attributes;
using RetroConsoleStoreDotWeb.ViewModel;
using System.Text;
using RetroConsoleStoreDotBusinessLogic.BL_struct.BL.Misc;
using RetroConsoleStoreDotDomain.Logs;

namespace RetroConsoleStoreDotWeb.Controllers
{
    [Admin]
    public class AdminController : BaseController
    {   
        private readonly BusinessLogic businessLogic;
        private readonly IProductBL _productBL;
        private readonly IAccountBL _accountBL;
        private readonly IAdmin _adminBL;
        private readonly IStatistics _statisticsBL;
        private readonly IReview _reviewBL;
        private readonly ILog _log;
        private const string UploadPath = "~/Content/images/Products/";
        // GET: Admin
        public AdminController()
        {
            businessLogic = new BusinessLogic();
            _productBL = businessLogic.GetProductBL();
            _accountBL = businessLogic.GetAccountAPI();
            _adminBL = businessLogic.GetAdminBl();
            _reviewBL = businessLogic.GetReviewBl();
            _statisticsBL = businessLogic.GetStatsBL();
            _log = businessLogic.GetLogBL();
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
            return RedirectToAction("ManageProducts", "Admin");
        }
        public ActionResult DashBoard()
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
        
        public ActionResult ManageProducts()
        {

            List<ProductModelBack> products = _productBL.GetProductModelBacks();

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
            return View(products);
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            ProductModelBack prod = _productBL.GetProduct(id);
            return View(prod);
        }
        [HttpPost]
        public ActionResult EditProduct(int id,bool edit)
        {
            var product = _productBL.GetProduct(id);
            _productBL.UpdateProduct(product);
            return View();
        }
        public ActionResult DeleteProduct(int id)
        {
            _productBL.DeleteProductBL(id);
            return RedirectToAction("ManageProducts", "Admin");
        }
        public ActionResult ProductDetail(int id)
        {
            var model = new ProductWithStatsViewModel();
            model.product = _productBL.GetProduct(id);
            model.reviews = _reviewBL.GetReviewsForProudctBL(id);
            model.statsT = _statisticsBL.GetProductStatsBL(id);
            return View(model);
        }
        public ActionResult ProductStatistics()
        {
            TotalStatsProducts statistics = _statisticsBL.GetOverallStats();
            return View(statistics);
        }
        public ActionResult ExportCSV(int id)
        {
            var csv = _statisticsBL.GenerateCSVBL();
            var fileName = $"ProductStatistics_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", fileName);
        }
        public ActionResult Logs(int? days)
        {
            var logs = _log.GetRecentBL(days);
            return View(logs);
        }
        public ActionResult Reviews()
        {
            var reviews = _reviewBL.GetAllBL();
            return View(reviews);
        }
    }
}