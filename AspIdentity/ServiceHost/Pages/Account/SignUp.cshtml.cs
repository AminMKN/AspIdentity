using _01_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Infrastructure.Customize;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account
{
    public class SignUpModel : PageModel
    {
        public string Message { get; set; }
        public SignUpAccount Command;
        private readonly IAccountApplication _accountApplication;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignUpModel(IAccountApplication accountApplication, SignInManager<ApplicationUser> signInManager)
        {
            _accountApplication = accountApplication;
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToPage("/Index");

            return Page();
        }

        public async Task<IActionResult> OnPost(SignUpAccount command)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _accountApplication.SignUp(command);
            if (result.Succeeded)
            {
                Message = ApplicationMessages.AccountCreated;
                return Page();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
