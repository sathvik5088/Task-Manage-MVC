using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPP.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;


var builder = WebApplication.CreateBuilder(args);

//// Get the connection string for Identity (TaskManagerAPPIdentityDbContextConnection)
//var connectionString = builder.Configuration.GetConnectionString("TaskManagerAPPIdentityDbContextConnection")
//    ?? throw new InvalidOperationException("Connection string 'TaskManagerAPPIdentityDbContextConnection' not found.");

// Add services to the container for regular application data (ApplicationDbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("dsc")));


// Add MVC controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//// Enable authentication and authorization middleware
//app.UseAuthentication();  // Ensure authentication comes before authorization
//app.UseAuthorization();

// Map Razor Pages (needed for Identity UI pages)
//app.MapRazorPages();

// Define the default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
