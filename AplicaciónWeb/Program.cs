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
builder.Services.AddScoped<BeneficiarioLN>();  // Añadido
builder.Services.AddScoped<BeneficiarioDA>();  // Añadido
builder.Services.AddScoped<VehiculoLN>();     // Añadido
builder.Services.AddScoped<VehiculoDA>();     // Añadido


// Configura la autenticación usando cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";  // Ruta de inicio de sesión
        options.LogoutPath = "/Login/Logout"; // Ruta de cierre de sesión
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

// Agrega el middleware de autenticación antes de autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
