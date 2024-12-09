using Microsoft.AspNetCore.Mvc;
using EntidadesProyecto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using iText.Kernel.Pdf;
using Microsoft.Data.SqlClient;
using MiAplicacion.Data;

using Microsoft.AspNetCore.Mvc;
using EntidadesProyecto;
using System.Collections.Generic;
using AplicaciónWeb.Models.EntidadesProyecto;
using AplicaciónWeb.Models;
using Microsoft.EntityFrameworkCore;
using MiAplicacion.Data;
using Microsoft.Data.SqlClient;
using iText.Kernel.Crypto.Securityhandler;
//using Org.BouncyCastle.Security;
using iText.Bouncycastleconnector;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas.Draw;
//using iText.Layout.Draw;


namespace AplicaciónWeb.Controllers
{
    public class AdminController : Controller
    {

        private readonly AdminLN _adminLN;
        private readonly MyDbContext _context; // Agregar esta línea

        public AdminController(AdminLN adminLN, MyDbContext context)
        {
            _adminLN = adminLN;
            _context = context; // Asignar el contexto
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }
        public IActionResult GestionarPresupuestos()
        {
            // Obtiene la lista de siniestros con presupuestos
            List<SiniestroPresupuestoViewModel> siniestros = _adminLN.ObtenerSiniestrosSinPresupuesto();

            // Pasa la lista a la vista
            return View(siniestros);
        }

        // Aprobar presupuesto de un siniestro
        [HttpPost]
        public IActionResult AprobarPresupuesto(int idPresupuesto)
        {
            bool resultado = _adminLN.AprobarPresupuesto(idPresupuesto);

            if (resultado)
            {
                TempData["SuccessMessage"] = "Presupuesto aprobado correctamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "No se pudo aprobar el presupuesto.";
            }

            return RedirectToAction("GestionarPresupuestos");
        }

        public IActionResult GestionarPagosIndemnizacion()
        {
            var siniestros = _adminLN.ObtenerSiniestrosParaPago()
                                     .Where(s => s.Presupuesto != null)
                                     .ToList();
            return View(siniestros);
        }


        [HttpPost]
        public IActionResult PagarIndemnizacion(int idSiniestro)
        {
            try
            {
                _adminLN.PagarIndemnizacion(idSiniestro); // No devuelve bool, lanza excepciones si hay errores.
                TempData["SuccessMessage"] = "Pago realizado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message; // Mensaje detallado del error.
            }

            return RedirectToAction("GestionarPagosIndemnizacion");
        }


        public IActionResult GestionarPresupuesto(int idSiniestro)
        {
            // Obtener el siniestro
            var siniestro = _context.Siniestros.FirstOrDefault(s => s.IdSiniestro == idSiniestro);

            if (siniestro == null)
            {
                return NotFound("No se encontró el siniestro.");
            }

            // Crear el modelo para la vista
            var presupuestoViewModel = new PresupuestoViewModel
            {
                IdSiniestro = siniestro.IdSiniestro, // Asegúrate de asignar el ID aquí
                Detalles = siniestro.Presupuesto?.Detalles,
                MontoTotal = siniestro.Presupuesto?.MontoTotal ?? 0,
                Impuestos = siniestro.Presupuesto?.Impuestos ?? 0,
                CostoSinImpuestos = siniestro.Presupuesto?.CostoSinImpuestos ?? 0,
                Descuento = siniestro.Presupuesto?.Descuento ?? 0,
                Enlace = siniestro.Presupuesto?.Enlace
            };

            return View(presupuestoViewModel);
        }



        [HttpPost]
        public IActionResult ValidarPresupuesto(PresupuestoViewModel model)
        {
            try
            {
                // Crear el nuevo presupuesto
                var presupuesto = new Presupuesto
                {
                    FechaEmision = DateTime.Now,
                    MontoTotal = model.MontoTotal,
                    Detalles = model.Detalles,
                    Impuestos = model.Impuestos,
                    CostoSinImpuestos = model.CostoSinImpuestos,
                    Estado = "Pendiente",
                    Descuento = 0, // Siempre 0
                    Enlace = "presupuesto.com" // Valor predeterminado
                };

                _context.Presupuestos.Add(presupuesto);
                _context.SaveChanges();

                // Obtener el ID del presupuesto recién creado
                int nuevoIdPresupuesto = presupuesto.Id;

                // Actualizar el campo id_presupuesto en la tabla Siniestro
                var siniestro = _context.Siniestros.FirstOrDefault(s => s.IdSiniestro == model.IdSiniestro);
                if (siniestro != null)
                {
                    siniestro.IdPresupuesto = nuevoIdPresupuesto;
                    _context.SaveChanges();
                }

                TempData["SuccessMessage"] = "Presupuesto validado y asociado correctamente.";
                return RedirectToAction("GestionarPresupuestos");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al validar el presupuesto: {ex.Message}";
                return View(model);
            }
        }



