using AccountManagement.Application.Contracts.UserClaim;
using AccountManagement.Domain.UserClaim;
using AccountManagement.Infrastructure.Customize;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AccountManagement.Application
{
    public class UserClaimApplication : IUserClaimApplication
    {
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserClaimApplication(UserManager<ApplicationUser> userManager, IUserClaimRepository userClaimRepository)
        {
            _userManager = userManager;
            _userClaimRepository = userClaimRepository;
        }

        public async Task<IdentityResult> AddClaim(AddOrRemoveClaim command)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            var requestClaims = command.UserClaims
                .Where(x => x.IsSelected)
                .Select(x => new Claim(x.ClaimName, true.ToString())).ToList();
            return await _userManager.AddClaimsAsync(user, requestClaims);
        }

        public async Task<IdentityResult> RemoveClaim(AddOrRemoveClaim command)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            var requestClaims = command.UserClaims
                .Where(x => x.IsSelected)
                .Select(x => new Claim(x.ClaimName, true.ToString())).ToList();
            return await _userManager.RemoveClaimsAsync(user, requestClaims);
        }

        public async Task<AddOrRemoveClaim> GetClaimDetailsForAdd(string id)
        {
            return await _userClaimRepository.GetClaimDetailsForAdd(id);
        }

        public async Task<AddOrRemoveClaim> GetClaimDetailsForRemove(string id)
        {
            return await _userClaimRepository.GetClaimDetailsForRemove(id);
        }
    }
}
