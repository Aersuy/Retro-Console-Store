using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.DBContext;
using RetroConsoleStoreDotDomain.Error;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.MiscAPI
{
    public class ErrorAPI
    {
        internal void ErrorToDatabaseAPI(Exception ex, string Description)
        {
            string exception = ex.ToString();
            using (var ctx = new LogContext())
            {
                var ErrorTable = new ETable()
                {
                    ErrorMessage = Description,
                    Exception = exception,
                    Date = DateTime.Now,
                };

                ctx.Errors.Add(ErrorTable);
                ctx.SaveChanges();

            }
        }
    }
}
