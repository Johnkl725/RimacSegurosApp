using EntidadesProyecto;
using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using AccesoDatos;

namespace AplicaciónWeb.Controllers
{
    public class ReclamacionController : Controller
    {

        private readonly ReclamacionLN _reclamacionLN;
        private readonly DocumentoReclamacionLN _documentosReclamacionLN;
        private readonly SiniestroLN _siniestroLN;
        private readonly Cloudinary _cloudinary;


        public ReclamacionController(ReclamacionLN reclamacionLN,
            DocumentoReclamacionLN documentosReclamacionLN,
            SiniestroLN siniestroLN, Cloudinary cloudinary)
        {
            _reclamacionLN = reclamacionLN;
            _documentosReclamacionLN = documentosReclamacionLN;
            _siniestroLN = siniestroLN;
            _cloudinary = cloudinary;

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
        public async Task<IActionResult> IngresarReclamacion(int idSiniestro, string descripcion, string tipo, List<IFormFile> archivos)
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
                // Crear una nueva reclamación
                var reclamacion = new Reclamacion
                {
                    IdSiniestro = idSiniestro,
                    FechaReclamacion = DateTime.Now,
                    Descripcion = descripcion,
                    Tipo = tipo,
                    Estado = "Pendiente"
                };

                _reclamacionLN.RegistrarReclamacion(reclamacion);
                int idReclamacion = reclamacion.Id;

                // Procesar archivos si existen
                if (archivos != null && archivos.Count > 0)
                {
                    foreach (var archivo in archivos)
                    {
                        var validacionResultado = ValidarArchivo(archivo);
                        if (!validacionResultado.IsValid)
                        {
                            ModelState.AddModelError("archivos", validacionResultado.Message);
                            continue;
                        }

                        try
                        {
                            // Subir archivo a Cloudinary
                            var uploadResult = await SubirArchivoACloudinary(archivo, idSiniestro, idReclamacion);

                            // Registrar en la base de datos
                            var documento = new DocumentosReclamacion
                            {
                                IdReclamacion = reclamacion.Id,
                                Nombre = archivo.FileName,
                                Extension = Path.GetExtension(archivo.FileName).ToLower(),
                                Url = uploadResult.SecureUrl.ToString()
                            };

                            _documentosReclamacionLN.RegistrarDocumento(documento);
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("archivos", $"Error al subir {archivo.FileName}: {ex.Message}");
                        }
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

        

        private async Task<UploadResult> SubirArchivoACloudinary(IFormFile archivo, int idSiniestro, int IdReclamacion)
        {
            var fileExtension = Path.GetExtension(archivo.FileName).ToLower();
            var isImage = archivo.ContentType.StartsWith("image/");
            // Generar el nombre de la carpeta
            var carpetaNombre = $"Siniestro_{idSiniestro}-Reclamacion_{IdReclamacion}";
            var uploadParams = isImage
                ? new ImageUploadParams
                {
                    File = new FileDescription(archivo.FileName, archivo.OpenReadStream()),
                    Folder = $"Reclamaciones/{carpetaNombre}",
                    PublicId = $"{Path.GetFileNameWithoutExtension(archivo.FileName)}"
                }
                : new RawUploadParams
                {
                    File = new FileDescription(archivo.FileName, archivo.OpenReadStream()),
                    Folder = $"Reclamaciones/{carpetaNombre}", 
                    PublicId = $"{Path.GetFileNameWithoutExtension(archivo.FileName)}"
                };

            return await _cloudinary.UploadAsync(uploadParams);
        }




        private (bool IsValid, string Message) ValidarArchivo(IFormFile archivo)
        {
            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".docx", ".xlsx" };
            const long maxSizeInBytes = 5 * 1024 * 1024; // Cambiado a 5 MB

            var fileExtension = Path.GetExtension(archivo.FileName).ToLower();

            if (!extensionesPermitidas.Contains(fileExtension))
            {
                return (false, $"El formato del archivo {archivo.FileName} no está permitido.");
            }

            if (archivo.Length > maxSizeInBytes)
            {
                return (false, $"El archivo {archivo.FileName} excede el tamaño máximo permitido (5 MB).");
            }

            return (true, "");
        }

        public IActionResult Confirmacion()
        {
            ViewBag.Message = TempData["SuccessMessage"];
            return View();
        }


        



    }

}





