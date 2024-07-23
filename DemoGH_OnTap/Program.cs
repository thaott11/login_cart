using DemoGH_OnTap.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SD18406CartDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//khai báo d?ch v? cho session
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromSeconds(60); // khai báo kho?ng th?i gian ?? session timeout
    //n?u k th?c hi?n ti?p yêu c?u nào thì session s? bi h?t han trong 60s
    //n?u th?c hi?n ti?p thì b? ??m c?a sesion s? reset d? li?u l?u vào web server
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
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
