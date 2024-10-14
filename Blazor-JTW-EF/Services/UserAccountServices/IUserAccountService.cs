using Blazor_JTW_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blazor_JTW_EF.Services.UserAccountServices
{
    public interface IUserAccountService
    {
        Task<List<UserAccountModel>> GetAccounts();
        Task<AuthTokenResponse> Authenticate(string username, string password);
    }
}
