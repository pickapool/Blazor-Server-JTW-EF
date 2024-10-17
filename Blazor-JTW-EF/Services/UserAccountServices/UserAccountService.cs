using Blazor_JTW_EF.AuthenticationProvider;
using Blazor_JTW_EF.Common;
using Blazor_JTW_EF.DatabaseContext;
using Blazor_JTW_EF.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blazor_JTW_EF.Services.UserAccountServices
{
    public class UserAccountService : Controller, IUserAccountService
    {
        private readonly IDbContextFactory<AppDatabaseContext> _contextFactory;
        private readonly ILocalStorageService _localStorage;
        private readonly PasswordHasher<IdentityUser> _passwordHasher;
        public UserAccountService(IDbContextFactory<AppDatabaseContext> contextFactory, ILocalStorageService localStorange)
        {
            _contextFactory = contextFactory;
            _localStorage = localStorange;
            _passwordHasher = new PasswordHasher<IdentityUser>();
        }

        public async Task<List<UserAccountModel>> GetAccounts()
        {
            using (var dbContext = _contextFactory.CreateDbContext())
            {
                return await dbContext.Accounts.ToListAsync();
            }
        }
        public async Task<AuthTokenResponseModel> Authenticate(string username, string password)
        {
            using (var dbContext = _contextFactory.CreateDbContext())
            {
                var user = await dbContext.Accounts.SingleOrDefaultAsync(a => a.UserName == username);
                if (user != null)
                {
                    var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                    if (verificationResult == PasswordVerificationResult.Success)
                    {
                        var claimsIdentity = new ClaimsIdentity(new[]
                        {
                            new Claim("username", user.UserName),
                            new Claim("firstname", user.Firstname),
                            new Claim("lastname", user.Lastname),
                            new Claim("middlename", user.Middlename),
                            new Claim("position", user.Position),
                            new Claim("userid", user.UserId.ToString()),
                            new Claim(ClaimTypes.Role, user.Position)
                        });

                        // Generate JWT token using the extension method
                        var token = Extensions.GenerateJwtToken(claimsIdentity, "1t_r3publ1cKey!@#$%^&*()_+12345678!");
                        await _localStorage.SetItemAsync("token", token);
                        // Return the token response model
                        return new AuthTokenResponseModel(user, token);
                    }
                }
                return null;
            }
        }
        public async Task CreateAccount(UserAccountModel userAccount)
        {
            using (var dbContext = _contextFactory.CreateDbContext())
            {
                var user = new UserAccountModel
                {
                    UserName = userAccount.UserName,
                    Firstname = userAccount.Firstname,
                    Lastname = userAccount.Lastname,
                    Middlename = userAccount.Middlename,
                    Position = userAccount.Position,
                    // Additional properties as needed
                };
                // Hash the password
                user.PasswordHash = _passwordHasher.HashPassword(user, userAccount.Password);
                dbContext.Accounts.Add(user);
                await dbContext.SaveChangesAsync();
            }
        }
        
    }
}
