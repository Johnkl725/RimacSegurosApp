using EntidadesProyecto;
using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
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
        public async Task<IActionResult> RegistrarSiniestro(Siniestro siniestro)
        {
            siniestro.IdDocumento ??= 1;
            siniestro.IdPoliza ??= 1;
            siniestro.IdTaller ??= 1;
            siniestro.IdPresupuesto ??= 1;

            if (ModelState.IsValid)
            {
                try
                {
                    await _siniestroLN.RegistrarSiniestro(siniestro);
                    ViewBag.Message = "Siniestro registrado con éxito.";
                    return RedirectToAction("Confirmacion"); // Redirigir a una página de confirmación
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }

            await CargarListasAsync();
            return View(siniestro);
        }

        // Método para obtener provincias por departamento
        [HttpGet]
        public async Task<JsonResult> ObtenerProvincias(int idDepartamento)
        {
            var provincias = await _siniestroLN.ObtenerProvinciasPorDepartamentoAsync(idDepartamento);
            return Json(provincias);
        }

        // Método para obtener distritos por provincia
        [HttpGet]
        public async Task<JsonResult> ObtenerDistritos(int idProvincia)
        {
            var distritos = await _siniestroLN.ObtenerDistritosPorProvinciaAsync(idProvincia);
            return Json(distritos);
        }

        // Método para cargar las listas de departamentos, provincias y distritos en la vista
        private async Task CargarListasAsync()
        {
            var departamentos = await _siniestroLN.ObtenerDepartamentosAsync();
            ViewBag.Departamentos = departamentos; // Asignamos una lista de `Departamento`
            ViewBag.Provincias = new List<Provincia>();
            ViewBag.Distritos = new List<Distrito>();
        }

        // Vista de confirmación de registro exitoso
        public IActionResult Confirmacion()
        {
            return View();
        }
    }
}
