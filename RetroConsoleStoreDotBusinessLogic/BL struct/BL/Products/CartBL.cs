using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.ProductsAPI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class CartBL : CartAPI, ICart
    {
        public CartBL(IError error, ILog log, ILogin login, IStatistics stats) : base(error, log, login, stats)
        {
        }
        public bool AddProductTooCart(int ProductID, int Quantity, UserSmall user)
        {
            return AddProductTooCartAPI(ProductID, Quantity, user);
        }
        public bool RemoveProductFromCart(int ProductID, UserSmall user) // Remove product from cart
        {
            return RemoveProductFromCartAPI(ProductID, user);
        }
        public IEnumerable<CartItemModel> GetCartItems(UserSmall user) // Get all items in the cart
        {
            return GetCartItemsAPI(user);
        }
        public bool UpdateCartItemQuantity(int ProductId, int Quantity, UserSmall user)
        {
            return UpdateCartItemQuantityAPI(ProductId, Quantity, user);
        }
        public decimal GetTotalPrice(UserSmall user) // Get total price of all items in the cart
        {
            return GetTotalPriceAPI(user);
        }

        public bool ClearCart(UserSmall user) // Clear all items in the cart
        {
            return ClearCartAPI(user);
        }
        public bool Checkout(UserSmall user) // Checkout the cart
        {
            return CheckoutAPI(user);
        }
        public bool ApplyDiscountCode(string DiscountCode, UserSmall user) // Apply discount code to the cart
        {
            return ApplyDiscountCodeAPI(DiscountCode, user);
        }
        public bool RemoveDiscountCode(string DiscountCode, UserSmall user) // Remove discount code from the cart
        {
            return RemoveDiscountCodeAPI(DiscountCode, user);
        }
    }
}
