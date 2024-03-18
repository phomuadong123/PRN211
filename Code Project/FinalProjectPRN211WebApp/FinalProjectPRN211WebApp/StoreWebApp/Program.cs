using StoreWebApp.CustomHandler;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.Cookie.Name = "UserLoginCookie";
                    config.LoginPath = "/Login";
                    config.AccessDeniedPath = "/Login/UserAccessDenied";
                });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("UserPolicy", policyBuilder =>
    {
        policyBuilder.RequireClaim(ClaimTypes.Name);
    });
});

builder.Services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();

builder.Services.AddDistributedMemoryCache(); // Store Cache in Memory
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
