
namespace RetroConsoleStoreDotWeb.ViewModel
{
    public class TotalStatsViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalPageViews { get; set; }
        public int TotalItemsSold { get; set; }
        public int TotalItemsAdded { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int ViewsPerUser { get; set; }
        public decimal SalesEfficiency { get; set; }
        public int TotalUsers { get; set; }
        public int CustomerActivity {  get; set; }
    }
}