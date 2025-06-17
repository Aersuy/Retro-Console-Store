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
using System.Security.Cryptography;
using RetroConsoleStoreHelpers.Interfaces;
namespace RetroConsoleStoreDotBusinessLogic.BL_struct.API.UserAPI
{
    public class AdminAPI
    {
        private readonly IError _error;
        private readonly ILogin _login;
        private readonly IMessaging _messaging;
        private readonly IPasswordHash _passwordHash;
        public AdminAPI(IError error, ILogin login, IMessaging messaging,IPasswordHash passwordHash)
        {
            _error = error;
            _login = login;
            _messaging = messaging;
            _passwordHash = passwordHash;
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
                        roleBeforeBeingBanned = user.level
                    };
                    user.level = URole.Banned;
                    var cookie = _login.GetCookieByUserIdBL(user.id);
                    _login.ExpireSessionByCookieDB(cookie);
                    ctx.UserBannedTs.Add(banReport);
                    ctx.SaveChanges();
                    _messaging.SendBanEmailBL(report);
                    return true;
                }

            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error banning user/API Layer");
                return false;
            }
        }
        internal bool UnbanUserAPI(UnbanMessage message)
        {
            try
            {
                using(var ctx = new MainContext())
                {
                    if (message.adminB == null || message.adminB.Role != URole.Administrator)
                    {
                        return false;
                    }
                    UDBTablecs user = ctx.Users.FirstOrDefault(p => p.id == message.UserID);
                    UserBannedT banReport = ctx.UserBannedTs.FirstOrDefault(p => p.User.id == message.UserID);

                    if (banReport != null)
                    {
                        user.level = banReport.roleBeforeBeingBanned;
                        ctx.UserBannedTs.Remove(banReport);
                        ctx.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error unbanning user");
                return false ;
            }

        }
        internal bool AutoUnbanAPI(UserSmall user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                using (var ctx = new MainContext())
                {
                    UDBTablecs userM = ctx.Users.FirstOrDefault(p => p.id == user.Id);
                    UserBannedT banReport = ctx.UserBannedTs.FirstOrDefault(p => p.User.id == user.Id);
                    if (banReport == null || userM == null)
                    {
                        {
                            throw new ArgumentNullException();
                        }
                    }
                    if (banReport.DateUnbanned < DateTime.Now)
                    {
                        userM.level = banReport.roleBeforeBeingBanned;
                        ctx.UserBannedTs.Remove(banReport);
                        ctx.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error auto unbanning user");
                return false;
            }
        }
       
        internal bool UpdateUserAPI(UserSmall user)
        {  try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                using (var ctx = new MainContext())
                {
                    UDBTablecs userM = ctx.Users.FirstOrDefault(p => p.id == user.Id);
                    if (userM == null)
                    {
                        throw new ArgumentNullException(nameof(userM));
                    }
                    userM.username = user.Name;
                    userM.email = user.Email;
                    userM.ImagePath = user.ImagePath;
                    userM.level = user.Role;
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error updating user");
                return false;
            }
        }
        internal int GetNumberOfUsersAPI()
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    var users = ctx.Users.ToList();
                    return users.Count;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error getting the number of users");
                return 0;
            }
        }
        internal string Generate2FactorCode()
        {
            int minValue = 0;
            int maxValue = 999999;
            int range = maxValue - minValue;
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                // Generate a uniformly distributed number in [minValue, maxValue)
                byte[] bytes = new byte[4];
                uint result;
                do
                {
                    rng.GetBytes(bytes);
                    result = BitConverter.ToUInt32(bytes, 0);
                } while (result >= uint.MaxValue - (uint.MaxValue % (uint)range)); // Avoid modulo bias

              return (minValue + (int)(result % range)).ToString("D6");
            }

        }
        internal ModifyPasswordRequest ModifyPasswordBegin(UserSmall user, string newPassword, string newPassword2, string oldPassword)
        {
            try
            {
                var modPass = new ModifyPasswordRequest()
                {
                    code = Generate2FactorCode(),
                    user = user,
                    password = newPassword,
                    status = true
                };
                using (var ctx = new MainContext()) 
                {
                    var userM = ctx.Users.FirstOrDefault(p => p.id == user.Id);
                    if (newPassword != newPassword2 || !_passwordHash.VerifyPassword(oldPassword,userM.password))
                    {
                        modPass.status = false;
                    }
                }
                if (modPass.status)
                {
                    modPass.status = _messaging.SendConfirmationEmailBl(modPass);

                }
                return modPass;
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error mod pass begin //API");
                return null;
            }
        }
        internal bool ModifyPassword(UserSmall user, string newPassword)
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    var userL = ctx.Users.FirstOrDefault(p => p.id == user.Id);
                    userL.password = _passwordHash.HashPassword(newPassword);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex,"Error modifying password");
                return false;
                
            }   
        }
    }
}
