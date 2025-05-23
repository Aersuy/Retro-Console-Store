using System;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotWeb.Models.Auth;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class AuthController : BaseController
    {
        // GET: Auth       

        private readonly BusinessLogic businessLogic;
        private readonly IAuth _auth;
        private readonly ILogin _login;
        public AuthController()
        {
            businessLogic = new BusinessLogic();
            _auth = businessLogic.GetAuthBL();
            _login = businessLogic.GetLoginBL();
        }

        [HttpGet]
        public ActionResult Auth()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Auth(Auth model)
        {
            var ModelDtO = new UserLoginDTO
            {   UserIp = Request.UserHostAddress,
                UserName = model.Name,
                Password = model.Password,
                Email = model.Email,
                Phone = model.Phone,
            };
            ViewBag.Message = _auth.UserAuthLogic(ModelDtO);
            return View(model);
        }
        



        public ActionResult Index()
        {

            return View();
           
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLoginDTO model)
        {
            if (ModelState.IsValid)
            {   
                var userSession = _login.LoginLogic(model);
                ViewBag.Message = userSession.Message;
                if (userSession.Success)
                {
                    HttpCookie cookie = _login.GenCookie(model);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userSession.Message);
                    return View();
                }
            }
           
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            if (Request.Cookies["X-KEY"]!= null)
            {
                string cookiValue = Request.Cookies["X-KEY"].Value;
                _login.ExpireSessionByCookieDB(cookiValue);
                var Cookie = new HttpCookie("X-KEY")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = ""
                };
                Response.Cookies.Add(Cookie);
            }
            return RedirectToAction("Login", "Auth");
        }

    

    }
}