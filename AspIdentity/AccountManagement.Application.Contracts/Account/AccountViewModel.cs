namespace AccountManagement.Application.Contracts.Account
{
    public class AccountViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePhoto { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
