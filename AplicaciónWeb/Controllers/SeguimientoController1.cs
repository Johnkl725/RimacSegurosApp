using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using EntidadesProyecto;
using System.Threading.Tasks;
using AccesoDatos;
using Rotativa.AspNetCore;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Font;

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
                int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
                Console.WriteLine($"ID Usuario autenticado: {idUsuario}");


                // Obtener el ID del beneficiario asociado al usuario autenticado
                var idBeneficiario = _reclamacionLN.ObtenerIdBeneficiarioPorUsuario(idUsuario);
                Console.WriteLine($"ID Beneficiario asociado: {idBeneficiario}");

                if (idBeneficiario <= 0)
                {
                    TempData["ErrorMessage"] = "No se encontró un beneficiario asociado.";
                    return RedirectToAction("Error");
                }

                // Obtener los siniestros asociados al beneficiario autenticado de forma asíncrona
                var siniestros = await _siniestroLN.ObtenerSiniestrosPorBeneficiarioAsync(idBeneficiario);
                Console.WriteLine($"Número de siniestros encontrados: {siniestros.Count}");
                var siniestrosConNumero = siniestros
               .Select((siniestro, index) => new
               {
                   Numero = index + 1,
                   IdSiniestro = siniestro.IdSiniestro,
                   Tipo = siniestro.Tipo,
                   FechaCreacion = siniestro.FechaCreacion
               }).ToList();

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
                // Obtener el siniestro con todos los detalles asociados
                var siniestro = await _siniestroLN.ObtenerSeguimientoCompletoPorSiniestroAsync(idSiniestro);

                if (siniestro == null)
                {
                    TempData["ErrorMessage"] = "No se encontró el siniestro solicitado.";
                    return RedirectToAction("Error");
                }

                // Obtener las reclamaciones relacionadas al siniestro
                var reclamaciones = await _reclamacionLN.ObtenerReclamacionesPorSiniestroAsync(idSiniestro);

                // Mapear el resultado al modelo de la vista
                var modelo = new SeguimientoViewModel
                {
                    IdSiniestro = siniestro.IdSiniestro,
                    TipoSiniestro = siniestro.Tipo,
                    FechaSiniestro = siniestro.FechaSiniestro ?? DateTime.MinValue,
                    Ubicacion = siniestro.Ubicacion,
                    Descripcion = siniestro.Descripcion,
                    PresupuestoId = siniestro.Presupuesto?.Id,
                    EstadoPresupuesto = siniestro.Presupuesto?.Estado,
                    MontoTotalPresupuesto = siniestro.Presupuesto?.MontoTotal,
                    TallerId = siniestro.Taller?.Id,
                    NombreTaller = siniestro.Taller?.Nombre,
                    DireccionTaller = siniestro.Taller?.Direccion,
                    TelefonoTaller = siniestro.Taller?.Telefono,
                    Reclamaciones = reclamaciones // Asignar las reclamaciones al modelo
                };

                return View(modelo);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cargar los detalles del siniestro: {ex.Message}";
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DescargarPDF(int idSiniestro)
        {
            try
            {
                // Obtener el siniestro con todos los detalles asociados
                var siniestro = await _siniestroLN.ObtenerSeguimientoCompletoPorSiniestroAsync(idSiniestro);

                if (siniestro == null)
                {
                    TempData["ErrorMessage"] = "No se encontró el siniestro solicitado.";
                    return RedirectToAction("Error");
                }

                // Obtener las reclamaciones relacionadas al siniestro
                var reclamaciones = await _reclamacionLN.ObtenerReclamacionesPorSiniestroAsync(idSiniestro);

                // Crear un modelo para la vista
                var modelo = new SeguimientoViewModel
                {
                    IdSiniestro = siniestro.IdSiniestro,
                    TipoSiniestro = siniestro.Tipo,
                    FechaSiniestro = siniestro.FechaSiniestro ?? DateTime.MinValue,
                    Ubicacion = siniestro.Ubicacion,
                    Descripcion = siniestro.Descripcion,
                    PresupuestoId = siniestro.Presupuesto?.Id,
                    EstadoPresupuesto = siniestro.Presupuesto?.Estado,
                    MontoTotalPresupuesto = siniestro.Presupuesto?.MontoTotal,
                    TallerId = siniestro.Taller?.Id,
                    NombreTaller = siniestro.Taller?.Nombre,
                    DireccionTaller = siniestro.Taller?.Direccion,
                    TelefonoTaller = siniestro.Taller?.Telefono,
                    Reclamaciones = reclamaciones // Asignar las reclamaciones al modelo
                };

                // Renderizar la vista como PDF usando Rotativa
                return new ViewAsPdf("DetalleSeguimientoPDF", modelo)
                {
                    FileName = $"DetalleSeguimiento_{idSiniestro}.pdf",
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
                };
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al generar el PDF: {ex.Message}";
                return RedirectToAction("Error");
            }
        }



        public IActionResult Error()
        {
            // Acción para manejar errores
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }
    }

}