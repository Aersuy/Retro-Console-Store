using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RetroConsoleStoreDotBusinessLogic.BL_struct.BL.Misc;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Model.Statistics;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.Products;
using RetroConsoleStoreDotDomain.Statistics;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.API.MiscAPI
{
    public class StatisticsAPI
    {
        private readonly IError _error;
        private readonly IAdmin _admin;
        public StatisticsAPI(IError error, IAdmin admin)
        {
            _error = error;
            _admin = admin;
        }
        internal bool CheckoutStatsAPI(UserSmall user)
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    decimal totalSpent = 0;
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
                            productStats.numberSold += item.Quantity;
                            totalQuantity += item.Quantity;
                            productStats.totalRevenue += item.Quantity * item.ProductTypeT.Price;
                            totalSpent += item.Quantity * item.ProductTypeT.Price;
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
                        userSt.totalSpent += totalSpent;
                        userSt.totalProductsPurchased += totalQuantity;
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
        internal bool UserVisitedPageAPI(UserSmall user, int productId)
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    var userStats = ctx.UserStatsTs.FirstOrDefault(p => p.UserId == user.Id);
                    var productStats = ctx.ProductStatistics.FirstOrDefault(p => p.ProductId == productId);

                    userStats.totalPagesViewed += 1;
                    productStats.pageViews += 1;
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Description");
                return false;
            }
        }
        internal bool AddToCartStatAPI(UserSmall user)
        {
            try
            {
                using (var ctx = new MainContext())
                {
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
                        userSt.totalProductsAddedToCart += 1;
                        ctx.SaveChanges();
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Stats for adding item to cart");
                return false;
            }
        }
        internal bool LoginStatAPI(UserSmall user)
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    var userSt = ctx.UserStatsTs.FirstOrDefault(p => p.UserId == user.Id);
                    {
                        if (userSt == null)
                        {
                            userSt = new UserStatsT
                            {   UserId = user.Id,
                                totalPagesViewed = 0,
                                totalProductsAddedToCart = 0,
                                totalSpent = 0,
                                totalTimesLoggedOn = 1,
                                totalProductsPurchased = 0,
                            };
                            ctx.UserStatsTs.Add(userSt);

                        }
                        userSt.totalTimesLoggedOn += 1;
                        ctx.SaveChanges();
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error adding times logged in");
                return false;
            }
        }
        internal ProductStatsT GetProdStatsAPI(int id)
        {
            try
            {
                using (var ctx = new MainContext())
                {
                
                    var stats =  ctx.ProductStatistics.FirstOrDefault(p => p.ProductId == id);

                    return stats;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error getting prod stats");
                return null;
            }
        }
        internal TotalStatsProducts GetOverallProductStatsAPI()
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    var stats = ctx.ProductStatistics.ToList();

                    var totalStats = new TotalStatsProducts()
                    {
                        TotalItemsAdded = 0,
                        TotalItemsSold = 0,
                        TotalPageViews = 0,
                        TotalRevenue = 0,
                        SalesEfficiency = 0,
                        TotalUsers = 0,
                        ConversionRate = 0,
                        AverageOrderValue = 0,
                        ViewsPerUser = 0,
                        CustomerActivity = 0,
                    };
                    foreach (var item in stats)
                    {
                        totalStats.TotalItemsAdded++;
                        totalStats.TotalItemsSold += item.numberSold;
                        totalStats.TotalPageViews += item.pageViews;
                        totalStats.TotalRevenue += item.totalRevenue;
                    }
                    totalStats.TotalUsers = _admin.GetNumberOfUsersBL();
                    if (totalStats.TotalPageViews > 0)
                    {
                        totalStats.ConversionRate = ((decimal)totalStats.TotalItemsSold / totalStats.TotalPageViews * 100);
                    }
                    if (totalStats.TotalItemsSold > 0)
                    {
                        totalStats.AverageOrderValue = ((decimal)totalStats.TotalRevenue/totalStats.TotalItemsSold);
                    }
                    if (totalStats.TotalUsers > 0)
                    {
                        totalStats.ViewsPerUser = totalStats.TotalPageViews / totalStats.TotalUsers;
                    }
                    if (totalStats.TotalItemsAdded > 0 && totalStats.TotalItemsSold > 0)
                    {
                        totalStats.SalesEfficiency = totalStats.TotalItemsSold * 100.0m / totalStats.TotalItemsAdded;
                    }
                    totalStats.CustomerActivity = totalStats.TotalPageViews + totalStats.TotalItemsSold; 
                
                   
                    return totalStats;
                }
            } catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error getting total product stats");
                return null;
            }
        }
        internal List<ProductStatsT> GetListProductStatsAPI()
        {
            try
            {
                using(var ctx = new MainContext())
                {
                    var stats = ctx.ProductStatistics.ToList();
                    return stats;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error getting a list of product stats");
                return null;
            }
        }
        internal StringBuilder GenerateStatsCSVAPI()
        {
            var csv = new StringBuilder();
            var statistics = GetOverallProductStatsAPI();
            var statsList = GetListProductStatsAPI();
            csv.AppendLine("Metric,Value,Description");
            csv.AppendLine($"Total Revenue,{statistics.TotalRevenue:C},All-time earnings from product sales");
            csv.AppendLine($"Items Sold,{statistics.TotalItemsSold:N0},Total products purchased by customers");
            csv.AppendLine($"Page Views,{statistics.TotalPageViews:N0},Total product page visits");
            csv.AppendLine($"Products Added,{statistics.TotalItemsAdded:N0},Total items in store inventory");
            csv.AppendLine($"Registered Users,{statistics.TotalUsers:N0},Total customer accounts created");
            csv.AppendLine($"Conversion Rate,{(statistics.TotalPageViews > 0 ? ((decimal)statistics.TotalItemsSold / statistics.TotalPageViews * 100).ToString("F1") : "0")}%,Views to purchases ratio");
            csv.AppendLine($"Average Order Value,{statistics.AverageOrderValue:C},Revenue per sale");
            csv.AppendLine($"Engagement Score,{statistics.ViewsPerUser:F1},Views per user");
            csv.AppendLine($"Store Productivity,{statistics.SalesEfficiency:F1}%,Sales efficiency");
            csv.AppendLine($"Customer Activity,{statistics.CustomerActivity:N0},Total interactions");

            // Add section separator for individual product statistics
            csv.AppendLine();
            csv.AppendLine("==========================================");
            csv.AppendLine("INDIVIDUAL PRODUCT STATISTICS");
            csv.AppendLine("==========================================");
            csv.AppendLine();
            csv.AppendLine("Product ID,Product Name,Page Views,Items Sold,Total Revenue,Conversion Rate");

            try
            {
                using (var ctx = new MainContext())
                {
                    foreach (var item in statsList)
                    {
                        var product = ctx.ProductTypes.FirstOrDefault(p => p.Id == item.ProductId);
                        var productName = product?.Name ?? "Unknown Product";
                        var conversionRate = item.pageViews > 0 ? (item.numberSold * 100.0m / item.pageViews).ToString("F2") : "0.00";
                        
                        csv.AppendLine($"{item.ProductId},{productName},{item.pageViews},{item.numberSold},{item.totalRevenue:C},{conversionRate}%");
                    }
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error generating product details in CSV");
                csv.AppendLine("Error retrieving individual product statistics");
            }

            csv.AppendLine();
            csv.AppendLine("==========================================");
            csv.AppendLine($"Export Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            csv.AppendLine("==========================================");

            return csv;
        }
    }
}
