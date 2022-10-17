using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using WowStats.Common.Config;
using WowStats.Common.Services;
using WowStats.Common.Services.Abstractions;
using WowStats.Common.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<WarshipsUserService>();
builder.Services.Configure<WargamingSettings>(builder.Configuration.GetSection("WargamingSettings"));
builder.Services.AddTransient<WargamingApiHelper>();
builder.Services.AddTransient(typeof(IWarshipsUserService), typeof(WarshipsUserService));

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

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=User}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "user",
	pattern: "{controller=User}/{action=Index}/{userId?}");

app.Run();
