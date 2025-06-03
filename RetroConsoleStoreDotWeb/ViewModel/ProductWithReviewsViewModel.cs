using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Products;

namespace RetroConsoleStoreDotWeb.ViewModel
{
    public class ProductPageViewModel
    {
        public ProductModelBack product {  get; set; }
        public List<ReviewT> reviews { get; set; }
        public ReviewMessage ReviewMessage { get; set; }
    }
}