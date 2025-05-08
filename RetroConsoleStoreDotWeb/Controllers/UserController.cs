using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStore.BusinessLogic;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        [HttpGet]
        public ActionResult UserPage()
        {
            var businessLogic = new BusinessLogic();
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