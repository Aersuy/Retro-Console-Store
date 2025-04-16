using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreDotDomain.Products
{
    internal class PhysicalProductT
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DatabaseID { get; set; }

        [Required]
        [ForeignKey("ProductTypeT")]
        public int ProductTypeTId { get; set; }
        public string SerialNumber { get; set; }   
        public bool IsUsed { get; set; } // true if used, false if new
        public string Condition { get; set; } // New, Like New, Good, Acceptable
        public string BoxCondition { get; set; } // New, Like New, Good, Acceptable
        public string InformationAboutPrivousOwner { get; set; } // Information about the previous owner, if any





    }
}
