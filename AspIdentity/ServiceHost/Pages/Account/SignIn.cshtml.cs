using _01_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Infrastructure.Customize;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account
{
    public class SignInModel : PageModel
    {
        public string Message { get; set; }
        public SignInAccount Command;
        private readonly IAccountApplication _accountApplication;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignInModel(IAccountApplication accountApplication, SignInManager<ApplicationUser> signInManager)
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

        public async Task<IActionResult> OnPost(SignInAccount command)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _accountApplication.SignIn(command);
            if (result.Succeeded)
                return RedirectToPage("/Index");

            Message = ApplicationMessages.UserNameOrPasswordNotValid;
            return Page();
        }

        public async Task<IActionResult> OnGetSignOut()
        {
            await _accountApplication.SignOut();
            return RedirectToPage("/Account/SignIn");
        }
    }
}
