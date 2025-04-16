using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Products;

namespace RetroConsoleStoreDotDomain.User
{
    class UserCartT
    {  
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UDBTablecs")]
        public int UDBTablecsId { get; set; } // Foreign key to the user table

        public List<PhysicalProductT> PhysicalProducts { get; } = new List<PhysicalProductT>(); // List of physical products in the cart

        public decimal TotalPrice { get; set; } // Total price of the cart

    }
}
