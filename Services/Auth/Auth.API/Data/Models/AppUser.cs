using Microsoft.AspNetCore.Identity;

namespace Auth.API.Data.Models
{
    public class AppUser : IdentityUser<string>
    {
        public AppUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string FullName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
    }
}
