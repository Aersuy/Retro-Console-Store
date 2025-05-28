using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroConsoleStoreDotBusinessLogic.Interfaces;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IAdmin _admin;
        public ErrorController()
        {
            
        }
        public ActionResult Banned()
        {
            return View();
        }
        
    }
}