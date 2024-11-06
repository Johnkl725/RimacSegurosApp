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
        public IActionResult Register(string nombres, string apellido1, string apellido2, string dni, string telefono, string liderSogId, string password, string confirmPassword,string userType, string vehiclePlaca, string vehicleMarca, string vehicleModelo, string vehicleTipo, int vehicleTarjeta)
        {
            var usuario = _usuarioLN.RegisterUser(nombres, apellido1, apellido2, dni, telefono, liderSogId, password, userType, vehiclePlaca, vehicleMarca, vehicleModelo, vehicleTipo, vehicleTarjeta);

            if (usuario != null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.ErrorMessage = "El registro ha fallado.";
                return View("Index","Register");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPersonal(string nombres, string apellido1, string apellido2, string dni, string telefono, string liderSogId, string password, string confirmPassword, string userType)
        {
            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Las contraseñas no coinciden.";
                return View("Index", "Register");
            }

            var usuario = _usuarioLN.RegisterUser(nombres, apellido1, apellido2, dni, telefono, liderSogId, password, userType);
            if (usuario != null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.ErrorMessage = "El registro ha fallado.";
            return View("Index", "Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterAdministrador(string nombres, string apellido1, string apellido2, string dni, string telefono, string liderSogId, string password, string confirmPassword, string userType)
        {
            try
            {
                if (password != confirmPassword)
                {
                    ViewBag.ErrorMessage = "Las contraseñas no coinciden.";
                    return View("Index", "Register");
                }

                var usuario = _usuarioLN.RegisterUser(nombres, apellido1, apellido2, dni, telefono, liderSogId, password, userType);

                if (usuario != null)
                {
                    return RedirectToAction("Index", "Login");
                }

                ViewBag.ErrorMessage = "El registro ha fallado.";
                return View("Index", "Register");
            }
            catch (Exception ex)
            {
                // Registra el error o maneja el error de la manera que necesites
                ViewBag.ErrorMessage = "Ocurrió un error al registrar el administrador: " + ex.Message;
                return View("Index", "Register");
            }
        }



    }
}
