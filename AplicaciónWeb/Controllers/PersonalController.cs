using AccesoDatos;
using EntidadesProyecto;
using LogicaNegocio;
using MiAplicacion.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace AplicaciónWeb.Controllers
{
    public class PersonalController : Controller
    {
        // GET: PersonalController
        private readonly UsuarioLN _usuarioLN;

        public PersonalController(UsuarioLN usuarioLN)
        {
            _usuarioLN = usuarioLN;
        }
        public ActionResult PersonalDashboard()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: PersonalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonalController/Create
        public IActionResult CreateUsuario()
        {
            return View();
        }

        // POST: PersonalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUsuario(string Nombres, string Apellido1, string Apellido2, string Dni, string Telefono, string Contraseña)
        {
            if (ModelState.IsValid)
            {
                // Hashear la contraseña
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Contraseña);

                // Llamar a la lógica de negocio para crear el usuario
                var usuarioCreado = _usuarioLN.CrearUsuario(
                    Nombres,
                    Apellido1,
                    Apellido2,
                    Dni,
                    Telefono,
                    hashedPassword,
                    "beneficiario" // Tipo de usuario
                );

                // Verificar si el usuario fue creado correctamente
                if (usuarioCreado != null)
                {
                    // Redirigir a la acción de crear vehículo y pasar el ID de beneficiario
                    return RedirectToAction("CrearVehiculo", "Beneficiario",usuarioCreado);
                }
                else
                {
                    // Si hubo un problema al crear el usuario, manejar el error (puedes agregar más lógica aquí)
                    ModelState.AddModelError("", "Hubo un error al crear el usuario.");
                    return View();
                }
            }

            // Si el modelo no es válido, regresar al formulario con los errores de validación
            return View();
        }




        // GET: PersonalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
