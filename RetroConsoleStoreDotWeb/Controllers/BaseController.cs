using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotWeb.Extensions;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogin _login;

        public BaseController()
        {
            var businessLogic = new BusinessLogic();
            _login = businessLogic.GetLoginBL();
        }
        public void SessionStatus()
        {
            var Cookie = Request.Cookies["X-KEY"];
            if (Cookie != null)
            {
                var user = _login.GetUserByCookie(Cookie.Value);
                if (user != null)
                {
                    System.Web.HttpContext.Current.SetMySessiobObject(user);
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                }
                else
                {
                    System.Web.HttpContext.Current.Session.Clear();
                    if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
                    {
                        var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }

                    System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
                }
            }
        }
        public ActionResult RedirectIfNotLoggedIn()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Auth");
            }
            return null;
        }
    }
}