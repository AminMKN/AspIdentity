namespace _01_Framework.Application.AuthHelper
{
    public interface IAuthHelper
    {
        string GetCurrentAccountId();
        string GetCurrentAccountUserName();
        string GetCurrentAccountEmail();
        Task<string> GetUserName(string id);
    }
}
