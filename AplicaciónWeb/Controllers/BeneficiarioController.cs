using AccesoDatos;
using EntidadesProyecto;
using LogicaNegocio;
using MiAplicacion.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AplicaciónWeb.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly VehiculoLN _vehiculoLN;
        private readonly BeneficiarioLN _beneficiarioLN;
        private readonly MyDbContext _context;

        // Inyección de dependencias para las capas de negocio
        public BeneficiarioController(VehiculoLN vehiculoLN, BeneficiarioLN beneficiarioLN, MyDbContext context)
        {
            _vehiculoLN = vehiculoLN;
            _beneficiarioLN = beneficiarioLN;
            _context = context;
        }

        public IActionResult BeneficiarioDashboard()
        {
            return View();
        }

        // Vista para crear el beneficiario, mostrando formulario para vehículo y usuario
        public IActionResult CrearVehiculo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearVehiculo(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                int idVehiculo = _vehiculoLN.CrearVehiculo(
                    vehiculo.Placa,
                    vehiculo.Marca,
                    vehiculo.Modelo,
                    vehiculo.Tipo,
                    vehiculo.TarjetaVehiculo
                );

                try
                {
                    // Obtener el id del último beneficiario 
                    int idBeneficiario = _context.Beneficiarios
                        .OrderByDescending(b => b.Id)  // Ordenar por IdUsuario descendente
                        .Select(b => b.Id)  // Seleccionamos el IdUsuario de beneficiario
                        .FirstOrDefault();

                    // para el usuario
                    int idBeneficiario2 = _context.Beneficiarios
                        .OrderByDescending(b => b.IdUsuario)  // Ordenar por IdUsuario descendente
                        .Select(b => b.IdUsuario)  // Seleccionamos el IdUsuario de beneficiario
                        .FirstOrDefault();


                    if (idBeneficiario == 0)
                    {
                        throw new Exception("No se encontró un beneficiario para asignar el vehículo.");
                    }

                    _beneficiarioLN.asignarIDVehiculo(idBeneficiario2, idVehiculo);

                    // Redirigir a CrearPoliza con el idBeneficiario obtenido
                    return RedirectToAction("CrearPoliza", "Poliza", new { idBeneficiario = idBeneficiario });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View("Error");
                }
            }

            return View(vehiculo);
        }



    }
}
