using Blazor_JTW_EF.AuthenticationProvider;
using Blazor_JTW_EF.Common;
using Blazor_JTW_EF.DatabaseContext;
using Blazor_JTW_EF.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blazor_JTW_EF.Services.UserAccountServices
{
    public class UserAccountService : Controller, IUserAccountService
    {
        private readonly IDbContextFactory<AppDatabaseContext> _contextFactory;
        private readonly ILocalStorageService _localStorage;
        public UserAccountService(IDbContextFactory<AppDatabaseContext> contextFactory, ILocalStorageService localStorange)
        {
            _contextFactory = contextFactory;
            _localStorage = localStorange;
        }

        public async Task<List<UserAccountModel>> GetAccounts()
        {
            using (var dbContext = _contextFactory.CreateDbContext())
            {
                return await dbContext.Accounts.ToListAsync();
            }
        }
        public async Task<AuthTokenResponse> Authenticate(string username, string password)
        {
            using (var dbContext = _contextFactory.CreateDbContext())
            {
                var user = await dbContext.Accounts.FirstOrDefaultAsync(a => a.UserName == username && a.Password == password);
                if (user == null)
                    return null;

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
                return new AuthTokenResponse(user, token);
            }
        }
        
    }
}
