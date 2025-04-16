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

        
    }
}
