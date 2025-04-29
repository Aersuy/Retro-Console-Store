using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult UserPage()
        {
            return View();
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