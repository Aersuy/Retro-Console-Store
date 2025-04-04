using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.DBContext;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Logs;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class Logs : ILog
    {
        public Logs() { }

        public string AuthLog(UserLoginDTO data)
        {
            try
            {
                using (var ctx = new LogContext())
                {
                    var LogEntry = new LTable
                    {
                        UserName = data.UserName,
                        Description = "Use Auth Attempt",
                        CreatedDate = DateTime.Now,
                        UserIp = data.UserIp
                    };

                    using (var ctx2 = new UserContext())
                    {
                        var user = ctx2.Users.FirstOrDefault(u => u.username == data.UserName);
                        if (user != null)
                        {
                            LogEntry.UserId = user.id;  
                            LogEntry.Description += " \n Successful \n";
                        }
                        else
                        {
                            LogEntry.Description += "\n Failed - User not found\n";
                        }
                    }
                    //Everything up untill here works
                    ctx.Logs.Add(LogEntry);
                    ctx.SaveChanges();
                    return "Log created";
                }

            } 
            
              catch (Exception ex)
            {
                return $"Error creating log entry: {ex.Message}";
            }
        }
        }
    }

