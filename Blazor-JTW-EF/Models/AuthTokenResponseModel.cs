namespace Blazor_JTW_EF.Models
{
    public class AuthTokenResponseModel
    {
        public UserAccountModel User { get; set; }
        public string Token { get; set; } = string.Empty;

        public AuthTokenResponseModel(UserAccountModel user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
