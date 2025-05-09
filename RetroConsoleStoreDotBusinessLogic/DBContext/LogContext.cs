﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Error;
using RetroConsoleStoreDotDomain.Logs;

namespace RetroConsoleStoreDotBusinessLogic.DBContext
{
    public class LogContext : DbContext
    {
        public LogContext() : base("name=Retro-Console-Store-Log") 
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LogContext>());
        }
        public virtual DbSet<LTable> Logs { get; set; }
        public virtual DbSet<ETable> Errors { get; set; }
    }

}
