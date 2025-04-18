using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotDomain.Products
{
    public class ProductTypeT
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public DateTime TimeCreated { get; set; }
        public DateTime? TimeUpdated { get; set; }
        [Required]
        public string Status { get; set; } // DiscoNtinued, out of stock etc
        [Required]
        public int YearReleased { get; set; }
        [Required]
        public int TotalSoldOnSite { get; set; }

     
    }
}
