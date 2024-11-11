using AplicaciónWeb.Models;
using MiAplicacion.Data;
using MiAplicacion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AplicaciónWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var personalList = _context.Personal.ToList();
            Console.WriteLine("Total registros en Personal: " + personalList.Count);

            foreach (var personal in personalList)
            {
                Console.WriteLine("IdUsuario: " + personal.IdUsuario + ", Contraseña: " + personal.Contraseña);
            }

            var model = new HomeViewModel
            {
                UsuariosTotales = _context.Usuarios.Count(),
                BeneficiariosTotales = _context.Beneficiarios.Count(),
                PersonalTotal = _context.Personal.Count(),
                AdministradoresTotales = _context.Administradores.Count(),
                SiniestrosTotales = _context.Siniestros.Count(),
                VehiculosTotales = _context.Vehiculos.Count()
            };

            return View(model);
        }

        public IActionResult Login()
        {
            // Lógica para mostrar la vista de inicio de sesión.
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Aquí iría la lógica para cerrar sesión
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
