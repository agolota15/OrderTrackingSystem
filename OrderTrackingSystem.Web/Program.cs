using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Services;
using OrderTrackingSystem.Services.Interfaces;
using OrderTrackingSystem.Web.Hubs;
<<<<<<< HEAD
using OrderTrackingSystem.Web;
=======
using OrderTrackingSystem.Web; // dla seederów
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja bazy danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

<<<<<<< HEAD
// Konfiguracja Identity z rolami
=======
// Konfiguracja Identity
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Rejestracja usług
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEmailService, EmailService>();

<<<<<<< HEAD
// Dodanie MVC, widoków i SignalR
=======
// Dodanie kontrolerów, widoków oraz SignalR
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

<<<<<<< HEAD
// Seeding ról i administratora
=======
// Seeding ról i konta administratora
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleInitializer.SeedRolesAndAdminAsync(services);
}

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
    pattern: "{controller=Order}/{action=Index}/{id?}");

// Mapowanie SignalR Huba
app.MapHub<OrderHub>("/orderHub");

app.Run();
