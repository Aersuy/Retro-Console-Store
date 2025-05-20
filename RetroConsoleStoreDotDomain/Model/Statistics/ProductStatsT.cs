using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Products;

namespace RetroConsoleStoreDotDomain.Model.Statistics
{
    internal class ProductStatsT
    {
     
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required]
        public int numberSold { get; set; }

        [Required]
        public decimal totalRevenue { get; set; }

        [Required]
        public int pageViews { get; set; }

        [Required]
        [ForeignKey("ProductTypeT")]
        public int ProductId { get; set; }

        // Navigation property for ProductId
        public virtual ProductTypeT ProductTypeT { get; set; }

    }
}
