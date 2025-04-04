using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Logs;

namespace RetroConsoleStoreDotBusinessLogic.DBContext
{
    public class LogContext : DbContext
    {
        public LogContext() : base("name=Retro-Console-Store-Log") { }
        public virtual DbSet<LTable> Logs { get; set; }
    }

}
