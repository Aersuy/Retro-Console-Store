using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.API.UserAPI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.BL.User
{
    public class AdminBL : AdminAPI, IAdmin
    {
        public AdminBL(IError error, ILogin login) : base(error,login)
        {
        }

        public bool BanUserBl(BanReport report)
        {
            return BanUserAPI(report);
        }
        public bool UnbanUserBL(UnbanMessage message)
        {
            return UnbanUserAPI(message);
        }
    }
}
