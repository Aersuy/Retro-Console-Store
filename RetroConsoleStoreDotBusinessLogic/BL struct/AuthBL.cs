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
using RetroConsoleStoreDotBusinessLogic.BL_struct;  // Added missing semicolon

namespace RetroConsoleStore.BusinessLogic.BL_Struct
{
    public class AuthBL : UserApi, IAuth
    {
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


                    var user = ctx.Users.FirstOrDefault(u => u.username == "testuser");
                    var logs = new Logs();
                    logs.AuthLog(data);
                    return user != null ? "User created and retrieved successfully!" : "Failed to create/retrieve user";
                }

                catch (Exception ex)
                {
                    var logs = new Logs();
                    logs.AuthLog(data);
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
                password = data.Password,
                email = data.Email ?? "default@email.com",
                RegisterDate = DateTime.Now,
                LastRegisterDate = DateTime.Now,
                LastIP = data.UserIp,
                level = URole.User
            };
        }
    }
}