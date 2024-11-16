using AccesoDatos;
using AplicaciónWeb.Models;
using EntidadesProyecto;
using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicaciónWeb.Controllers
{
    public class SiniestroController : Controller
    {
        private readonly SiniestroLN _siniestroLN;
        private readonly TallerLN _tallerLN; // Para trabajar con los talleres

        public SiniestroController(SiniestroLN siniestroLN, TallerLN tallerLN)
        {
            _siniestroLN = siniestroLN;
            _tallerLN = tallerLN;
        }

        // GET: Muestra la vista para registrar un siniestro
        public async Task<IActionResult> RegistrarSiniestro()
        {
            await CargarListasAsync();
            return View();
        }

        // POST: Procesa el formulario de registro de siniestro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarSiniestro(SiniestroViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Convertir ViewModel a entidad `Siniestro`
                var siniestro = new Siniestro
                {
                    IdDepartamento = model.IdDepartamento,
                    IdProvincia = model.IdProvincia,
                    IdDistrito = model.IdDistrito,
                    IdDocumento = 2, // Valores por defecto
                    IdPoliza = 5,
                    IdTaller =  1,
                    IdPresupuesto = 1,
                    Tipo = model.Tipo,
                    FechaSiniestro = model.FechaSiniestro,
                    Ubicacion = model.Ubicacion,
                    Descripcion = model.Descripcion
                };

                try
                {
                    await _siniestroLN.RegistrarSiniestro(siniestro);
                    ViewBag.Message = "Siniestro registrado con éxito.";
                    return RedirectToAction("Confirmacion");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }

            await CargarListasAsync();
            return View(model);
        }


        // Método AJAX para obtener provincias por departamento seleccionado
        [HttpGet]
        public async Task<JsonResult> GetProvincias(int departamentoId)
        {
            var provincias = await _siniestroLN.ObtenerProvinciasPorDepartamentoAsync(departamentoId);
            return Json(provincias.Select(p => new { id = p.Id, nombre = p.Descripcion }));
        }

        // Método AJAX para obtener distritos por provincia seleccionada
        [HttpGet]
        public async Task<JsonResult> GetDistritos(int provinciaId)
        {
            var distritos = await _siniestroLN.ObtenerDistritosPorProvinciaAsync(provinciaId);
            return Json(distritos.Select(d => new { id = d.Id, nombre = d.Descripcion }));
        }

        // Método para cargar la lista de departamentos en la vista
        private async Task CargarListasAsync()
        {
            var departamentos = await _siniestroLN.ObtenerDepartamentosAsync();
            ViewBag.Departamentos = new SelectList(departamentos, "Id", "Descripcion");

            ViewBag.Departamentos = departamentos;
            ViewBag.Provincias = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una provincia", Value = "" } };
            ViewBag.Distritos = new List<SelectListItem> { new SelectListItem { Text = "Seleccione un distrito", Value = "" } };
        }

        // Vista de confirmación de registro exitoso
        public IActionResult Confirmacion()
        {
            TempData["SuccessMessage"] = "Siniestro registrado exitosamente";

            return View();
        }



        // GET: Asignar taller a un siniestro
        public async Task<IActionResult> AsignarTaller()
        {
            int idTallerPorDefecto = 1; // Taller por defecto
            var siniestros = await _siniestroLN.ObtenerSiniestrosConTallerPorDefectoAsync(idTallerPorDefecto);

            return View(siniestros); // Renderiza vista con siniestros filtrados
        }


        // GET: Detalles del siniestro para asignar taller
        public async Task<IActionResult> DetalleSiniestro(int id)
        {
            var siniestro = await _siniestroLN.ObtenerSiniestroPorIdAsync(id);
            if (siniestro == null)
            {
                return NotFound("Siniestro no encontrado.");
            }

            var talleres = _tallerLN.ObtenerTodosLosTalleres();
            if (talleres == null || !talleres.Any())
            {
                ModelState.AddModelError("", "No hay talleres disponibles.");
                return PartialView("_DetalleSiniestro", siniestro);
            }

            ViewBag.Talleres = talleres.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nombre
            }).ToList();

            return PartialView("_DetalleSiniestro", siniestro);
        }




        // POST: Confirmar asignación de taller
        // POST: Confirmar asignación de taller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarAsignacion(int idSiniestro, int idTaller)
        {
            if (idSiniestro <= 0 || idTaller <= 0)
            {
                TempData["ErrorMessage"] = "Valores inválidos para la asignación.";
                return RedirectToAction("AsignarTaller");
            }

            try
            {
                var siniestro = await _siniestroLN.ObtenerSiniestroPorIdAsync(idSiniestro);
                if (siniestro == null)
                {
                    TempData["ErrorMessage"] = "Siniestro no encontrado.";
                    return RedirectToAction("AsignarTaller");
                }

                siniestro.IdTaller = idTaller;

                await _siniestroLN.ActualizarSiniestroAsync(siniestro);
                TempData["SuccessMessage"] = "Taller asignado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al asignar el taller: " + ex.Message;
            }

            return RedirectToAction("AsignarTaller");
        }






    }
}
