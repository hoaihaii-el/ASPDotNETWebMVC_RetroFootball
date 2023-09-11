using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;

var builder = WebApplication.CreateBuilder(args);

//connectionDB
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"]);
});

// Configure Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;

    options.User.RequireUniqueEmail = true;
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("Administrator"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// Verify account
app.UseAuthentication();

// Verify account role
app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Login}/{id?}");


// Create roles before run app
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
