﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetroConsoleStoreDotWeb.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }
        public ActionResult TradeIn()
        {
            return View();
        }
    }
}