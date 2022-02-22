using AccountManagement.Application.Contracts.UserClaim;

namespace AccountManagement.Domain.UserClaim
{
    public interface IUserClaimRepository
    {
        Task<AddOrRemoveClaim> GetClaimDetailsForAdd(string id);
        Task<AddOrRemoveClaim> GetClaimDetailsForRemove(string id);
    }
}
