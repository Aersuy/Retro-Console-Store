using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.DBContext;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.User;
using RetroConsoleStoreHelpers.Cookies;
using RetroConsoleStoreHelpers.Interfaces;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class LoginBL1 : ILogin
    {
        ILog _log;
        IError _error;
        IPasswordHash _passWordHash;
        public LoginBL1(IPasswordHash passWordHash, ILog log, IError error)
        {
            _passWordHash = passWordHash;
            _log = log;
            _error = error;
        }
        public UserLoginResponse LoginLogic(UserLoginDTO data)
        {   UserLoginResponse response = new UserLoginResponse();
            using (var ctx = new UserContext())
            {
                try
                {
                    if (!ValidateUserInput(data))
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
                {   response.Message = ex.Message;
                    _error.ErrorToDatabase(ex, "Login attempt failed");
                    return response;
                }
            }

        }

        private bool ValidateUserInput(UserLoginDTO data)
        {
            

            if (!string.IsNullOrEmpty(data.UserName) && !string.IsNullOrEmpty(data.Password) && data.UserName.Length >= 5 && data.Password.Length >= 8)
            {
                return true;
            }
            
            return false;
        }
        private bool UserWithNameAlreadyExists(UserLoginDTO data, UserContext ctx)
        {
            return ctx.Users.Any(u => u.username == data.UserName);
        }
        public HttpCookie GenCookie(UserLoginDTO data)
        {  try
            {
                var Cookie = new HttpCookie("X-KEY");
                {
                    Cookie.Value = CreateCookie.Create(data.Password);
                }
                using (var ctx = new UserContext())
                {
                    SessionT current;
                    ValidateUserInput(data);

                    current = ctx.Sessions.FirstOrDefault(u => u.Name == data.UserName);

                    if (current != null)
                    {
                        current.CookieString = Cookie.Value;
                        current.ExpireTime = DateTime.Now.AddMinutes(60);
                        using (var todo = new UserContext())
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
            } catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error generating cookie");
                throw;  
            }
           
        }
        public UserSmall GetUserByCookie(string cookieText)
        {   SessionT current;
            using (var ctx = new UserContext())
            {
              current = ctx.Sessions.FirstOrDefault(u => u.CookieString == cookieText && u.ExpireTime > DateTime.Now);
            }
            if (current == null)
            {
                return null;
            }
            UDBTablecs user;
            using (var ctx = new UserContext())
            {
                user = ctx.Users.FirstOrDefault(u => u.username == current.Name);
            }

            UserSmall userSmall = new UserSmall()
            {
                Email = user.email,
                Id = user.id,
                Role = user.level,
                CartId = user.UserCartID,
                Name = user.username
            };
            return userSmall;
        }
        public void ExpireSessionByCookieDB(string cookieValue)
        {
            if (string.IsNullOrEmpty(cookieValue))
                return;

            using (var ctx = new UserContext())
            {
                var session = ctx.Sessions.FirstOrDefault(s => s.CookieString == cookieValue);
                if (session != null)
                {
                    session.ExpireTime = DateTime.Now.AddDays(-1); 
                    ctx.SaveChanges();
                }
            }
        }
    }
}