using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStoreDotBusinessLogic.Attributes;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotWeb.ViewModel;

namespace RetroConsoleStoreDotWeb.Controllers
{
    [User]
    public class UserController : BaseController
    {
        private readonly BusinessLogic businessLogic;
        private readonly ILogin _login;
        private readonly IAccountBL _account;
        private readonly IProductBL _product;
        private readonly ICart _cart;
        private readonly IAdmin _admin;
        private const string UploadPath = "~/Content/Images/ProfilePictures/";
        public UserController()
        {
            businessLogic = new BusinessLogic();
            _login = businessLogic.GetLoginBL();
            _account = businessLogic.GetAccountAPI();
            _product = businessLogic.GetProductBL();
            _cart = businessLogic.GetCartBL();
            _admin = businessLogic.GetAdminBl();
        }
        // GET: User
        [HttpGet]
        public ActionResult UserPage()
        {
            var userSmall = GetCurrentUser();
            if (userSmall != null)
            {
                return View(userSmall);
            }

            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        public ActionResult UserPage(UserSmall model, HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string uploadDir = Server.MapPath(UploadPath);
                if (!Directory.Exists(uploadDir))
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(uploadDir);
                }
                string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                string extension = Path.GetExtension(imageFile.FileName);

                fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;

                string path = Path.Combine(uploadDir, fileName);
                imageFile.SaveAs(path);
                var cookie = Request.Cookies["X-KEY"];
                if (cookie != null)
                {
                    model = businessLogic.GetLoginBL().GetUserByCookie(cookie.Value);
                    if (model == null)
                    {
                        return View(model);
                    }
                }
                // Use virtual path for database storage
                model.ImagePath = UploadPath.Replace("~", "") + fileName;
                _account.AddProfilePicture(model);
                return RedirectToAction("UserPage");
            }
            return View(model);
        }
        public ActionResult Cart()
        {
            List<CartItemsViewModel> viewModel = new List<CartItemsViewModel>();
            UserSmall user = GetCurrentUser();
            IEnumerable<CartItemModel> cartItemsModel = _cart.GetCartItems(user);
            var productIds = cartItemsModel.Select(x => x.ProductId);
            var products = _product.GetProductModelBacks().Where(p => productIds.Contains(p.Id)).ToList();

            viewModel = cartItemsModel.Select(item =>
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                return new CartItemsViewModel
                {
                    CartItemId = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Name = product?.Name,
                    ImagePath = product?.ImagePath,
                    Price = product?.Price ?? 0,
                    Brand = product?.Brand,
                    StockQuantity = product?.StockQuantity ?? 0
                };
            }).ToList();

            return View(viewModel);
        }
        public ActionResult Statistics()
        {
            return View();
        }
        public ActionResult AddToCart(int productId, int quantity)
        {

            UserSmall user = GetCurrentUser();
            if (user != null)
            {
                var result = _cart.AddProductTooCart(productId, quantity, user);
                if (result)
                    TempData["CartMessage"] = "Product added to cart!";
                else
                    TempData["CartMessage"] = "Could not add product to cart.";

                return RedirectToAction("Product", "Item", new { id = productId });
            }
            return View();
        }
        public ActionResult RemoveItemFromCart(int productId)
        {
            UserSmall user = GetCurrentUser();

            if (user != null)
            {
                var result = _cart.RemoveProductFromCart(productId, user);
                return RedirectToAction("Cart", "User");
            }
            return RedirectToAction("Cart", "User");
        }
        public ActionResult UpdateCartItemQuantity(int productId, int quantity)
        {
            UserSmall user = GetCurrentUser();

            if (user != null)
            {
                var result = _cart.UpdateCartItemQuantity(productId, quantity, user);
                return RedirectToAction("Cart", "User");
            }

            return RedirectToAction("Cart", "User");
        }
        public ActionResult Checkout()
        {
            UserSmall user = GetCurrentUser();

            if (user != null)
            {
                var result = _cart.Checkout(user);
                return RedirectToAction("Cart", "User");
            }

            return View();
        }
        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            var modelM = _account.GetUserByIDBL(id);

            return View(modelM);
        }
        [HttpPost]
        public ActionResult UpdateUser(UserSmall model)
        {
            _admin.UpdateUserBL(model);
            return RedirectToAction("UserPage", "User", model.Id);
        }
        [HttpGet]
        public ActionResult ModifyPassword(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModifyPassword(string OldPassword, string newPass1, string newPass2)
        {
            var ModPassReq = _admin.ModifyPasswordBeginBL(GetCurrentUser(), newPass1, newPass2, OldPassword);
            if (ModPassReq.status == false)
            {
                return RedirectToAction("ModifyPassword", "User", GetCurrentUser().Id);
            }
            return RedirectToAction("TwoFactorPage", "User", ModPassReq);
        }
        [HttpGet]
        public ActionResult TwoFactorPage(ModifyPasswordRequest model)
        {
            model.user = GetCurrentUser();
            return View(model);
        }
        [HttpPost]
        public ActionResult TwoFactorPage(ModifyPasswordRequest model,string inputCode)
        {
            model.user = GetCurrentUser();
            if (model.code == inputCode)
            {
                _admin.ModifyPasswordBL(model.user, model.password);
            }
            else
            {
                ViewBag.message = "Failed 2 factor";
            }
            return RedirectToAction("UserPage","User",model.user.Id);
        }


    }
}