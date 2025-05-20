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

    }
}
