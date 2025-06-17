using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface IAdmin
    {
        bool BanUserBl(BanReport report);
        bool UnbanUserBL(UnbanMessage message);
        bool AutoUnbanBL(UserSmall user);
        bool UpdateUserBL(UserSmall user);
        int GetNumberOfUsersBL();
        ModifyPasswordRequest ModifyPasswordBeginBL(UserSmall user, string newPassword, string newPassword2, string oldPassword);
        bool ModifyPasswordBL(UserSmall user, string newPassword);
    }
}
