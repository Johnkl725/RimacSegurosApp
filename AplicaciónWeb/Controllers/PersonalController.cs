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
        [HttpGet]
        public IActionResult BuscarUsuarioPorDni(string dni)
        {
            if (string.IsNullOrEmpty(dni))
            {
                return Json(new { success = false, message = "El campo DNI está vacío." });
            }

            var usuarios = _usuarioLN.ObtenerUsuariosPorDni(dni);
            if (!usuarios.Any())
            {
                return Json(new { success = false, message = "No se encontraron usuarios con ese DNI." });
            }

            return Json(new { success = true, data = usuarios });
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

        public IActionResult MantenerUsuario()
        {
            var beneficiarios = _usuarioLN.ObtenerUsuariosPorTipo("beneficiario");
            return View(beneficiarios);
        }





        // GET: PersonalController/Edit/5
        // Acción para obtener los datos de un usuario y mostrarlos en la vista de edición
        public IActionResult EditUsuario(int id)
        {
            var usuario = _usuarioLN.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Acción para guardar los cambios realizados en la edición de un usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUsuario(int id, Usuario usuarioEditado)
        {
            if (id != usuarioEditado.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var actualizado = _usuarioLN.ActualizarUsuario(usuarioEditado);
                    if (actualizado)
                    {
                        TempData["Mensaje"] = "El usuario se actualizó correctamente.";
                        return RedirectToAction(nameof(MantenerUsuario));
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo actualizar el usuario. Inténtalo de nuevo.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }

            return View(usuarioEditado);
        }

        // Acción para confirmar la eliminación de un usuario
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _usuarioLN.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Acción para eliminar un usuario definitivamente
        [HttpPost, ActionName("DeleteUsuario")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDeleteUsuario(int id)
        {
            try
            {
                var eliminado = _usuarioLN.EliminarUsuario(id);
                if (eliminado)
                {
                    TempData["Mensaje"] = "El usuario fue eliminado correctamente.";
                    return RedirectToAction(nameof(MantenerUsuario));
                }
                else
                {
                    TempData["Error"] = "No se pudo eliminar el usuario.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
            }

            return RedirectToAction(nameof(MantenerUsuario));
        }

    }
}
