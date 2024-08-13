using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Goal_Management_Web_App.Data;
using Goal_Management_Web_App.Infrastructure;
using Goal_Management_Web_App.Models;
using Goal_Management_Web_App.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<IActionStepRepository, ActionStepRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    

builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz.@";
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IUserValidator<User>, CustomEmailValidator>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//SeedData.EnsurePopulated(app);

SeedIdentityData.EnsurePopulated(app);



app.Run();
