using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetroConsoleStoreDotWeb.ViewModel
{
    public class CartItemsViewModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Product info
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public int StockQuantity { get; set; }
    }
}