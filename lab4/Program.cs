using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using lab4.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<lab4Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("lab4Context") ?? throw new InvalidOperationException("Connection string 'lab4Context' not found.")));
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(1); });


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=usersaccounts}/{action=login}/{id?}");

app.Run();
