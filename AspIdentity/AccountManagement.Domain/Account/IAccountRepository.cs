using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.Account
{
    public interface IAccountRepository
    {
        Task<AccountViewModel> GetUserInfo();
        Task<EditAccount> GetDetails(string id);
        Task<List<AccountViewModel>> Search(AccountSearchModel searchModel);
    }
}
