using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor_JTW_EF.Models
{
    [Table("UserAccounts")]
    [PrimaryKey("UserId")]
    public class UserAccountModel : IdentityUser
    {
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Middlename { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }
}
