using Microsoft.EntityFrameworkCore;
using Amanoi.Infrastructure.Data;
using Amanoi.Web.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext và chỉ định assembly cho migrations
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Amanoi.Infrastructure") // Chỉ định assembly
    ));

//sử dụng các tính năng có sẳn của MVC
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Cấu hình pipeline xử lý yêu cầu HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Thêm nếu bạn đang sử dụng Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
