using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.API.MiscAPI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.Statistics;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.BL.Misc
{
    public class StatisticsBL : StatisticsAPI, IStatistics
    {
        public StatisticsBL(IError error) : base(error)
        {
        }
        public bool CheckoutStat(UserSmall user)
        {
            return CheckoutStatsAPI(user);
        }
        public bool UserVisitedPage(UserSmall user, int productId)
        {
            return UserVisitedPageAPI(user, productId);
        }
        public bool AddToCartStatBl(UserSmall user)
        {
            return AddToCartStatAPI(user);
        }
        public bool LoginStatBL(UserSmall user)
        {
            return LoginStatAPI(user);  
        }
        public ProductStatsT GetProductStatsBL(int productId)
        {
            return GetProdStatsAPI(productId);
        }
    }
}
