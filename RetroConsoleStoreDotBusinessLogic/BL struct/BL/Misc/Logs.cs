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
using RetroConsoleStoreDotBusinessLogic.BL_struct.MiscAPI;
using System.Runtime.InteropServices.WindowsRuntime;
using RetroConsoleStoreDotDomain.Model.Misc;
namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    //TODO : Make the product log method work with just the id
    // need to query the db for other data
    public class Logs :LogsAPI, ILog
    {
        public Logs(IError error) : base(error)
        {

        }
        /// <summary>
        /// Method for loggign data to database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string AuthLog(UserLoginDTO data)
        {
            return AuthLogAPI(data);
        }
        /// <summary>
        /// Method for logging login to database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string LoginLog(UserLoginDTO data)
        {
            return LoginLogAPI(data);

        }
        public string ProductLog(ProductModelBack data, string Descr)
        {
           return ProductLogAPI(data, Descr);
        }
        public List<LogM> GetRecentBL(int? days)
        {
            return GetRecentAPI(days);
        }
    }
}

