using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.API.UserAPI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreHelpers.Interfaces;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.BL.User
{
    public class AdminBL : AdminAPI, IAdmin
    {
        public AdminBL(IError error, ILogin login,IMessaging messaging,IPasswordHash passwordHash) : base(error,login,messaging,passwordHash)
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
        public bool AutoUnbanBL(UserSmall user)
        {
            return AutoUnbanAPI(user);
        }
        public bool UpdateUserBL(UserSmall user)
        {
            return UpdateUserAPI(user);
        }
        public int GetNumberOfUsersBL()
        {
            return GetNumberOfUsersAPI();
        }
        public OTPRequest ModifyPasswordBeginBL(UserSmall user, string newPassword, string newPassword2, string oldPassword)
        {
            return ModifyPasswordBegin(user, newPassword, newPassword2, oldPassword);
        }
        public bool ModifyPasswordBL(UserSmall user, string newPassword)
        {
            return ModifyPassword(user, newPassword);
        }
        public string Generate2FactorCodeBL()
        {
            return Generate2FactorCode();
        }
    }
}
