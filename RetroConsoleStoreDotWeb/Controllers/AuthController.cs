using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotWeb.Models.Auth;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class AuthController : BaseController
    {
        // GET: Auth

        private readonly BusinessLogic _businessLogic;

        public AuthController()
        {
            _businessLogic = new BusinessLogic();
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
            ViewBag.Message = _businessLogic.GetAuthBL().UserAuthLogic(ModelDtO);
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
        public ActionResult Login(Login model)
        {
            //password doesn't arrive in the model??
            if (ModelState.IsValid)
            {
                var ModelDtO = new UserLoginDTO
                {
                    UserIp = Request.UserHostAddress,
                    UserName = model.UserName,
                    Password = model.Password,

                };
                var userSession = _businessLogic.GetLoginBL().LoginLogic(ModelDtO);
                if (userSession.Success)
                {
                    HttpCookie cookie = _businessLogic.GetLoginBL().GenCookie(ModelDtO);
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
    

    }
}