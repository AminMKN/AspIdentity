using _01_Framework.Application;
using AccountManagement.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AccountManagement.Domain.Account;
using AccountManagement.Domain.UserClaim;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.Extensions.DependencyInjection;
using AccountManagement.Infrastructure.Customize;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.UserClaim;
using AccountManagement.Infrastructure.EFCore.Repository;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services
                .AddIdentity<ApplicationUser, IdentityRole>(option =>
                {
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequiredUniqueChars = 0;
                    option.User.RequireUniqueEmail = true;
                    option.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<AccountContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie.Name = "AspIdentity";
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();

            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IUserClaimApplication, UserClaimApplication>();

            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
