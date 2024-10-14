namespace Blazor_JTW_EF.Models
{
    public class AuthTokenResponse
    {
        public UserAccountModel User { get; set; }
        public string Token { get; set; } = string.Empty;

        public AuthTokenResponse(UserAccountModel user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
