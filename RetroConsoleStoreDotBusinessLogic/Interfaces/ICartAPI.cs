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
        bool RemoveProductFromCart(int ProductID, int Quantity); // Remove product from cart
        IEnumerable<CartItemModel> GetCartItems(); // Get all items in the cart
        decimal GetTotalPrice(); // Get total price of all items in the cart
        bool ClearCart(); // Clear all items in the cart
        bool UpdateCartItem(int ProductID, int Quantity); // Update quantity of a product in the cart
        bool Checkout(); // Checkout the cart
        bool ApplyDiscountCode(string DiscountCode); // Apply discount code to the cart
        bool RemoveDiscountCode(string DiscountCode); // Remove discount code from the cart
        bool SaveCart(); // Save the cart to the database

    }
}
