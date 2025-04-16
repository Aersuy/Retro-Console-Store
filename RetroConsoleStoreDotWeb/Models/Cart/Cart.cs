using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetroConsoleStoreDotWeb.Models.Articol;

namespace RetroConsoleStoreDotWeb.Models.Cart
{
    public class Cart
    {
        List<Product> products;
        public decimal TotalPrice { get; set; }
        
    }
}
