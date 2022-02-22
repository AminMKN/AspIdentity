using AccountManagement.Application.Contracts.UserClaim;
using AccountManagement.Domain.UserClaim;
using AccountManagement.Infrastructure.Customize;
using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class UserClaimRepository : IUserClaimRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserClaimRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddOrRemoveClaim> GetClaimDetailsForAdd(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var claims = ClaimStore.AllClaims;
            var userClaims = await _userManager.GetClaimsAsync(user);
            var validClaims = claims.Where(x => userClaims.All(c => c.Type != x.Type))
                .Select(x => new UserClaim()
                {
                    ClaimName = x.Type
                }).ToList();

            return new AddOrRemoveClaim()
            {
                UserId = id,
                UserClaims = validClaims
            };
        }

        public async Task<AddOrRemoveClaim> GetClaimDetailsForRemove(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var claims = ClaimStore.AllClaims;
            var userClaims = await _userManager.GetClaimsAsync(user);
            var validClaims = userClaims
                .Select(x => new UserClaim()
                {
                    ClaimName = x.Type
                }).ToList();

            return new AddOrRemoveClaim()
            {
                UserId = id,
                UserClaims = validClaims
            };
        }
    }
}
