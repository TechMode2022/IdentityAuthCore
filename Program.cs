using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityAuthCore.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityAuthCoreDBContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityAuthCoreDBContextConnection' not found.");

builder.Services.AddDbContext<IdentityAuthCoreDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityAuthCoreUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityAuthCoreDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

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

app.UseAuthorization();



app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


