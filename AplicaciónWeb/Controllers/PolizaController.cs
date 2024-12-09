using LogicaNegocio;
using EntidadesProyecto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Data.SqlClient;

namespace AplicaciónWeb.Controllers
{
    public class PolizaController : Controller
    {
        private readonly PolizaLN _polizaLN;
        private readonly ILogger<PolizaController> _logger;

        public PolizaController(PolizaLN polizaLN, ILogger<PolizaController> logger)
        {
            _polizaLN = polizaLN;
            _logger = logger;
        }

        // GET: Muestra la vista para crear una nueva póliza
        public IActionResult CrearPoliza(int idBeneficiario)
        {
            _logger.LogInformation("Iniciando la creación de una nueva póliza para el beneficiario con ID: {IdBeneficiario}", idBeneficiario);

            if (idBeneficiario == 0)
            {
                _logger.LogWarning("No se proporcionó un id válido para el beneficiario.");
                return View("Error", "No se proporcionó un id válido para el beneficiario");
            }

            // Obtener los tipos de póliza disponibles sincrónicamente
            var tiposPoliza = _polizaLN.ObtenerTipoPolizaAsync().Result;

            ViewBag.TiposPoliza = tiposPoliza;
            ViewBag.IdBeneficiario = idBeneficiario;

            return View();
        }


        public async Task<IActionResult> ValidarPoliza(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                ModelState.AddModelError("", "Por favor ingrese un número de póliza o DNI.");
                return View(new List<PolizaConTipo>());
            }

            var polizas = await _polizaLN.ObtenerPolizasPorFiltroAsync(filtro);

            if (!polizas.Any())
            {
                ViewBag.Message = "No se encontraron pólizas con el filtro proporcionado.";
            }

            return View(polizas);
        }

        public async Task<IActionResult> DetallePoliza(int id)
        {
            var poliza = await _polizaLN.ObtenerPolizasPorFiltroAsync(id.ToString());
            if (poliza == null || !poliza.Any())
            {
                return NotFound("Póliza no encontrada.");
            }

            return PartialView("_DetallePoliza", poliza.First());
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearPoliza(int idBeneficiario, int idTipoPoliza, DateTime fechaInicio, DateTime fechaFin, string estado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Registrar la póliza y obtener su ID de manera síncrona
                    int idPoliza = _polizaLN.CrearPoliza(idBeneficiario, idTipoPoliza, fechaInicio, fechaFin, estado);
                    _logger.LogInformation("Póliza creada con ID: {IdPoliza}", idPoliza);

                    if (idPoliza <= 0)
                    {
                        throw new Exception("No se pudo registrar la póliza.");
                    }

                    // Asignar póliza al beneficiario
                    _polizaLN.AsignarPolizaABeneficiario(idPoliza, idBeneficiario);
                    _logger.LogInformation("Póliza con ID: {IdPoliza} asignada al beneficiario con ID: {IdBeneficiario}", idPoliza, idBeneficiario);

                    TempData["SuccessMessage"] = "Póliza creada y asignada correctamente.";
                    return RedirectToAction("SuccessMessage");
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Error de base de datos al crear la póliza.");
                    ModelState.AddModelError("", "Error de base de datos: " + ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error general al crear la póliza.");
                    ModelState.AddModelError("", "Error general: " + ex.Message);
                }
            }

            // Recargar Tipos de Póliza si hay un error
            ViewBag.TiposPoliza = _polizaLN.ObtenerTipoPolizaAsync().Result;
            ViewBag.IdBeneficiario = idBeneficiario;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidarPoliza([FromBody] int idPoliza)
        {
            await _polizaLN.ActualizarEstadoPolizaAsync(idPoliza, "Activo");
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> NoValidarPoliza([FromBody] int idPoliza)
        {
            await _polizaLN.ActualizarEstadoPolizaAsync(idPoliza, "Inactivo");
            return Json(new { success = true });
        }

        public IActionResult SuccessMessage()
        {
            // Recuperar el mensaje de TempData
            ViewBag.Message = TempData["SuccessMessage"];
            return View();
        }

    }
}
