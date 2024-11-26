using AccesoDatos;
using AplicaciónWeb.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EntidadesProyecto;
using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicaciónWeb.Controllers
{
    public class SiniestroController : Controller
    {
        private readonly SiniestroLN _siniestroLN;
        private readonly TallerLN _tallerLN;
        private readonly Cloudinary _cloudinary;

        public SiniestroController(SiniestroLN siniestroLN, TallerLN tallerLN, Cloudinary cloudinary)
        {
            _siniestroLN = siniestroLN;
            _tallerLN = tallerLN;
            _cloudinary = cloudinary;
        }

        // GET: Muestra la vista para registrar un siniestro
        public async Task<IActionResult> RegistrarSiniestro()
        {
            await CargarListasAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarSiniestro(SiniestroViewModel model, List<IFormFile> archivos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

                    // Obtener la póliza activa del beneficiario
                    var poliza = await _siniestroLN.ObtenerPolizaActivaPorUsuarioAsync(idUsuario);
                    if (poliza == null)
                    {
                        ModelState.AddModelError("", "No se encontró una póliza activa para este beneficiario.");
                        await CargarListasAsync();
                        return View(model);
                    }

                    // Crear y registrar el siniestro
                    var siniestro = new Siniestro
                    {
                        IdDepartamento = model.IdDepartamento,
                        IdProvincia = model.IdProvincia,
                        IdDistrito = model.IdDistrito,
                        IdPoliza = poliza.Id,
                        Tipo = model.Tipo,
                        FechaSiniestro = model.FechaSiniestro,
                        Ubicacion = model.Ubicacion,
                        Descripcion = model.Descripcion
                    };

                    await _siniestroLN.RegistrarSiniestro(siniestro);

                    // Procesar y subir los documentos asociados al siniestro
                    if (archivos != null && archivos.Any())
                    {
                        foreach (var archivo in archivos)
                        {
                            // Validar archivo
                            var validacion = ValidarArchivo(archivo);
                            if (!validacion.IsValid)
                            {
                                ModelState.AddModelError("archivos", validacion.Message);
                                continue;
                            }

                            // Subir a Cloudinary
                            var uploadResult = await SubirArchivoACloudinary(archivo, siniestro.IdSiniestro);

                            // Registrar el documento en la base de datos
                            var documento = new DocumentosReclamacion
                            {
                                IdReclamacion = siniestro.IdSiniestro,
                                Nombre = archivo.FileName,
                                Extension = Path.GetExtension(archivo.FileName).ToLower(),
                                Url = uploadResult.SecureUrl.ToString()
                            };

                            await _siniestroLN.RegistrarDocumento(documento);
                        }
                    }

                    TempData["SuccessMessage"] = "Siniestro y documentos registrados exitosamente.";
                    return RedirectToAction("Confirmacion");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al registrar el siniestro: {ex.Message}");
                }
            }

            await CargarListasAsync();
            return View(model);
        }

        private (bool IsValid, string Message) ValidarArchivo(IFormFile archivo)
        {
            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".docx", ".xlsx" };
            const long maxSizeInBytes = 5 * 1024 * 1024; // 5 MB

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
        private async Task<UploadResult> SubirArchivoACloudinary(IFormFile archivo, int idSiniestro)
        {
            var fileExtension = Path.GetExtension(archivo.FileName).ToLower();
            var isImage = archivo.ContentType.StartsWith("image/");

            var carpetaNombre = $"Siniestros/Siniestro_{idSiniestro}";
            var uploadParams = isImage
                ? new ImageUploadParams
                {
                    File = new FileDescription(archivo.FileName, archivo.OpenReadStream()),
                    Folder = carpetaNombre,
                    PublicId = Path.GetFileNameWithoutExtension(archivo.FileName)
                }
                : new RawUploadParams
                {
                    File = new FileDescription(archivo.FileName, archivo.OpenReadStream()),
                    Folder = carpetaNombre,
                    PublicId = Path.GetFileNameWithoutExtension(archivo.FileName)
                };

            return await _cloudinary.UploadAsync(uploadParams);
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

            // Llamada asincrónica al método ObtenerTodosLosTalleresAsync
            var talleres = await _tallerLN.ObtenerTodosLosTalleresAsync();
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
