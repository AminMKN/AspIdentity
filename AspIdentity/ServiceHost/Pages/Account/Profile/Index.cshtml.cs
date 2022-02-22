using _01_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account.Profile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public AccountViewModel Command;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public async Task OnGet()
        {
            Command = await _accountApplication.GetUserInfo();
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _accountApplication.SendEmailConfirmation();
            Message = result.Message;
            await OnGet();
            return Page();
        }

        public async Task<IActionResult> OnGetConfirmEmail(string userName, string token)
        {
            var result = await _accountApplication.ConfirmEmail(userName, token);
            if (result.IsSuccess)
            {
                Message = ApplicationMessages.EmailConfirmed;
                await OnGet();
                return Page();
            }

            return NotFound();
        }
    }
}
