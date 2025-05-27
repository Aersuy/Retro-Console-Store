using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RetroConsoleStoreDotDomain.Enums;

namespace RetroConsoleStoreDotDomain.Model.User
{
    public class BanReport
    {
        public int UserID { get; set; }
        public UserSmall adminB {  get; set; }
        public String reason { get; set; }
        public String UserIp { get; set; }
        public int numberOfDays { get; set; }
   
    }
}
