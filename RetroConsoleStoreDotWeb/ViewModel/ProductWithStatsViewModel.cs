using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Products;
using RetroConsoleStoreDotDomain.Statistics;

namespace RetroConsoleStoreDotWeb.ViewModel
{
    public class ProductWithStatsViewModel
    {
        public ProductModelBack product { get; set; }
        public List<ReviewT> reviews { get; set; }
        public ProductStatsT statsT { get; set; }
    }
}