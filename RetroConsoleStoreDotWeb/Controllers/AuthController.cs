using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStore.Domain.Model.User;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class AuthController : Controller
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
        public ActionResult Auth(UserLoginDTO model)
        {
            ViewBag.Message = _businessLogic.GetAuthBL().UserAuthLogic(model);
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
        {   ViewBag.Message = _businessLogic.GetLoginBL().LoginLogic(model);
            return View(model);
        }
     
      
    }
}