using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Infrastructure.Customize
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePhoto { get; set; }
    }
}