using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyTaskTracker.Data;
using MyTaskTracker.Interfaces;
using MyTaskTracker.Models;
using MyTaskTracker.Repository;
using RunGroopWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>(); 
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITaskRepository, TaskRepository>(); 

builder.Services.AddRazorPages();
builder.Services.AddDbContext<TaskTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AgonenConnection") ?? throw new InvalidOperationException("Connection string 'TaskTrackerDb' not found.")));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<TaskTrackerDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

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

//await Seed.SeedUsersAndRolesAsync(app);

app.Run();

