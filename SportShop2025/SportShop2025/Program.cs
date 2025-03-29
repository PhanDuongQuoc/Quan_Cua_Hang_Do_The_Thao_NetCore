using Microsoft.EntityFrameworkCore;
using SportShop2025.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ??ng ký DbContext
builder.Services.AddDbContext<SportShop2025Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportShop2025"));
});

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor(); 

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // ?? ??m b?o g?i UseSession() tr??c UseAuthorization()
app.UseAuthorization();

// C?u hình route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductHome}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
