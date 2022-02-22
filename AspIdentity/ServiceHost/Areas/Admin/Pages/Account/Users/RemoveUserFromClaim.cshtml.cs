using AccountManagement.Application.Contracts.UserClaim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Account.Users
{
    [Authorize(Policy = "UserManagementPolicy")]
    public class RemoveUserFromClaimModel : PageModel
    {
        public AddOrRemoveClaim Command;
        private readonly IUserClaimApplication _userClaimApplication;

        public RemoveUserFromClaimModel(IUserClaimApplication userClaimApplication)
        {
            _userClaimApplication = userClaimApplication;
        }

        public async Task OnGet(string id)
        {
            Command = await _userClaimApplication.GetClaimDetailsForRemove(id);
        }

        public async Task<IActionResult> OnPost(AddOrRemoveClaim command)
        {
            var result = await _userClaimApplication.RemoveClaim(command);
            if (result.Succeeded)
                return RedirectToPage("./Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await OnGet(command.UserId);
            return Page();
        }
    }
}
