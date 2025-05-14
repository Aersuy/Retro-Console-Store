using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotDomain.Products
{
    public class CartItemT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserCartT")]
        public int CartId { get; set; }

        [Required]
        [ForeignKey("ProductTypeT")]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Navigation properties
        public virtual UserCartT UserCartT { get; set; }
        public virtual ProductTypeT ProductTypeT { get; set; }
       
    }
}
