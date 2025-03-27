using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain;
using OrderTrackingSystem.Services.Interfaces;
using OrderTrackingSystem.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfiguracja Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Rejestracja serwisów
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IShipmentService, ShipmentService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IComplaintService, ComplaintService>();
builder.Services.AddTransient<IVoucherService, VoucherService>();

// Dodanie kontrolerów i widoków
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Inicjalizacja ról i testowego sprzedawcy
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleInitializer.InitializeAsync(userManager, roleManager);
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
