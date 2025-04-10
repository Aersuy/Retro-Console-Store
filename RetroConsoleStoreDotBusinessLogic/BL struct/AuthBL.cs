using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.Core;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotDomain.User;
using RetroConsoleStoreDotDomain.Enums;
using System.Runtime.InteropServices.ComTypes;
using RetroConsoleStoreDotBusinessLogic.BL_struct;
using RetroConsoleStoreHelpers.PasswordHash;
using RetroConsoleStoreHelpers.Interfaces;
using RetroConsoleStoreDotBusinessLogic.Interfaces;


namespace RetroConsoleStore.BusinessLogic.BL_Struct
{     // Use the presentation layer model 
      // Use auto object mapping for translation
      // between the presentation object and the BL
      // object
    public class AuthBL : UserApi, IAuth
    {
        private readonly IPasswordHash _passwordHash;
        private readonly IError _error;
        private readonly ILog _log;
        public IPasswordHash GetPasswordHash()
        {
            return _passwordHash;
        }
        public AuthBL(IPasswordHash passwordHash, IError error, ILog log)
        {
            _error = error;
            _passwordHash = passwordHash;
            _log = log;
        }
        public string UserAuthLogic(UserLoginDTO data)
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    if (!ValidateUserInput(data))
                    {
                        return "Enter valid data";
                    }
                    if(UserWithNameAlreadyExists(data,ctx))
                    {
                        return "User with name already exists";
                    }
                   
                    UDBTablecs NewUser = CreateNewUser(data);
                    // user nou
                    
                    ctx.Users.Add(NewUser);
                    ctx.SaveChanges();


                    var user = ctx.Users.FirstOrDefault(u => u.username == data.UserName);
                    // var logs = new Logs();
                    //  logs.AuthLog(data);
                    _log.AuthLog(data);
                    
                    return user != null ? "User created and retrieved successfully!" : "Failed to create/retrieve user";
                }

                catch (Exception ex)
                {
                  //  var logs = new Logs();
                    _log.AuthLog(data);
                   // logs.AuthLog(data);

                    _error.ErrorToDatabase(ex,"Problem with auth process");
                    
                    return $"Database error: {ex.Message}";
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
        private UDBTablecs CreateNewUser(UserLoginDTO data)
        {
            return new UDBTablecs
            {
                username = data.UserName,
                password = GetPasswordHash().HashPassword(data.Password),
                email = data.Email ?? "default@email.com",
                RegisterDate = DateTime.Now,
                LastRegisterDate = DateTime.Now,
                LastIP = data.UserIp,
                level = URole.User
            };
        }
    }
}