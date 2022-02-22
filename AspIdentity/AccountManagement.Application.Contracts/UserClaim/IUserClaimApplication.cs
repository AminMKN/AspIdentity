using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Application.Contracts.UserClaim
{
    public interface IUserClaimApplication
    {
        Task<IdentityResult> AddClaim(AddOrRemoveClaim command);
        Task<IdentityResult> RemoveClaim(AddOrRemoveClaim command);
        Task<AddOrRemoveClaim> GetClaimDetailsForAdd(string id);
        Task<AddOrRemoveClaim> GetClaimDetailsForRemove(string id);
    }
}
