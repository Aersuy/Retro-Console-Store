﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.DBContext;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Logs;
using RetroConsoleStoreDotDomain.Model.Misc;
using RetroConsoleStoreDotDomain.Model.Product;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.MiscAPI
{
    public class LogsAPI
    {   private readonly IError _error;
        public LogsAPI(IError error)
        {
            _error = error;
        }
        public string AuthLogAPI(UserLoginDTO data)
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

                    using (var ctx2 = new MainContext())
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
        public string LoginLogAPI(UserLoginDTO data)
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
        public string ProductLogAPI(ProductModelBack data, string Descr)
        {
            try
            {
                using (var ctx = new LogContext())
                {
                    var LogEntry = new LTable
                    {
                        UserName = data.Name,
                        Description = Descr,
                        CreatedDate = DateTime.Now,
                        // UserIp = data.UserIp,
                        Type = "Product"
                    };

                    using (var ctx2 = new MainContext())
                    {
                        var product = ctx2.ProductTypes.FirstOrDefault(u => u.Id == data.Id);
                        if (product != null)
                        {
                            LogEntry.UserId = product.Id;
                            LogEntry.Description += " \n Successful \n";
                        }
                        else
                        {
                            LogEntry.Description += "\n Failed \n";
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
        internal LogM TableToModelLogMap(LTable table)
        {
            var logMap = new LogM()
            {
                CreatedDate = table.CreatedDate,
                LogId = table.LogId,
                Description = table.Description,
                Type = table.Type,  
                UserId = table.UserId,
                UserIp = table.UserIp,
                UserName = table.UserName
            };
            return logMap;
        }
        internal List<LogM> GetRecentAPI(int? days)
        {
            try 
            {
                if (days < 0)
                {
                    throw new InvalidDataException("Days parameter cannot be negative");
                }

                using (var ctx = new LogContext())
                {
                    var cutoffDate = DateTime.Now.AddDays(-(days ?? 1));
                    var logs = ctx.Logs
                        .Where(p => p.CreatedDate >= cutoffDate)
                        .OrderByDescending(p => p.CreatedDate)
                        .ToList();

                    return logs.Select(TableToModelLogMap).ToList();
                }
            }
            catch (InvalidDataException ex)
            {
                _error.ErrorToDatabase(ex, "Invalid days parameter for log retrieval");
                return new List<LogM>();
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error retrieving logs from database");
                return new List<LogM>();
            }
        }
    }
}
