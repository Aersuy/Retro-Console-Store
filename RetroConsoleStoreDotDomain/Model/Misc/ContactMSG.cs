using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotDomain.Model.Misc
{
    public class ContactMSG
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string message { get; set; }
        public UserSmall User { get; set; }
    }
}
