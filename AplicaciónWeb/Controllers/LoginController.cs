using Entidades;
using LogicaNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AplicaciónWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioLN _usuarioLN;

        public LoginController(UsuarioLN usuarioLN)
        {
            _usuarioLN = usuarioLN;
        }
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string dni, string password)
        {
            var usuario = _usuarioLN.LoginUser(dni, password);

            if (usuario != null)
            {
                // Verificar el tipo de usuario y redirigir a la interfaz correspondiente
                if (usuario is Beneficiario)
                {
                    return RedirectToAction("BeneficiarioDashboard", "Beneficiario");
                }
                else if (usuario is Personal)
                {
                    return RedirectToAction("PersonalDashboard", "Personal");
                }
                else if (usuario is Administrador)
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
            }

            // Si las credenciales son incorrectas, mostrar un mensaje de error
            ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
            return View();
        }

    }
}
