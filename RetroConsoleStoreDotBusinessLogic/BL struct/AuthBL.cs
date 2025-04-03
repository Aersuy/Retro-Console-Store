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
using RetroConsoleStoreDotDomain.Enums;  // Added missing semicolon

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
                    if (string.IsNullOrEmpty(data.UserName) || string.IsNullOrEmpty(data.Password))
                    {
                        return "Username and password required";
                    }
                    
                    if (ctx.Users.Any(u => u.username == data.UserName))
                    {
                        return "Username Already Exists";
                    }
                    // user nou
                    var newUser = new UDBTablecs
                    {
                        username = "testuser",
                        password = "testpass123", 
                        email = "test@example.com",
                        RegisterDate = DateTime.Now,
                        LastRegisterDate = DateTime.Now,
                        level = URole.User
                    };

                    ctx.Users.Add(newUser);
                    ctx.SaveChanges();

                   
                    var user = ctx.Users.FirstOrDefault(u => u.username == "testuser");
                    return user != null ? "User created and retrieved successfully!" : "Failed to create/retrieve user";
                }
                catch (Exception ex)
                {
                    return $"Database error: {ex.Message}";
                }
            }
        } 
    }
}