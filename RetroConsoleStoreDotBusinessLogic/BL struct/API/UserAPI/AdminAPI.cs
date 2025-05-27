using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.User;
using RetroConsoleStoreDotDomain.Enums;
namespace RetroConsoleStoreDotBusinessLogic.BL_struct.API.UserAPI
{
    public class AdminAPI
    {
        private readonly IError _error;
        private readonly ILogin _login;
        public AdminAPI(IError error, ILogin login)
        {
            _error = error;
            _login = login;
        }
        internal bool BanUserAPI(BanReport report)
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    UDBTablecs admin = ctx.Users.FirstOrDefault(p => p.id == report.adminB.Id);
                    UDBTablecs user = ctx.Users.FirstOrDefault(p => p.id == report.UserID);
                    if (admin == null || admin.level != URole.Administrator) 
                    {
                        return false;
                    }
                    UserBannedT banReport = new UserBannedT
                    {
                        AdmBanner = admin,
                        User = user,
                        Reason = report.reason,
                        DateBanned = DateTime.Now,
                        DateUnbanned = DateTime.Now.AddDays(report.numberOfDays),
                        LastIp = report.UserIp,
                    };
                    user.level = URole.Banned;
                    var cookie = _login.GetCookieByUserIdBL(user.id);
                    _login.ExpireSessionByCookieDB(cookie);
                    ctx.UserBannedTs.Add(banReport);
                    ctx.SaveChanges();    
                    return true;
                }

            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error banning user/API Layer");
                return false;
            }
        }
        internal UserSmall GetUserByID(int id)
        {
            return new UserSmall();
        }
        internal bool UpdateUser(UserSmall user)
        {
            return true;
        }
    }
}
