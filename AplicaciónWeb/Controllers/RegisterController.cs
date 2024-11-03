using Entidades;
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
        public IActionResult Index(string nombres, string apellido1, string apellido2, string dni, string telefono, string liderSogId, string password, string userType)
        {
            bool isRegistered = _usuarioLN.RegisterUser(nombres, apellido1, apellido2, dni, telefono, liderSogId, password, userType);
            if (isRegistered)
            {
                // Registro exitoso, redirigir a la página de inicio de sesión
                return RedirectToAction("Index", "Login");
            }
            else
            {
                // Fallo en el registro, mostrar mensaje de error
                ViewBag.ErrorMessage = "El registro ha fallado.";
                return View();
            }
        }
    }
}
