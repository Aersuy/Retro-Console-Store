using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Model.User;
namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface ICartAPI
    {
        bool AddProductTooCart(int ProductID, int Quantity, UserSmall user);
        bool RemoveProductFromCart(int ProductID, int Quantity,UserSmall user); // Remove product from cart
        IEnumerable<CartItemModel> GetCartItems(UserSmall user); // Get all items in the cart
        decimal GetTotalPrice(UserSmall user); // Get total price of all items in the cart
        bool ClearCart(UserSmall user); // Clear all items in the cart
        bool UpdateCartItem(int ProductID, int Quantity, UserSmall user); // Update quantity of a product in the cart
        bool Checkout(UserSmall user); // Checkout the cart
        bool ApplyDiscountCode(string DiscountCode, UserSmall user); // Apply discount code to the cart
        bool RemoveDiscountCode(string DiscountCode, UserSmall user); // Remove discount code from the cart
    }
}
