using AccesoDatos;
using EntidadesProyecto;
using LogicaNegocio;
using MiAplicacion.Data;
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
        public BeneficiarioController(VehiculoLN vehiculoLN, BeneficiarioLN beneficiarioLN,MyDbContext context)
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
                // 1. Crear el vehículo y obtener el id
                int idVehiculo = _vehiculoLN.CrearVehiculo(
                    vehiculo.Placa,
                    vehiculo.Marca,
                    vehiculo.Modelo,
                    vehiculo.Tipo,
                    vehiculo.TarjetaVehiculo
                );

                try
                {
                    // 2. Actualizar el id_vehiculo del beneficiario
                    int idBeneficiario = _context.Beneficiarios
                .OrderByDescending(b => b.IdUsuario)  // Ordenar en orden descendente por id
                .Select(b => b.IdUsuario)             // Seleccionar solo el campo id
                .FirstOrDefault();             // Obtener el primer (último) id

                    if (idBeneficiario == 0)
                    {
                        throw new Exception("No se encontró un beneficiario para asignar el vehículo.");
                    }
                    _beneficiarioLN.asignarIDVehiculo(idBeneficiario, idVehiculo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View("Error");
                }

                // Redirigir a una página después de la creación del vehículo y asignación del id_vehiculo al beneficiario
                return RedirectToAction("PersonalDashboard", "Personal");
            }

            return View(vehiculo);
        }


    }
}
