using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Products;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.API.ProductsAPI
{
    public class ReviewAPI
    {
        private readonly IError _error;
        public ReviewAPI(IError error)
        {
            _error = error;
        }
        
        public bool ReviewProductAPI(ReviewMessage message)
        {
            try
            {   if (message == null)
                {
                    throw new ArgumentNullException("message");
                }
                
                using (var ctx = new MainContext())
                {
                    var user = ctx.Users.FirstOrDefault(p => p.id == message.UserId);
                    var product = ctx.ProductTypes.FirstOrDefault(p => p.Id == message.ProductId);
                    var review = new ReviewT
                    {
                        ProductId = message.ProductId,
                        UserId = message.UserId,
                        rating = message.Rating,
                        ReviewText = message.Message,
                        Title = message.Title,
                        VerifiedPurchase = false, // To add this implementation need to add 
                        // more statistics, including saving every single product that a user has
                        // purchased, will see if I have time
                        DateTime = DateTime.Now
                    };
                    ctx.Reviews.Add(review);
                    user.Reviews.Add(review);
                    product.Reviews.Add(review);
                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) 
            {
                _error.ErrorToDatabase(ex, "Error leaving review//API");
                return false;
            }
        }
    }
}
