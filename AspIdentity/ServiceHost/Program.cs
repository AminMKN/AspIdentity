using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using _01_Framework.Application.Email;
using AccountManagement.Application.Contracts.UserClaim;
using AccountManagement.Infrastructure.Configuration;
using ServiceHost;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("IdentityDB");
AccountManagementBootstrapper.Configure(builder.Services, connectionString);
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("UserManagementPolicy", policy => policy.RequireClaim(ClaimTypesStore.UserManagement));
});

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
