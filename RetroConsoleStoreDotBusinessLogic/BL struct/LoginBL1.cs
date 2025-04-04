using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.DBContext;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class LoginBL1 : ILogin
    {
        public string LoginLogic(UserLoginDTO data)
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    if (!ValidateUserInput(data))
                    {
                        return "Invalid username or password format";
                    }
                    if (!UserWithNameAlreadyExists(data, ctx))
                    {
                        return "User does not exist";
                    }

                    var user = ctx.Users.FirstOrDefault(u => u.username == data.UserName);
                    if (user == null)
                    {
                        return "User does not exist";
                    }
                    if (user.password != data.Password)
                    {
                        return "Wrong Password";
                    }

                    user.LastRegisterDate = DateTime.Now;
                    user.LastIP = data.UserIp;
                    ctx.SaveChanges();
                    using (var ctx2 = new LogContext()) 
                    {
                        
                    }
                    return "Login successful";
                }
                catch (Exception ex)
                {
                    return $"An error occurred: {ex.Message}";
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
    }
}