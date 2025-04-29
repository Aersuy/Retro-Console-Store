using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Enums;

namespace RetroConsoleStoreDotDomain.Model.User
{
    public class UserSmall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastIp { get; set; }  
        public URole Role { get; set; }
        public int CartId { get; set; }

    }
}
