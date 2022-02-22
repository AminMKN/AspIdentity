using AccountManagement.Application.Contracts.Account;
using AccountManagement.Infrastructure.Customize;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class AdminProfileNavbarViewComponent : ViewComponent
    {
        private readonly IAccountApplication _accountApplication;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminProfileNavbarViewComponent(IAccountApplication accountApplication, SignInManager<ApplicationUser> signInManager)
        {
            _accountApplication = accountApplication;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn((System.Security.Claims.ClaimsPrincipal)User))
            {
                var profile = await _accountApplication.GetUserInfo();
                return View(profile);
            }

            return View(new AccountViewModel());
        }
    }
}
