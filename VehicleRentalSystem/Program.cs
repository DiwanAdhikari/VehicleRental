using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.Data;
using VehicleRentalSystem.Interface;
using VehicleRentalSystem.Repository;
using VehicleRentalSystem.Security;
using VehicleRentalSystem.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Configure the database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

// Configure Identity with ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add controllers and Razor Pages with runtime compilation
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Register application services
builder.Services.AddScoped<ICommon, CommonRepository>();
builder.Services.AddScoped<IUtility, Utilities>();

// Add Authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
// Map routes for controllers and areas
app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Default route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Enable Razor Pages
app.MapRazorPages();

app.Run();

#region Create Role and initial User Rergistered
void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
{
    var roleNames = new List<string>
    {
       
        UserRole.Admin,
        UserRole.User,
        UserRole.Renter,
        UserRole.Owner,
        
    };

    // Creating roles
    foreach (var role in roleNames)
    {
        CreateRole(serviceProvider, role);
    }

    // Administrator user Setup
    const string administratorUserEmail = "softech@gmail.com";
    const string administratorPwd = "Softech@123!";
    AddUserToRole(serviceProvider, new ApplicationUser()
    {
        FullName = UserRole.Admin,
        Email = administratorUserEmail,
        UserName = administratorUserEmail,
        EmailConfirmed = true,
        LockoutEnabled = false,
    }, administratorPwd, UserRole.Admin);
}

void CreateRole(IServiceProvider serviceProvider, string roleName)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roleExists = roleManager.RoleExistsAsync(roleName);
    roleExists.Wait();

    if (roleExists.Result) return;
    var roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
    roleResult.Wait();
}

void AddUserToRole(IServiceProvider serviceProvider, ApplicationUser user, string userPwd, string roleName)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    Task<ApplicationUser> checkAppUser = userManager.FindByEmailAsync(user.Email);
    checkAppUser.Wait();

    ApplicationUser appUser = checkAppUser.Result;

    if (checkAppUser.Result == null)
    {
        Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(user, userPwd);
        taskCreateAppUser.Wait();

        if (taskCreateAppUser.Result.Succeeded)
        {
            appUser = user;
        }
    }
    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
    newUserRole.Wait();
}
#endregion