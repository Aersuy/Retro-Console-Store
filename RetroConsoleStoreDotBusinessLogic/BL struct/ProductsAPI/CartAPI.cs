using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;
namespace RetroConsoleStoreDotBusinessLogic.BL_struct.ProductsAPI
{
    internal class CartAPI : ICartAPI
    {
        public bool AddProductTooCart(int ProductID, int Quantity)
        {
            throw new NotImplementedException();
        }
        public bool RemoveProductFromCart(int ProductID,int Quantity)
        {
            throw new NotImplementedException();
        }
        public bool UpdateCartItem(int ProductID, int Quantity)
        {
            throw new NotImplementedException();
        }
        public decimal GetTotalPrice()
        {
            throw new NotImplementedException();
        }
        public bool ClearCart()
        {
            throw new NotImplementedException();
        }
        public bool Checkout()
        {
            throw new NotImplementedException();
        }
        public bool ApplyDiscountCode(string DiscountCode)
        {
            throw new NotImplementedException();
        }
        public bool RemoveDiscountCode(string DiscountCode)
        {
            throw new NotImplementedException();
        }
        public bool SaveCart()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartItemModel> GetCartItems()
        {
            throw new NotImplementedException();
        }
    }
    
    }

