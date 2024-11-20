using EntidadesProyecto;
using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using AccesoDatos;

namespace AplicaciónWeb.Controllers
{
    public class ReclamacionController : Controller
    {
        
            private readonly ReclamacionLN _reclamacionLN;
            private readonly DocumentoReclamacionLN _documentosReclamacionLN;
            private readonly SiniestroLN _siniestroLN;



        public ReclamacionController(ReclamacionLN reclamacionLN,
            DocumentoReclamacionLN documentosReclamacionLN,
            SiniestroLN siniestroLN)
            {
            _reclamacionLN = reclamacionLN;
            _documentosReclamacionLN = documentosReclamacionLN;
            _siniestroLN = siniestroLN;


        }
        [HttpGet]
        public async Task<IActionResult> IngresarReclamacion()
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
                ModelState.AddModelError("", $"Error al cargar los siniestros: {ex.Message}");
                return View(new List<Siniestro>());
            }
        }


        [HttpPost]
        public IActionResult IngresarReclamacion(int idSiniestro, string descripcion, string tipo, List<IFormFile> archivos)
        {
            if (string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(tipo) || idSiniestro <= 0)
            {
                ModelState.AddModelError("", "Todos los campos son obligatorios.");
                return View();
            }

            // Validación para verificar la cantidad de archivos
            if (archivos != null && archivos.Count > 5)
            {
                ModelState.AddModelError("archivos", "No puede subir más de 5 archivos.");
                return View();
            }

            try
            {
                var reclamacion = new Reclamacion
                {
                    IdSiniestro = idSiniestro,
                    FechaReclamacion = DateTime.Now,
                    Descripcion = descripcion,
                    Tipo = tipo,
                    Estado = "Pendiente"
                };

                _reclamacionLN.RegistrarReclamacion(reclamacion);

                if (archivos != null && archivos.Count > 0)
                {
                    foreach (var archivo in archivos)
                    {
                        using var memoryStream = new MemoryStream();
                        archivo.CopyTo(memoryStream);

                        var documento = new DocumentosReclamacion
                        {
                            IdReclamacion = reclamacion.Id,
                            Nombre = archivo.FileName,
                            Archivo = memoryStream.ToArray(),
                            Extension = Path.GetExtension(archivo.FileName)
                        };

                        _documentosReclamacionLN.RegistrarDocumento(documento);
                    }
                }

                TempData["SuccessMessage"] = "Reclamación registrada exitosamente.";
                return RedirectToAction("Confirmacion");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ocurrió un error: {ex.Message}");
                return View();
            }
        }


        public IActionResult Confirmacion()
            {
                ViewBag.Message = TempData["SuccessMessage"];
                return View();
            }

        
        }
}
