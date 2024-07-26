using BusinessLayer;
using BusinessLayer.Implementation;
using BusinessLayer.Implementation.PasswordEncoder;
using DataLayer;
using DataLayer.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Progetto_settimanale_22_07___26_07;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .RegisterDAOs()
    .AddScoped<DbContext>()
    .AddScoped<IAccountService, AccountService>()
    .AddScoped<IPasswordEncoder, NoOpPasswordEncoder>()
    .AddControllersWithViews()
    ;

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => opt.LoginPath = "/Account/Login")
    ;

builder.Services.AddAuthorization(opt =>
    opt.AddPolicy(Policies.isLogged, cfg => cfg.RequireAuthenticatedUser())
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
