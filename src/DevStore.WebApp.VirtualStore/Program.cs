using DevStore.Catalog.Application.Mappings;
using DevStore.Catalog.Data;
using DevStore.Payment.Data;
using DevStore.Sales.Data;
using DevStore.WebApp.VirtualStore.Configurations;
using DevStore.WebApp.VirtualStore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<CatalogContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<SalesContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<PaymentContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(DomainToDTOMappingProfile), typeof(DTOToDomainMappingProfile));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.ResolveDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
