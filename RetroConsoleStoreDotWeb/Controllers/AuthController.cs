using System;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotWeb.Models.Auth;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class AuthController : BaseController
    {
        // GET: Auth       

        private readonly BusinessLogic businessLogic;
        private readonly IAuth _auth;
        private readonly ILogin _login;
        private readonly IStatistics _statistics;
        private readonly IAdmin _admin;
        public AuthController()
        {
            businessLogic = new BusinessLogic();
            _auth = businessLogic.GetAuthBL();
            _login = businessLogic.GetLoginBL();
            _statistics = businessLogic.GetStatsBL();
            _admin = businessLogic.GetAdminBl();
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
                    var userSmall = GetCurrentUser();
                    _statistics.LoginStatBL(userSmall);
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
        [HttpGet]
        public ActionResult OTPLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OTPLogin(OTPRequest request)
        {
            request.code = _admin.Generate2FactorCodeBL();
            if (_login.Login2FABL(request).Success)
            {
                return RedirectToAction("Login2FA", "Auth", request);
            }
            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        public ActionResult Login2FA(OTPRequest request, string inputCode)
        {
            if (request.code == inputCode)
            {
               var response = _login.Login2FAEndBL(request.email);
                if (response.Success)
                {
                    HttpCookie cookie = _login.GenCookie(new UserLoginDTO { 
                        Email = request.email,
                        UserName = response.Message
                        });
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", response.Message);
                }
            }
            return View(request);
        }
        [HttpGet]
        public ActionResult Login2FA(OTPRequest request)
        {
            return View(request);
        }
    }
}