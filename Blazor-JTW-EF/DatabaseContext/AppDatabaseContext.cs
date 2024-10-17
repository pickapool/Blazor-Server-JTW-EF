using Blazor_JTW_EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blazor_JTW_EF.DatabaseContext
{
    public class AppDatabaseContext : IdentityDbContext<UserAccountModel>
    {
        public DbSet<UserAccountModel> Accounts { get; set; }
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccountModel>().ToTable("UserAccounts");

            // Configure IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey }); // Define composite key
                b.ToTable("UserLogins");
            });

            // Configure IdentityRole
            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("Roles");
            });

            // Configure IdentityUserRole
            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId }); // Define composite key
                b.ToTable("UserRoles");
            });

            // Configure IdentityUserClaim
            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.HasKey(uc => uc.Id); // Define primary key
                b.ToTable("UserClaims");
            });

            // Configure IdentityUserToken
            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name }); // Define composite key
                b.ToTable("UserTokens");
            });
        }
    }
}
