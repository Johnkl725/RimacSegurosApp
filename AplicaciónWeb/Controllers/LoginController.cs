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
            var (tipoUsuario, idUsuario) = _usuarioLN.LoginUser(username, password);
            if (!string.IsNullOrEmpty(tipoUsuario))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username), // DNI o nombre de usuario
            new Claim("TipoUsuario", tipoUsuario),
            new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()) // Almacena el IdUsuario
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return tipoUsuario switch
                {
                    "Beneficiario" => RedirectToAction("BeneficiarioDashboard", "Beneficiario"),
                    "Personal" => RedirectToAction("PersonalDashboard", "Personal"),
                    "Administrador" => RedirectToAction("AdminDashboard", "Admin"),
                    _ => View("Error")
                };
            }

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
