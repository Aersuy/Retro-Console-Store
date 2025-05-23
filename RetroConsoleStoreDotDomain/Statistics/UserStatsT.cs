using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotDomain.Model.Statistics
{
    public class UserStatsT
    {

        [Key]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Navigation property
        public virtual UDBTablecs User {  get; set; }
        public int totalTimesLoggedOn {  get; set; }
        public int totalProductsAddedToCart {  get; set; }
        public int totalPagesViewed {  get; set; }
        public int totalProductsPurchased {  get; set; }
        public decimal totalSpent { get; set; }





    }
}
