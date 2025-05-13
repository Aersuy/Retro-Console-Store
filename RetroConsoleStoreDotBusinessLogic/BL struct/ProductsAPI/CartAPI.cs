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

        public bool AddProductTooCart(int ProdunctID, int Quantity, UserSmall user)
        {
            {
               try
                {
                    CartItemT item = new CartItemT
                    {
                        ProductId = ProdunctID,
                        Quantity = Quantity,
                        CartId = user.CartId,
                    };
                    UserCartT cart;
                    using (var ctx = new UserContext())
                    {
                        cart = ctx.UserCarts.FirstOrDefault(c => c.UserID == user.Id);
                        if (cart == null)
                        {
                            cart = new UserCartT
                            {
                                UserID = user.Id,
                                CartItemTIds = new List<int>()
                            };
                            ctx.UserCarts.Add(cart);
                            ctx.SaveChanges();
                        }
                        ctx.CartItems.Add(item);
                        ctx.SaveChanges();
                        cart.CartItemTIds.Add(item.Id);
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
        public bool RemoveProductFromCart(int ProductID, int Quantity)
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
    /*
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     */
   
        
    };

