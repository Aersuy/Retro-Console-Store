using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreDotDomain.Model.Statistics
{
    internal class UserStatsT
    {
        public int totalTimesLoggedOn {  get; set; }
        public int totalProductsAddedToCart {  get; set; }
        public int totalPagesViewed {  get; set; }
        public int totalProductsPurchased {  get; set; }
        public decimal totalSpent { get; set; }



    }
}
