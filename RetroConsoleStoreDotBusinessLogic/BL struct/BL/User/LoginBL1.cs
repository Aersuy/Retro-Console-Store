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
using RetroConsoleStoreDotBusinessLogic.BL_struct.UserAPI;
using RetroConsoleStoreDotBusinessLogic.DBContext;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.User;
using RetroConsoleStoreHelpers.Cookies;
using RetroConsoleStoreHelpers.Interfaces;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class LoginBL1 :LoginAPI, ILogin
    {
        ILog _log;
        IError _error;
        IPasswordHash _passWordHash;
        public LoginBL1(IPasswordHash passWordHash, ILog log, IError error) : base(passWordHash, log, error)
        {
            _passWordHash = passWordHash;
            _log = log;
            _error = error;
        }
        public UserLoginResponse LoginLogic(UserLoginDTO data)
        {  
            return LoginLogicAPI(data);
        }
            
        public UserSmall GetUserByCookie(string cookieText)
        {
            return GetUserByCookieAPI(cookieText);
        }
        public HttpCookie GenCookie(UserLoginDTO data)
        {
            return GenCookieAPI(data);
        }
        public void ExpireSessionByCookieDB(string cookieString)
        {
            ExpireSessionByCookieDBAPI(cookieString);
        }
        public string GetCookieByUserIdBL (int userId)
        {
            return GetCookieByUserIdAPI(userId);
        }
    }
}