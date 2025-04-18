using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreDotDomain.Model.Product
{
     public class CartItemModel
    {
        public int Id { get; set; }
        public int ProductId { get;  set; }
        public int Quantity { get; set; }

    }
}
