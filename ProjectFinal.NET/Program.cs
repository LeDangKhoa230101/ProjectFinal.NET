using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProjectFinal.NET.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DotnetContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dotnet"));
});
builder.Services.AddAuthentication
	(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
		options.LoginPath = "/User/Login";
		options.AccessDeniedPath = "/AccessDenied"; }
	);

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
