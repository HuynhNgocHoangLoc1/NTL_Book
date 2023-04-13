using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NTL_Book.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("NTL_BookDbContextConnection") ?? throw new InvalidOperationException("Connection string 'NTL_BookDbContextConnection' not found.");

builder.Services.AddDbContext<NTL_BookDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<NTL_BookUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<NTL_BookDbContext>();

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