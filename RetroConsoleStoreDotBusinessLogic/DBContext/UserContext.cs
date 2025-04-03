using System;
using System.Data.Entity;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotBusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=Retro-Console-Store") { }

        public virtual DbSet<UDBTablecs> Users { get; set; } 
    }
}
