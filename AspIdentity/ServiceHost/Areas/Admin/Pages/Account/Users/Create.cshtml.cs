using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Account.Users
{
    [Authorize(Policy = "UserManagementPolicy")]
    public class CreateModel : PageModel
    {
        public SignUpAccount Command;
        private readonly IAccountApplication _accountApplication;

        public CreateModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(SignUpAccount command)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _accountApplication.SignUp(command);
            if (result.Succeeded)
                return RedirectToPage("./Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
