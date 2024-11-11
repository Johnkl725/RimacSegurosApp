using AccesoDatos;
using MiAplicacion.Data;
using LogicaNegocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<UsuarioLN>();
builder.Services.AddScoped<UsuarioDA>();
builder.Services.AddScoped<SiniestroLN>();
builder.Services.AddScoped<SiniestroDA>();
builder.Services.AddScoped<BeneficiarioLN>();  // A�adido
builder.Services.AddScoped<BeneficiarioDA>();  // A�adido
builder.Services.AddScoped<VehiculoLN>();     // A�adido
builder.Services.AddScoped<VehiculoDA>();     // A�adido


// Configura la autenticaci�n usando cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";  // Ruta de inicio de sesi�n
        options.LogoutPath = "/Login/Logout"; // Ruta de cierre de sesi�n
    });

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

// Agrega el middleware de autenticaci�n antes de autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
