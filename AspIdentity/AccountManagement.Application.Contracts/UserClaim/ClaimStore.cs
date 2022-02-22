using System.Security.Claims;

namespace AccountManagement.Application.Contracts.UserClaim
{
    public static class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new(ClaimTypesStore.UserManagement, true.ToString())
        };
    }
}