using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotDomain.Model.Product
{
    
    public class ReviewMessage
    {
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }

    }
}
