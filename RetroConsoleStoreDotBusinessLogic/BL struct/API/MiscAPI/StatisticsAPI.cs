using System;
using System.Collections.Generic;
using System.Linq;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Statistics;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.Products;
using RetroConsoleStoreDotDomain.Statistics;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.API.MiscAPI
{
    public class StatisticsAPI
    {
        private readonly IError _error;
        public StatisticsAPI(IError error)
        {
            _error = error;
        }
        internal bool CheckoutStatsAPI(UserSmall user)
        {
            try
            {
                using (var ctx = new UserContext())
                {   Decimal allRoundSpent = 0;
                    int totalQuantity = 0;
                    List<CartItemT> items = ctx.CartItems.Where(p => p.CartId == user.CartId).ToList();
                    foreach (var item in items)
                    {
                        var productStats = ctx.ProductStatistics.FirstOrDefault(p => p.ProductId == item.ProductId);
                            {
                            if (productStats == null)
                            {
                                productStats = new ProductStatsT
                                {
                                    ProductId = item.ProductId,
                                    totalRevenue = 0,
                                    numberSold = 0,
                                    pageViews = 0,
                                };
                                ctx.ProductStatistics.Add(productStats);
                            }
                            productStats.numberSold = item.Quantity;
                            totalQuantity += item.Quantity;
                            productStats.totalRevenue = item.Quantity * item.ProductTypeT.Price;
                            allRoundSpent += item.Quantity * item.ProductTypeT.Price;
                            ctx.SaveChanges();
                        }
                    }
                    var userSt = ctx.UserStatsTs.FirstOrDefault(p => p.UserId == user.Id);
                    {
                        if (userSt == null)
                        {
                            userSt = new UserStatsT
                            {
                                totalPagesViewed = 0,
                                totalProductsAddedToCart = 0,
                                totalSpent = 0,
                                totalTimesLoggedOn = 1,
                                totalProductsPurchased = 0,     
                            };
                            ctx.UserStatsTs.Add(userSt);
                           
                        }
                        userSt.totalSpent = allRoundSpent;
                        userSt.totalProductsPurchased = totalQuantity;
                        ctx.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error stats api");
                return false;
            }
        }
    }
}
