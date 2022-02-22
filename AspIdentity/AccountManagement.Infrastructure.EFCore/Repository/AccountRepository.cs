using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.Account;
using AccountManagement.Infrastructure.Customize;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAuthHelper _authHelper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(IAuthHelper authHelper, UserManager<ApplicationUser> userManager)
        {
            _authHelper = authHelper;
            _userManager = userManager;
        }

        public async Task<AccountViewModel> GetUserInfo()
        {
            var user = await _userManager.FindByIdAsync(_authHelper.GetCurrentAccountId());
            return new AccountViewModel()
            {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                ProfilePhoto = user.ProfilePhoto,
                EmailConfirmed = user.EmailConfirmed
            };
        }

        public async Task<EditAccount> GetDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return new EditAccount()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<List<AccountViewModel>> Search(AccountSearchModel searchModel)
        {
            var query = _userManager.Users.Select(x => new AccountViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                PhoneNumber = x.PhoneNumber,
                ProfilePhoto = x.ProfilePhoto,
                EmailConfirmed = x.EmailConfirmed
            });

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));

            return await query.OrderBy(x => x.UserName).AsNoTracking().ToListAsync();
        }
    }
}
