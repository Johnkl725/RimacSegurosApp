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
            List<SiniestroPresupuestoViewModel> siniestros = _adminLN.ObtenerSiniestrosConPresupuestos();

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
            bool resultado = _adminLN.PagarIndemnizacion(idSiniestro);

            if (resultado)
            {
                TempData["SuccessMessage"] = "Pago realizado correctamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "No se pudo realizar el pago.";
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
                    Descuento = model.Descuento,
                    Enlace = model.Enlace
                };

                _context.Presupuestos.Add(presupuesto);
                _context.SaveChanges();

                // Obtener el ID del presupuesto recién creado
                int nuevoIdPresupuesto = presupuesto.Id;

                // Actualizar el campo id_presupuesto en la tabla Siniestro
                var siniestro = _context.Siniestros.FirstOrDefault(s => s.IdSiniestro == model.IdSiniestro);
                // Modificar esta línea
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

                // Agregar contenido al PDF
                document.Add(new Paragraph($"Detalle del Siniestro: {detalleViewModel.NumeroSiniestro}"));
                document.Add(new Paragraph($"Fecha de Registro: {detalleViewModel.FechaRegistro:dd/MM/yyyy}"));
                document.Add(new Paragraph($"Estado: {detalleViewModel.Estado}"));
                document.Add(new Paragraph($"Taller Asignado: {detalleViewModel.TallerAsignado}"));
                document.Add(new Paragraph($"Costo de Mantenimiento: {detalleViewModel.CostoMantenimiento:C}"));
                document.Add(new Paragraph($"Tipo de Siniestro: {detalleViewModel.TipoSiniestro}"));
                document.Add(new Paragraph($"Descripción: {detalleViewModel.Descripcion}"));

                document.Close();

                return File(stream.ToArray(), "application/pdf", $"Siniestro_{detalleViewModel.NumeroSiniestro}.pdf");
            }

        }
    }
}
