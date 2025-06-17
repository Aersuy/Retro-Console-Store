using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreDotDomain.Model.User
{
    public class ModifyPasswordRequest
    {
        public UserSmall user;
        public string code {  get; set; }
        public string password { get; set; }
        public bool status { get; set; }
    }
}
