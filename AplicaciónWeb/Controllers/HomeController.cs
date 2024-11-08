using AplicaciónWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AplicaciónWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
