using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.User;
using RetroConsoleStoreHelpers.Cookies;
using System.Web;
using RetroConsoleStoreHelpers.Interfaces;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.UserAPI
{
    public class LoginAPI
    {   
        private readonly IError _error;
        private readonly ILog _log;
        private readonly IPasswordHash _passWordHash;

        internal LoginAPI(IPasswordHash passWordHash, ILog log, IError error)
        {
            _passWordHash = passWordHash;
            _log = log;
            _error = error;
        }
        internal UserLoginResponse LoginLogicAPI(UserLoginDTO data)
        {
            UserLoginResponse response = new UserLoginResponse();
            using (var ctx = new MainContext())
            {
                try
                {
                    if (!ValidateUserInputAPI(data))
                    {
                        response.Success = false;
                        response.Message = "Couldn't validate data format \n";
                        return response;
                    }

                    string TempPass = _passWordHash.HashPassword(data.Password);
                    var user = ctx.Users.FirstOrDefault(u => u.username == data.UserName);

                    if (user == null)
                    {
                        response.Success = false;
                        response.Message = "The username or password is incorect";
                        return response;
                    }
                    if (!_passWordHash.VerifyPassword(data.Password, user.password))
                    {
                        response.Success = false;
                        response.Message = "Incorrect password";
                        return response;
                    }



                    user.LastRegisterDate = DateTime.Now;
                    user.LastIP = data.UserIp;
                    ctx.SaveChanges();

                    response.Success = true;

                    return response;
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    _error.ErrorToDatabase(ex, "Login attempt failed");
                    return response;
                }
            }

        }

        internal bool ValidateUserInputAPI(UserLoginDTO data)
        {


            if (!string.IsNullOrEmpty(data.UserName) && !string.IsNullOrEmpty(data.Password) && data.UserName.Length >= 5 && data.Password.Length >= 8)
            {
                return true;
            }

            return false;
        }
        internal bool UserWithNameAlreadyExistsAPI(UserLoginDTO data, MainContext ctx)
        {
            return ctx.Users.Any(u => u.username == data.UserName);
        }
        internal HttpCookie GenCookieAPI(UserLoginDTO data)
        {
            try
            {
                var Cookie = new HttpCookie("X-KEY");
                {
                    Cookie.Value = CreateCookie.Create(data.Password);
                }
                using (var ctx = new MainContext())
                {
                    SessionT current;
                    ValidateUserInputAPI(data);

                    current = ctx.Sessions.FirstOrDefault(u => u.Name == data.UserName);

                    if (current != null)
                    {
                        current.CookieString = Cookie.Value;
                        current.ExpireTime = DateTime.Now.AddMinutes(60);
                        using (var todo = new MainContext())
                        {
                            todo.Entry(current).State = EntityState.Modified;
                            todo.SaveChanges();
                        }
                    }
                    else
                    {
                        ctx.Sessions.Add(new SessionT()
                        {
                            CookieString = Cookie.Value,
                            Name = data.UserName,
                            ExpireTime = DateTime.Now.AddMinutes(60)

                        });
                        ctx.SaveChanges();
                    }

                }
                return Cookie;
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error generating cookie");
                throw;
            }

        }
        internal UserSmall GetUserByCookieAPI(string cookieText)
        {
            try
            {
                SessionT current;
                using (var ctx = new MainContext())
                {
                    current = ctx.Sessions.FirstOrDefault(u => u.CookieString == cookieText && u.ExpireTime > DateTime.Now);
                }
                if (current == null)
                {
                    return null;
                }
                UDBTablecs user;
                using (var ctx = new MainContext())
                {
                    user = ctx.Users.FirstOrDefault(u => u.username == current.Name);
                }

                UserSmall userSmall = new UserSmall()
                {
                    Email = user.email,
                    Id = user.id,
                    Role = user.level,
                    CartId = user.UserCartID,
                    Name = user.username,
                    ImagePath = user.ImagePath
                };
                return userSmall;
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Issue getting user from cookie");
                return null;
            }

        }
        internal void ExpireSessionByCookieDBAPI(string cookieValue)
        {
            if (string.IsNullOrEmpty(cookieValue))
                return;

            using (var ctx = new MainContext())
            {
                var session = ctx.Sessions.FirstOrDefault(s => s.CookieString == cookieValue);
                if (session != null)
                {
                    session.ExpireTime = DateTime.Now.AddDays(-1);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Expires all active sessions for a specific user - used for forced logout (e.g., when banned)
        /// </summary>
        /// <param name="username">Username to expire sessions for</param>
        internal bool ExpireAllUserSessionsAPI(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    return false;

                using (var ctx = new MainContext())
                {
                    var sessions = ctx.Sessions.Where(s => s.Name == username && s.ExpireTime > DateTime.Now).ToList();
                    foreach (var session in sessions)
                    {
                        session.ExpireTime = DateTime.Now.AddDays(-1);
                    }
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error expiring all user sessions");
                return false;
            }
        }

        /// <summary>
        /// Expires all active sessions for a specific user by user ID - used for forced logout
        /// </summary>
        /// <param name="userId">User ID to expire sessions for</param>
        internal bool ExpireAllUserSessionsByIdAPI(int userId)
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    // First get the username
                    var user = ctx.Users.FirstOrDefault(u => u.id == userId);
                    if (user == null)
                        return false;

                    // Expire all sessions for this username
                    return ExpireAllUserSessionsAPI(user.username);
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error expiring user sessions by ID");
                return false;
            }
        }
    }
}
   

