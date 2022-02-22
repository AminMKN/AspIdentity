using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Account.Users
{
    [Authorize(Policy = "UserManagementPolicy")]
    public class EditModel : PageModel
    {
        public EditAccount Command;
        private readonly IAccountApplication _accountApplication;

        public EditModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public async Task OnGet(string id)
        {
            Command = await _accountApplication.GetDetails(id);
        }

        public async Task<IActionResult> OnPost(EditAccount command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet(command.Id);
                return Page();
            }

            var result = await _accountApplication.EditAccount(command);
            if (result.Succeeded)
                return RedirectToPage("./Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await OnGet(command.Id);
            return Page();
        }
    }
}
