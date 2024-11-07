using EntidadesProyecto;
using LogicaNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AplicaciónWeb.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UsuarioLN _usuarioLN;

        public RegisterController(UsuarioLN usuarioLN)
        {
            _usuarioLN = usuarioLN;
        }

        // GET: Register
        public IActionResult Index()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string nombres, string apellido1, string apellido2, string dni, string telefono, string liderSogId, string password, string confirmPassword, string userType, string vehiclePlaca, string vehicleMarca, string vehicleModelo, string vehicleTipo, int vehicleTarjeta)
        {
            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Las contraseñas no coinciden.";
                return View();
            }

            // Hash the password before saving using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var usuario = _usuarioLN.RegisterUser(nombres, apellido1, apellido2, dni, telefono, liderSogId, hashedPassword, userType, vehiclePlaca, vehicleMarca, vehicleModelo, vehicleTipo, vehicleTarjeta);

            if (usuario != null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.ErrorMessage = "El registro ha fallado.";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPersonal(string nombres, string apellido1, string apellido2, string dni, string telefono, string liderSogId, string password, string confirmPassword, string userType)
        {
            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Las contraseñas no coinciden.";
                return View();
            }

            // Hash the password before saving using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var usuario = _usuarioLN.RegisterUser(nombres, apellido1, apellido2, dni, telefono, liderSogId, hashedPassword, userType);
            if (usuario != null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.ErrorMessage = "El registro ha fallado.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterAdministrador(string nombres, string apellido1, string apellido2, string dni, string telefono, string liderSogId, string password, string confirmPassword, string userType)
        {
            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Las contraseñas no coinciden.";
                return View();
            }

            // Hash the password before saving using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var usuario = _usuarioLN.RegisterUser(nombres, apellido1, apellido2, dni, telefono, liderSogId, hashedPassword, userType);
            if (usuario != null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.ErrorMessage = "El registro ha fallado.";
            return View();
        }


    }
}
