using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Enums;

namespace RetroConsoleStoreDotDomain.Model.User
{
    public class BanReport
    {
        public UserSmall userBanned {  get; set; }
        public UserSmall adminB {  get; set; }
        public String reason { get; set; }
        public String UserIp { get; set; }
        public int numberOfDays { get; set; }
    }
}
