using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.Product;

namespace RetroConsoleStoreDotDomain.Model.User
{
    class UserCart
    {
        public List<CartItemModel> CartItems { get; set; }
    }
}
