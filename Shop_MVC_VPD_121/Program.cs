using Microsoft.EntityFrameworkCore;
using Data;
using Shop_MVC_VPD_121.Services;
using Microsoft.AspNetCore.Identity;
using DataAccess.Entities;
using System.Data;
using Shop_MVC_VPD_121.Models;

var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("LocalDb");

// Configure Dependency Injection (DI) services 

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopDbContext>(x => x.UseSqlServer(connStr));

builder.Services.AddIdentity<User, IdentityRole>()
               .AddDefaultTokenProviders()
               .AddDefaultUI()
               .AddEntityFrameworkStores<ShopDbContext>();

builder.Services.AddHttpContextAccessor();

// add custom services
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// seed admin user
using (var scope = app.Services.CreateScope())
{
    // seed roles
    var serviceProvider = scope.ServiceProvider;

    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    foreach (var role in Enum.GetNames(typeof(Roles)))
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // seed admin
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

    const string USERNAME = "myadmin@myadmin.com";
    const string PASSWORD = "Admin1@";

    var existingUser = userManager.FindByNameAsync(USERNAME).Result;

    if (existingUser == null)
    {
        var user = new User
        {
            UserName = USERNAME,
            Email = USERNAME,
        };

        var result = userManager.CreateAsync(user, PASSWORD).Result;
        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }
}


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
app.UseAuthentication();;

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
