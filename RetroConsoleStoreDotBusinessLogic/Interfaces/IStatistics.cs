using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.Statistics;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface IStatistics
    {
        bool CheckoutStat(UserSmall user);
        bool UserVisitedPage(UserSmall user,int ProductId);
        bool AddToCartStatBl (UserSmall user);
        bool LoginStatBL(UserSmall user);
        ProductStatsT GetProductStatsBL(int productId);
        TotalStatsProducts GetOverallStats();
        List<ProductStatsT> GetProductsStatsListBl();
        StringBuilder GenerateCSVBL();
    }
}
