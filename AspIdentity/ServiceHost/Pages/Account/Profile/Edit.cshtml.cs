using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account.Profile
{
    [Authorize]
    public class EditModel : PageModel
    {
        public EditAccount Command;
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;

        public EditModel(IAuthHelper authHelper, IAccountApplication accountApplication)
        {
            _authHelper = authHelper;
            _accountApplication = accountApplication;
        }

        public async Task OnGet()
        {
            Command = await _accountApplication.GetDetails(_authHelper.GetCurrentAccountId());
        }

        public async Task<IActionResult> OnPost(EditAccount command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet();
                return Page();
            }

            var result = await _accountApplication.EditAccount(command);
            if (result.Succeeded)
                return RedirectToPage("/Account/Profile/Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await OnGet();
            return Page();
        }
    }
}
