using _01_Framework.Application;
using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        Task<SignInResult> SignIn(SignInAccount command);
        Task<IdentityResult> SignUp(SignUpAccount command);
        Task<IdentityResult> EditAccount(EditAccount command);
        Task<IdentityResult> ResetPassword(ResetPassword command);
        Task<OperationResult> ForgotPassword(ForgotPassword command);
        Task<OperationResult> ConfirmEmail(string userName, string token);
        Task<OperationResult> SendEmailConfirmation();
        Task<EditAccount> GetDetails(string id);
        Task<AccountViewModel> GetUserInfo();
        Task<List<AccountViewModel>> Search(AccountSearchModel searchModel);
        Task SignOut();
    }
}
