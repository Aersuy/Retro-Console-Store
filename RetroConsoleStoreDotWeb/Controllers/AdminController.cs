using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AddProduct()
        {
            return View();
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