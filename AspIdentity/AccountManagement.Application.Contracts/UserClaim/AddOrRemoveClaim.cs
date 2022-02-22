namespace AccountManagement.Application.Contracts.UserClaim
{
    public class AddOrRemoveClaim
    {
        public string UserId { get; set; }
        public List<UserClaim> UserClaims { get; set; }
    }
}