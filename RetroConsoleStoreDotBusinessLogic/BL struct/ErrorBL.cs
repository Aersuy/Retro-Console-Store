using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.DBContext;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Error;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class ErrorBL : IError
    {

        public void ErrorToDatabase(Exception ex, string Description)
        {
            string exception = ex.ToString();
            using(var ctx = new LogContext())
            {
                var ErrorTable = new ETable()
                {   ErrorMessage = Description,
                    Exception = exception,
                    Date = DateTime.Now,
                };
                
            ctx.Errors.Add(ErrorTable);
            ctx.SaveChanges(); 

            }
        }
        
    }
}
