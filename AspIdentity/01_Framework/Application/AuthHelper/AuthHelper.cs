using System.Security.Claims;
using AccountManagement.Infrastructure.Customize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace _01_Framework.Application.AuthHelper
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthHelper(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public string GetCurrentAccountId()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string GetCurrentAccountUserName()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public string GetCurrentAccountEmail()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }

        public async Task<string> GetUserName(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user.UserName;
        }
    }
}