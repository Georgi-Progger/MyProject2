using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProdASP.Areas.Identity.Data;
using ProdASP.Data;
using ProdASP.Models;
using System.IO;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationContextConnection' not found.");

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(opts =>
{
    opts.Password.RequiredLength = 5; 
    opts.Password.RequireNonAlphanumeric = false;   
    opts.Password.RequireLowercase = false; 
    opts.Password.RequireUppercase = false; 
    opts.Password.RequireDigit = false; 
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();
builder.Services.AddAuthorization();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/AccessDenied";
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<ApplicationContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await ContextSeed.SeedRolesAsync(userManager, roleManager);
        await ContextSeed.SeedAdminAsync(userManager, roleManager);
        await Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

 static async Task Initialize(IServiceProvider serviceProvider)
{
    var context = serviceProvider.GetRequiredService<ApplicationContext>();
    if (!context.Places.Any())
    {
        context.Places.Add(new Country()
        {
            Id = 1,
            NamePlace = "Россия",
            Language = "русский",
            Information = "Какой-то текстКакой-то текстКакой-то текстКакой-то текстКакой-то текстКакой-то текст",
            Image = @"/images/russia.jpg",
        }); ;
        context.SaveChanges();
    }
}