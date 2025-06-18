using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStoreDotBusinessLogic.Attributes;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Misc;

namespace RetroConsoleStoreDotWeb.Controllers
{
    [User]
    public class HomeController : BaseController
    {
        // GET: Home
        private readonly IMessaging _message;
        public HomeController()
        {   var businessLogic = new BusinessLogic();
            _message = businessLogic.GetMessageBl();
        }
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login") 
            {
               return RedirectToAction("Login", "Auth");
            }
            return RedirectToAction("Catalog", "Item");
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
        [HttpPost]
        public ActionResult Contact(ContactMSG model)
        {
            RedirectIfNotLoggedIn();
            model.User = GetCurrentUser();
            _message.SendContactMessageBL(model);
            return RedirectToAction("Contact", "Home");
        }
        [HttpGet]
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