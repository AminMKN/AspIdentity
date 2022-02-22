using AccountManagement.Application.Contracts.Account;
using AccountManagement.Infrastructure.Customize;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
        public AccountViewModel Command;
        private readonly IAccountApplication _accountApplication;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(IAccountApplication accountApplication, SignInManager<ApplicationUser> signInManager)
        {
            _accountApplication = accountApplication;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                Command = await _accountApplication.GetUserInfo();
                return Page();
            }

            return Page();
        }
    }
}