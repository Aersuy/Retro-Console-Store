using System;
using System.Data.Entity;
using System.Reflection.Emit;
using RetroConsoleStoreDotDomain.Products;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotBusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=Retro-Console-Store") { }

        public virtual DbSet<UDBTablecs> Users { get; set; } 
        public virtual DbSet<ProductTypeT> ProductTypes { get; set; }
        public virtual DbSet<CartItemT> CartItems { get; set; }
        public virtual DbSet<UserCartT> UserCarts { get; set; }
        public virtual DbSet<SessionT> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCartT>().HasKey(k => k.UserID);
            modelBuilder.Entity<UDBTablecs>().HasOptional(e => e.Cart)
                .WithRequired(a => a.User);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
