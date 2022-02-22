using _01_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        public string Message { get; set; }
        public ResetPassword Command;
        private readonly IAccountApplication _accountApplication;

        public ResetPasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(string email, string token)
        {
            Command = new ResetPassword()
            {
                Email = email,
                Token = token
            };
        }

        public async Task<IActionResult> OnPost(ResetPassword command)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _accountApplication.ResetPassword(command);
            if (result.Succeeded)
            {
                Message = ApplicationMessages.ResetPasswordSuccessful;
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
