using EduHome.DAL;
using EduHome.Models;
using EduHome.Services.Implementations;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddIdentity<AppUser, IdentityRole>(identityOption =>
{
    identityOption.Password.RequiredLength = 5;
    identityOption.Password.RequireNonAlphanumeric = true;
    identityOption.Password.RequireDigit = true;
    identityOption.Password.RequireUppercase = true;
    identityOption.Password.RequireLowercase = true;

    identityOption.User.RequireUniqueEmail = true;

    identityOption.SignIn.RequireConfirmedEmail = true;
    identityOption.Lockout.MaxFailedAccessAttempts = 3;
    identityOption.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    identityOption.Lockout.AllowedForNewUsers = true;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

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
          name: "areas",
          pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
        );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
