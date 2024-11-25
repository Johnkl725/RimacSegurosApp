using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using EntidadesProyecto;
using System.Threading.Tasks;
using AccesoDatos;
using System.Linq;
using System;

namespace AplicaciónWeb.Controllers
{
    public class SeguimientoController : Controller
    {
        private readonly SiniestroLN _siniestroLN;
        private readonly ReclamacionLN _reclamacionLN;

        public SeguimientoController(SiniestroLN siniestroLN, ReclamacionLN reclamacionLN)
        {
            _siniestroLN = siniestroLN;
            _reclamacionLN = reclamacionLN;
        }

        [HttpGet]
        public async Task<IActionResult> GestionarSeguimiento()
        {
            try
            {
                // Obtener el ID del usuario autenticado
                int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
                Console.WriteLine($"ID Usuario autenticado: {idUsuario}");

                if (idUsuario == 0)
                {
                    TempData["ErrorMessage"] = "No se pudo identificar al usuario autenticado.";
                    return RedirectToAction("Error");
                }

                // Obtener el ID del beneficiario asociado al usuario autenticado
                var idBeneficiario = _reclamacionLN.ObtenerIdBeneficiarioPorUsuario(idUsuario);
                Console.WriteLine($"ID Beneficiario asociado: {idBeneficiario}");

                if (idBeneficiario <= 0)
                {
                    TempData["ErrorMessage"] = "No se encontró un beneficiario asociado.";
                    return RedirectToAction("Error");
                }

                var siniestros = await _siniestroLN.ObtenerSiniestrosPorBeneficiarioAsync(idBeneficiario);
                Console.WriteLine($"Número de siniestros encontrados: {siniestros.Count}");

                var siniestrosConNumero = siniestros
                    .Select((siniestro, index) => new
                    {
                        Numero = index + 1,
                        IdSiniestro = siniestro.IdSiniestro,
                        Tipo = siniestro.Tipo ?? "Tipo no especificado",
                        FechaCreacion = siniestro.FechaCreacion
                    })
                    .ToList();

                ViewBag.Siniestros = siniestrosConNumero;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cargar los seguimientos: {ex.Message}";
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetalleSeguimiento(int idSiniestro)
        {
            try
            {
                var siniestro = await _siniestroLN.ObtenerSeguimientoCompletoPorSiniestroAsync(idSiniestro);

                if (siniestro == null)
                {
                    TempData["ErrorMessage"] = "No se encontró el siniestro solicitado.";
                    return RedirectToAction("Error");
                }

                var reclamaciones = await _reclamacionLN.ObtenerReclamacionesPorSiniestroAsync(idSiniestro);

                var modelo = new SeguimientoViewModel
                {
                    IdSiniestro = siniestro.IdSiniestro,
                    TipoSiniestro = siniestro.Tipo ?? "Tipo no especificado",
                    FechaSiniestro = siniestro.FechaSiniestro ?? DateTime.MinValue,
                    Ubicacion = siniestro.Ubicacion ?? "Ubicación no especificada",
                    Descripcion = siniestro.Descripcion ?? "Sin descripción",
                    PresupuestoId = siniestro.Presupuesto?.Id ?? 0,
                    EstadoPresupuesto = siniestro.Presupuesto?.Estado ?? "Estado no definido",
                    MontoTotalPresupuesto = siniestro.Presupuesto?.MontoTotal ?? 0,
                    TallerId = siniestro.Taller?.Id ?? 0,
                    NombreTaller = siniestro.Taller?.Nombre ?? "Taller no especificado",
                    DireccionTaller = siniestro.Taller?.Direccion ?? "Dirección no disponible",
                    TelefonoTaller = siniestro.Taller?.Telefono ?? "Teléfono no disponible",
                    Reclamaciones = reclamaciones // Lista vacía si no hay reclamaciones
                };

                return View(modelo);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cargar los detalles del siniestro: {ex.Message}";
                return RedirectToAction("Error");
            }
        }

        public IActionResult Error()
        {
            // Obtener el mensaje de error desde TempData
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "Ocurrió un error inesperado.";

            return View();
        }
    }
}