        // otra funcion nueva para el reporte

        public IActionResult GenerarReporte()
        {
            var siniestros = _context.Siniestros
                .Include(s => s.Taller)
                .Include(s => s.Presupuesto)
                .Select(s => new GenerarReporteViewModel
                {
                    NumeroSiniestro = $"SIN-{s.IdSiniestro:D3}",
                    FechaRegistro = s.FechaCreacion ?? DateTime.MinValue, // Usar coalescencia nula para manejar valores null
                    Estado = s.Presupuesto != null ? s.Presupuesto.Estado : "Pendiente", // Condición explícita para manejar null
                    IdSiniestro = s.IdSiniestro
                })
                .ToList();

            return View(siniestros);
        }



        public IActionResult ObtenerDetalleReporte(int idSiniestro)
        {
            // Consulta SQL
            string query = @"
    SELECT 
        s.id_siniestro AS IdSiniestro,
        s.fecha_creacion AS FechaRegistro,
        p.estado AS Estado,
        t.nombre AS TallerAsignado,
        p.monto_total AS CostoMantenimiento,
        s.tipo AS TipoSiniestro,
        s.descripcion AS Descripcion
    FROM 
        siniestro s
    LEFT JOIN 
        taller t ON s.id_taller = t.id
    LEFT JOIN 
        presupuesto p ON s.id_presupuesto = p.id
    WHERE 
        s.id_siniestro = @Id";

            // Objeto para almacenar los datos
            DetalleReporteViewModel detalleViewModel = null;

            // Conexión y lectura manual
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Id", idSiniestro));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detalleViewModel = new DetalleReporteViewModel
                            {
                                NumeroSiniestro = $"SIN-{reader["IdSiniestro"]:D3}",
                                FechaRegistro = reader["FechaRegistro"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["FechaRegistro"])
                                    : DateTime.MinValue,
                                Estado = reader["Estado"] != DBNull.Value
                                    ? reader["Estado"].ToString()
                                    : "Pendiente",
                                TallerAsignado = reader["TallerAsignado"] != DBNull.Value
                                    ? reader["TallerAsignado"].ToString()
                                    : "No asignado",
                                CostoMantenimiento = reader["CostoMantenimiento"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["CostoMantenimiento"])
                                    : 0,
                                TipoSiniestro = reader["TipoSiniestro"] != DBNull.Value
                                    ? reader["TipoSiniestro"].ToString()
                                    : "No especificado",
                                Descripcion = reader["Descripcion"] != DBNull.Value
                                    ? reader["Descripcion"].ToString()
                                    : "Sin descripción"
                            };
                        }
                    }
                }
            }

            if (detalleViewModel == null)
            {
                return NotFound();
            }

            // Cambiar a una vista completa
            return View("DetalleSiniestro", detalleViewModel);
        }



        [HttpGet]
        public IActionResult DescargarReportePDF(int idSiniestro)
        {
            // Consulta SQL para obtener los datos del siniestro
            string query = @"
        SELECT 
            s.id_siniestro AS IdSiniestro,
            s.fecha_creacion AS FechaRegistro,
            p.estado AS Estado,
            t.nombre AS TallerAsignado,
            p.monto_total AS CostoMantenimiento,
            s.tipo AS TipoSiniestro,
            s.descripcion AS Descripcion
        FROM 
            siniestro s
        LEFT JOIN 
            taller t ON s.id_taller = t.id
        LEFT JOIN 
            presupuesto p ON s.id_presupuesto = p.id
        WHERE 
            s.id_siniestro = @Id";

            DetalleReporteViewModel detalleViewModel = null;

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Id", idSiniestro));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detalleViewModel = new DetalleReporteViewModel
                            {
                                NumeroSiniestro = $"SIN-{reader["IdSiniestro"]:D3}",
                                FechaRegistro = reader["FechaRegistro"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["FechaRegistro"])
                                    : DateTime.MinValue,
                                Estado = reader["Estado"] != DBNull.Value
                                    ? reader["Estado"].ToString()
                                    : "Pendiente",
                                TallerAsignado = reader["TallerAsignado"] != DBNull.Value
                                    ? reader["TallerAsignado"].ToString()
                                    : "No asignado",
                                CostoMantenimiento = reader["CostoMantenimiento"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["CostoMantenimiento"])
                                    : 0,
                                TipoSiniestro = reader["TipoSiniestro"] != DBNull.Value
                                    ? reader["TipoSiniestro"].ToString()
                                    : "No especificado",
                                Descripcion = reader["Descripcion"] != DBNull.Value
                                    ? reader["Descripcion"].ToString()
                                    : "Sin descripción"
                            };
                        }
                    }
                }
            }

            if (detalleViewModel == null)
            {
                return NotFound();
            }

            // Crear el PDF
            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Definir fuentes (necesitas tener acceso a las fuentes, puede usar fuentes estándar)
                var boldFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);
                var normalFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);

                // Título principal
                var titulo = new Paragraph("Detalle del Siniestro")
                    .SetFont(boldFont)
                    .SetFontSize(18)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetMarginBottom(20);

                document.Add(titulo);

                // Puede agregar una línea divisoria
                var linea = new LineSeparator(new SolidLine());
                document.Add(linea);

                // Crear una tabla con dos columnas para mostrar label y valor
                var table = new iText.Layout.Element.Table(new float[] { 1, 2 }).UseAllAvailableWidth();
                table.SetMarginTop(15);
                table.SetMarginBottom(15);

                // Opcional: Color de fondo para las celdas de encabezado
                var headerBackgroundColor = new iText.Kernel.Colors.DeviceRgb(230, 230, 230);

                // Añadir filas a la tabla (Encabezados)
                table.AddHeaderCell(new iText.Layout.Element.Cell().Add(new Paragraph("Campo").SetFont(boldFont)).SetBackgroundColor(headerBackgroundColor));
                table.AddHeaderCell(new iText.Layout.Element.Cell().Add(new Paragraph("Valor").SetFont(boldFont)).SetBackgroundColor(headerBackgroundColor));

                // Añadir datos del siniestro
                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph("Número de Siniestro:").SetFont(normalFont)));
                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph(detalleViewModel.NumeroSiniestro).SetFont(normalFont)));

                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph("Fecha de Registro:").SetFont(normalFont)));
                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph(detalleViewModel.FechaRegistro.ToString("dd/MM/yyyy")).SetFont(normalFont)));

                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph("Estado:").SetFont(normalFont)));
                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph(detalleViewModel.Estado).SetFont(normalFont)));

                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph("Taller Asignado:").SetFont(normalFont)));
                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph(detalleViewModel.TallerAsignado).SetFont(normalFont)));

                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph("Costo de Mantenimiento:").SetFont(normalFont)));
                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph($"{detalleViewModel.CostoMantenimiento:C}").SetFont(normalFont)));

                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph("Tipo de Siniestro:").SetFont(normalFont)));
                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph(detalleViewModel.TipoSiniestro).SetFont(normalFont)));

                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph("Descripción:").SetFont(normalFont)));
                // Para la descripción, si es muy larga, el párrafo se ajustará de manera automática.
                table.AddCell(new iText.Layout.Element.Cell().Add(new Paragraph(detalleViewModel.Descripcion).SetFont(normalFont)));

                document.Add(table);

                // Agregar una imagen, logo o pie de página (opcional)
                // Ejemplo: Incluir un pie de página:
                document.ShowTextAligned(
                    new Paragraph("Reporte generado el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                        .SetFont(normalFont)
                        .SetFontSize(10),
                    550, 20, pdf.GetNumberOfPages(),
                    iText.Layout.Properties.TextAlignment.RIGHT,
                    iText.Layout.Properties.VerticalAlignment.BOTTOM, 0);

                document.Close();

                return File(stream.ToArray(), "application/pdf", $"Siniestro_{detalleViewModel.NumeroSiniestro}.pdf");
            }
        }

    }
}
