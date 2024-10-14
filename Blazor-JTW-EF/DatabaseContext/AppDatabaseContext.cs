using Blazor_JTW_EF.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blazor_JTW_EF.DatabaseContext
{
    public class AppDatabaseContext : DbContext
    {
        public DbSet<UserAccountModel> Accounts { get; set; }
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccountModel>().ToTable("UserAccounts");
        }
    }
}
