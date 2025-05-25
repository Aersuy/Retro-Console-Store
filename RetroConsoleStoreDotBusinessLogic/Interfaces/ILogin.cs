using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStore.Domain.Model.User;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface ILogin
    {
        UserLoginResponse LoginLogic(UserLoginDTO data);
        HttpCookie GenCookie(UserLoginDTO data);
        UserSmall GetUserByCookie(string cookieName);
        void ExpireSessionByCookieDB(string cookieString);
        
        /// <summary>
        /// Expires all active sessions for a specific user - used for forced logout
        /// </summary>
        /// <param name="username">Username to expire sessions for</param>
        bool ExpireAllUserSessions(string username);
        
        /// <summary>
        /// Expires all active sessions for a specific user by user ID - used for forced logout
        /// </summary>
        /// <param name="userId">User ID to expire sessions for</param>
        bool ExpireAllUserSessionsById(int userId);
    }
}
