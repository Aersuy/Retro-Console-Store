using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.Products;
using RetroConsoleStoreDotDomain.User;
namespace RetroConsoleStoreDotBusinessLogic.BL_struct.ProductsAPI
{
    internal class CartAPI : ICartAPI
    {
        private readonly IError _error;
        private readonly ILog _log;

        internal CartAPI(IError error, ILog log,ILogin login)
        {
            _error = error;
            _log = log;
        }

        public bool AddProductTooCart(int ProductID, int Quantity, UserSmall user)
        {
            {
               try
                {
                   
                    UserCartT cart;
                    using (var ctx = new UserContext())
                    {
                        cart = ctx.UserCarts.FirstOrDefault(c => c.UserID == user.Id);
                        if (cart == null)
                        {
                            cart = new UserCartT
                            {
                                UserID = user.Id,
                                
                            };
                            ctx.UserCarts.Add(cart);
                            ctx.SaveChanges();
                            var user2 = ctx.Users.FirstOrDefault(c => c.id == user.Id);
                            user2.UserCartID = cart.CartID;
                            ctx.SaveChanges();
                        }
                        CartItemT cartItem = ctx.CartItems.FirstOrDefault(c => c.CartId == user.CartId && c.ProductId == ProductID);
                        if (cartItem != null)
                        {
                            cartItem.Quantity += Quantity;
                            ctx.SaveChanges();
                            return true;
                        }
                        CartItemT item = new CartItemT
                        {
                            ProductId = ProductID,
                            Quantity = Quantity,
                            CartId = cart.CartID,
                        };
                        cart.CartItems.Add(item);
                        ctx.SaveChanges();
                        return true;
                    }
                } catch (Exception ex)
                {
                    _error.ErrorToDatabase(ex, "Problem with adding product to the cart");
                    return false;
                }
            }
        }
        public bool RemoveProductFromCart(int ProductID, int Quantity,UserSmall user)
        {
            try
            {
                using (var ctx = new UserContext())
                {
                    CartItemT cartItem = ctx.CartItems.FirstOrDefault(c => c.ProductId == ProductID && c.CartId == user.CartId);
                    if (cartItem != null)
                    {
                        if (cartItem.Quantity - Quantity <= 0)
                        {
                            ctx.CartItems.Remove(cartItem);
                            ctx.SaveChanges();
                            return true;
                        }
                        cartItem.Quantity -= Quantity;
                        ctx.SaveChanges();
                    }
                }
            } catch(Exception ex)
            {
                _error.ErrorToDatabase(ex, "Issue removing product form cart");
                return false;
            }
            throw new NotImplementedException();
        }
        public bool UpdateCartItem(int ProductID, int Quantity,UserSmall user)
        {
            throw new NotImplementedException();
        }
        public decimal GetTotalPrice(UserSmall user)
        {
            throw new NotImplementedException();
        }
        public bool ClearCart(UserSmall user)
        {
            throw new NotImplementedException();
        }
        public bool Checkout(UserSmall user)
        {
            throw new NotImplementedException();
        }
        public bool ApplyDiscountCode(string DiscountCode, UserSmall user)
        {
            throw new NotImplementedException();
        }
        public bool RemoveDiscountCode(string DiscountCode, UserSmall user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartItemModel> GetCartItems(UserSmall user)
        {
            try
            {
                using (var ctx = new UserContext())
                {
                    var products = ctx.CartItems.Where(ci => ci.CartId == user.CartId);
                    var cartItems = ctx.CartItems
                   .Where(ci => ci.CartId == user.CartId)
                   .Select(ci => new CartItemModel
                  {
                     Id = ci.Id,
                     ProductId = ci.ProductId,
                     Quantity = ci.Quantity
                   })
                  .ToList();

                    return cartItems;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error getting all cart itmes");
                return new List<CartItemModel>();
            }
        }
    }        
};

