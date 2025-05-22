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
    public class CartAPI 
    {
        private readonly IError _error;
        private readonly ILog _log;

        internal CartAPI(IError error, ILog log,ILogin login)
        {
            _error = error;
            _log = log;
        }

        internal bool AddProductTooCartAPI(int ProductID, int Quantity, UserSmall user)
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
        internal bool RemoveProductFromCartAPI(int ProductID,UserSmall user)
        {
            try
            {
                using (var ctx = new UserContext())
                {
                    CartItemT cartItem = ctx.CartItems.FirstOrDefault(c => c.ProductId == ProductID && c.CartId == user.CartId);
                    if (cartItem != null)
                    {
                            ctx.CartItems.Remove(cartItem);
                            ctx.SaveChanges();
                            return true;  
                    }
                }
            } catch(Exception ex)
            {
                _error.ErrorToDatabase(ex, "Issue removing product form cart");
                return false;
            }
            throw new NotImplementedException();
        }
        internal bool UpdateCartItemQuantityAPI(int ProductID, int Quantity, UserSmall user)
        {
            try
            {
                using (var ctx = new UserContext())
                {
                    CartItemT cartItem = ctx.CartItems.FirstOrDefault(c => c.ProductId == ProductID && c.CartId == user.CartId);
                    if (cartItem != null)
                    {
                        cartItem.Quantity = Quantity;
                        ctx.SaveChanges();
                        return true;
                    }
                    return false;
                }
            } catch(Exception ex)
            {
                _error.ErrorToDatabase(ex, "Issue updating item quantity");
                return false;
            }
        }
        internal decimal GetTotalPriceAPI(UserSmall user)
        {
            throw new NotImplementedException();
        }
        internal bool ClearCartAPI(UserSmall user)
        {
            throw new NotImplementedException();
        }
        internal bool CheckoutAPI(UserSmall user)
        {
            try
            {

                using (var ctx = new UserContext())
                {

                    var cartItems = ctx.CartItems.Where(p => p.CartId == user.CartId).ToList();
                    ctx.CartItems.RemoveRange(cartItems);
                    ctx.SaveChanges();
                    return true;
                }
            } catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Description");
                return false;
            }
        }
        internal bool ApplyDiscountCodeAPI(string DiscountCode, UserSmall user)
        {
            throw new NotImplementedException();
        }
        internal bool RemoveDiscountCodeAPI(string DiscountCode, UserSmall user)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<CartItemModel> GetCartItemsAPI(UserSmall user)
        {
            try
            {
                using (var ctx = new UserContext())
                {
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

