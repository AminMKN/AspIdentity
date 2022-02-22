using AccountManagement.Infrastructure.Customize;
using AccountManagement.Infrastructure.EFCore.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore
{
    public class AccountContext : IdentityDbContext<ApplicationUser>
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var assembly = typeof(ApplicationUserMapping).Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);
            SeedUsers(builder);
            SeedUserClaims(builder);
            base.OnModelCreating(builder);
        }

        private static void SeedUsers(ModelBuilder builder)
        {
            var user = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "OwnerSite",
                NormalizedUserName = "OWNERSITE",
                Email = "aspemail007@gmail.com",
                NormalizedEmail = "ASPEMAIL007@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEBBK9Bg51jFDirDTHa87eP8x6avQiHafGcOur3xOddi1NCf1hN+xXh4uc9HrwwHpbA==",
                PhoneNumber = "09876543210"
            };

            builder.Entity<ApplicationUser>().HasData(user);
        }

        private static void SeedUserClaims(ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>()
                {
                    Id = 1,
                    ClaimType = "مدیریت کاربران",
                    ClaimValue = true.ToString(),
                    UserId = "b74ddd14-6340-4840-95c2-db12554843e5"
                });
        }
    }
}
