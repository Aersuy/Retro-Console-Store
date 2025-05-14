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
    public class UserCartT
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }

        public int UserID { get; set; }
        

        // Add the navigation property that matches the ForeignKey name
        public virtual UDBTablecs User { get; set; }






        public virtual ICollection<CartItemT> CartItems { get; set; } = new List<CartItemT>();

    }
}
