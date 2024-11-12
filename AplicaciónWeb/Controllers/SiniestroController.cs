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

        public SiniestroController(SiniestroLN siniestroLN)
        {
            _siniestroLN = siniestroLN;
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
                    IdTaller =  4,
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
    }
}
