using EntidadesProyecto;
using LogicaNegocio;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Index(string username, string password)
        {
            string tipo_usuario = _usuarioLN.LoginUser(username, password);

            if (tipo_usuario != null)
            {
                // Crear las claims del usuario
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("TipoUsuario", tipo_usuario)
        };

                // Crear la identidad del usuario y el principal
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Iniciar sesión con las cookies
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                // Redirigir a la interfaz correspondiente según el tipo de usuario
                switch (tipo_usuario)
                {
                    case "Beneficiario":
                        return RedirectToAction("BeneficiarioDashboard", "Beneficiario");
                    case "Personal":
                        return RedirectToAction("PersonalDashboard", "Personal");
                    case "Administrador":
                        return RedirectToAction("AdminDashboard", "Admin");
                    default:
                        ViewBag.ErrorMessage = "Tipo de usuario desconocido.";
                        return View();
                }
            }

            // Si las credenciales son incorrectas, mostrar un mensaje de error
            ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
            return View();
        }

        public IActionResult Perfil()
        {
            var username = User.Identity.Name;

            // Llama a ObtenerPerfilPorDni para obtener la información del usuario actual usando el DNI
            var perfilViewModel = _usuarioLN.ObtenerPerfilPorDni(username);

            if (perfilViewModel != null)
            {
                return PartialView("_PerfilPartial", perfilViewModel);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


    }
}
