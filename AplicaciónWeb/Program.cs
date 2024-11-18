using AccesoDatos;
using MiAplicacion.Data;
using LogicaNegocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using EntidadesProyecto;

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
builder.Services.AddScoped<AdminDA>();
builder.Services.AddScoped<AdminLN>();
builder.Services.AddScoped<ProveedorDA>();
builder.Services.AddScoped<ProveedorLN>();
builder.Services.AddScoped<TallerDA>();
builder.Services.AddScoped<TallerLN>();


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

