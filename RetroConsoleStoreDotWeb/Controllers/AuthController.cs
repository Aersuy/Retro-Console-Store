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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Auth()
        {
            return View();
        }
        private readonly BusinessLogic _businessLogic;

        public AuthController()
        {
            _businessLogic = new BusinessLogic();
        }

        // GET: /Auth/TestDatabase
        public ActionResult TestDatabase()
        {
            var result = _businessLogic.GetAuthBL().UserAuthLogic(new UserLoginDTO());
            ViewBag.Message = result;
            return View();
        }
    }
}