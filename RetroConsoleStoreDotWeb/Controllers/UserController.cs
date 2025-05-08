using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class UserController : BaseController
    {
        private readonly BusinessLogic businessLogic;
        private const string UploadPath = "~/Content/Images/ProfilePictures/";
        public UserController()
        {
            businessLogic = new BusinessLogic();
        }
        // GET: User
        [HttpGet]
        public ActionResult UserPage()
        {
            
            var cookie = Request.Cookies["X-KEY"];
            if (cookie != null)
            {
               
                var userSmall = businessLogic.GetLoginBL().GetUserByCookie(cookie.Value);
                if (userSmall != null)
                {
                    return View(userSmall);
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        public ActionResult UserPage(UserSmall model,  HttpPostedFileBase imageFile)
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
                businessLogic.GetUserAPI().AddProfilePicture(model);
                return RedirectToAction("UserPage");
            }
            return View(model);
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Statistics()
        {
            return View();
        }
    }
}