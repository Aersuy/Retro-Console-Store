using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStoreDotBusinessLogic.Attributes;

namespace RetroConsoleStoreDotWeb.Controllers
{
    [User]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login") 
            {
               return RedirectToAction("Login", "Auth");
            }
            return View();
        }
        public ActionResult productDetail()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }
        public ActionResult About()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
        public ActionResult Contact()
        {
            RedirectIfNotLoggedIn();

            return View();
        }
        public ActionResult Catalogue()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
    }
}