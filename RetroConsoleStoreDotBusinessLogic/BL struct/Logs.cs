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
using RetroConsoleStoreDotDomain.Model.Product;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class Logs : ILog
    {   private readonly IError _error;
        public Logs(IError error) 
        { 
            _error = error;
        }
        /// <summary>
        /// Method for loggign data to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string AuthLog(UserLoginDTO data)
        {
            try
            {
                using (var ctx = new LogContext())
                {
                    var LogEntry = new LTable
                    {
                        UserName = data.UserName,
                        Description = "User attempted authentification",
                        CreatedDate = DateTime.Now,
                        UserIp = data.UserIp,
                        Type = "Auth"
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
                    //Everything up untill here works / Update method works perfectly
                    ctx.Logs.Add(LogEntry);
                    ctx.SaveChanges();
                    return "Log created";
                }

            } 
            
              catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error in logging Auth process to db");
                return $"Error creating log entry: {ex.Message}";
            }
        }
        /// <summary>
        /// Method for logging login to database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string LoginLog(UserLoginDTO data)
        {
            try
            {
                throw new Exception(); // exception for not yet implemented
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error in loggin Login process to db");
                return $"Error creating log entry: {ex.Message}";
            }

        }
        public string ProductLog(ProductModelBack data)
        {
            try
            { 
                using (var ctx = new LogContext())
                {
                    var LogEntry = new LTable
                    {
                        UserName = data.Name
                        Description = "Attempted to add Product",
                        CreatedDate = DateTime.Now,
                        // UserIp = data.UserIp,
                        Type = "Product"
                    };

                    using (var ctx2 = new UserContext())
                    {
                        var product = ctx2.ProductTypes.FirstOrDefault(u => u.Id == data.Id);
                        if (product != null)
                        {
                            LogEntry.UserId = product.Id;
                            LogEntry.Description += " \n Successful \n";
                        }
                        else
                        {
                            LogEntry.Description += "\n Failed - User not found\n";
                        }
                    }
                    //Everything up untill here works / Update method works perfectly
                    ctx.Logs.Add(LogEntry);
                    ctx.SaveChanges();
                    return "Log created";
                
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error in logging the process of adding a product to db");
                return $"Error creating log entry: {ex.Message}";
            }
        }
}

