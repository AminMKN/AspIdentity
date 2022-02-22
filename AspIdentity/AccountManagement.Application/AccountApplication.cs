using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using _01_Framework.Application.Email;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.Account;
using AccountManagement.Infrastructure.Customize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IEmailSender _emailSender;
        private readonly IAuthHelper _authHelper;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LinkGenerator _linkGenerator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountApplication(IEmailSender emailSender, IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthHelper authHelper, IAccountRepository accountRepository, IFileUploader fileUploader)
        {
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            _linkGenerator = linkGenerator;
            _userManager = userManager;
            _signInManager = signInManager;
            _authHelper = authHelper;
            _accountRepository = accountRepository;
            _fileUploader = fileUploader;
        }

        public async Task<SignInResult> SignIn(SignInAccount command)
        {
            return await _signInManager.PasswordSignInAsync(command.UserName, command.Password, command.RememberMe, false);
        }

        public async Task<IdentityResult> SignUp(SignUpAccount command)
        {
            var user = new ApplicationUser()
            {
                UserName = command.UserName,
                Email = command.Email
            };

            return await _userManager.CreateAsync(user, command.Password);
        }

        public async Task<IdentityResult> EditAccount(EditAccount command)
        {
            var user = await _userManager.FindByIdAsync(command.Id);
            user.Email = command.Email;
            user.UserName = command.UserName;
            user.PhoneNumber = command.PhoneNumber;
            if (command.ProfilePhoto != null)
            {
                var profilePhotoPath = $"{"Users"}/{user.UserName}";
                var profilePhoto = _fileUploader.Upload(command.ProfilePhoto, profilePhotoPath);
                user.ProfilePhoto = profilePhoto;
            }
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ResetPassword(ResetPassword command)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            return await _userManager.ResetPasswordAsync(user, command.Token, command.Password);
        }

        public async Task<OperationResult> ForgotPassword(ForgotPassword command)
        {
            var operation = new OperationResult();
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user != null)
            {
                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var emailMessage = _linkGenerator.GetUriByPage(_contextAccessor.HttpContext, "/Account/ResetPassword", "",
                     new { email = user.Email, token = resetPasswordToken }, _contextAccessor.HttpContext.Request.Scheme);

                await _emailSender.SendEmail(user.Email, "تغییر کلمه عبور",
                    $"<a href='{emailMessage}' style='display: block; width: 120px; height: 25px; background: #9dcc1b; padding: 10px; text-align: center; border-radius: 50px; color: black; font-weight: bold; line-height: 25px;'>تغییر کلمه عبور</a>", true);
            }

            return operation.Success(ApplicationMessages.ForgotPasswordEmailSend);
        }

        public async Task<OperationResult> ConfirmEmail(string userName, string token)
        {
            var operation = new OperationResult();
            if (userName != null && token != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, token);
                    if (result.Succeeded)
                    {
                        return operation.Success();
                    }
                }
            }

            return operation.Failed(ApplicationMessages.RecordNotFound);
        }

        public async Task<OperationResult> SendEmailConfirmation()
        {
            var operation = new OperationResult();
            var user = await _userManager.FindByIdAsync(_authHelper.GetCurrentAccountId());
            if (user != null)
            {
                var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var emailMessage = _linkGenerator.GetUriByPage(_contextAccessor.HttpContext, "/Account/Profile/Index", "ConfirmEmail",
                     new { userName = user.UserName, token = emailConfirmToken }, _contextAccessor.HttpContext.Request.Scheme);

                await _emailSender.SendEmail(user.Email, "فعال سازی",
                    $"<a href='{emailMessage}' style='display: block; width: 120px; height: 25px; background: #9dcc1b; padding: 10px; text-align: center; border-radius: 50px; color: black; font-weight: bold; line-height: 25px;'>فعال سازی</a>", true);
            }

            return operation.Success(ApplicationMessages.EmailConfirmationSent);
        }

        public async Task<EditAccount> GetDetails(string id)
        {
            return await _accountRepository.GetDetails(id);
        }

        public async Task<AccountViewModel> GetUserInfo()
        {
            return await _accountRepository.GetUserInfo();
        }

        public async Task<List<AccountViewModel>> Search(AccountSearchModel searchModel)
        {
            return await _accountRepository.Search(searchModel);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}