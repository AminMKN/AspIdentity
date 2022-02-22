using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Account.Users
{
    [Authorize(Policy = "UserManagementPolicy")]
    public class IndexModel : PageModel
    {
        public AccountSearchModel SearchModel;
        public List<AccountViewModel> Users;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public async Task OnGet(AccountSearchModel searchModel)
        {
            Users = await _accountApplication.Search(searchModel);
        }
    }
}
