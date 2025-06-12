using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreDotDomain.Model.Product
{
    public class TotalStatsProducts
    {
        public decimal TotalRevenue { get; set; }
        public int TotalPageViews { get; set; }
        public int TotalItemsSold { get; set; }
        public int TotalItemsAdded { get; set; }
        public int TotalUsers {  get; set; }
        public decimal ConversionRate { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int ViewsPerUser { get; set; }
        public decimal SalesEfficiency { get; set; }
        public int CustomerActivity { get; set; }
    }
}
