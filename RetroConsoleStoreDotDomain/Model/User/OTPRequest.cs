using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreDotDomain.Model.User
{
    public class OTPRequest
    {
        public string email {  get; set; }
        public string code {  get; set; }
        public string password { get; set; }
        public bool status { get; set; }
    }
}
