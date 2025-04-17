using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotDomain.Model.Product
{
    public class ProductModelBack
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        
        public string Description { get; set; }
        public string ImagePath { get; set; }
       
        public decimal Price { get; set; }
        
        public int StockQuantity { get; set; }
        
        public string Brand { get; set; }
        
        public DateTime TimeCreated { get; set; }
        public DateTime? TimeUpdated { get; set; }
        
        public string Status { get; set; } // DiscoNtinued, out of stock etc
      
        public int YearReleased { get; set; }
        
        public int TotalSoldOnSite { get; set; }
 
    }
}
