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


        public ReclamacionController(ReclamacionLN reclamacionLN, DocumentoReclamacionLN documentosReclamacionLN)
            {
                _reclamacionLN = reclamacionLN;
                _documentosReclamacionLN = documentosReclamacionLN;
            
        }

        [HttpGet]
        public IActionResult IngresarReclamacion()
        {
            try
            {
                // Datos estáticos para siniestros
                var siniestros = new List<Siniestro>
            {
                new Siniestro { IdSiniestro = 5, Tipo = "Choque", Descripcion = "Choque leve" },
                new Siniestro { IdSiniestro = 6, Tipo = "Robo", Descripcion = "Robo total" },
                new Siniestro { IdSiniestro = 7, Tipo = "Daño Material", Descripcion = "Daño en la carrocería" }
            };

                ViewBag.Siniestros = siniestros;

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

            try
            {
                var reclamacion = new Reclamacion
                {
                    IdSiniestro = idSiniestro,
                    FechaReclamacion = DateTime.Now,
                    Tipo = tipo,
                    Descripcion = descripcion,
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
