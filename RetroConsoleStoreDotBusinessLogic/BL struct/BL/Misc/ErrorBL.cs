using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.MiscAPI;
using RetroConsoleStoreDotBusinessLogic.DBContext;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Error;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class ErrorBL :ErrorAPI, IError
    {

        public void ErrorToDatabase(Exception ex, string Description)
        {
           ErrorToDatabaseAPI(ex, Description);
        }
        
    }
}
