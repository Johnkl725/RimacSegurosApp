using AccesoDatos;
using EntidadesProyecto;
using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AplicaciónWeb.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly VehiculoLN _vehiculoLN;
        private readonly BeneficiarioLN _beneficiarioLN;

        // Inyección de dependencias para las capas de negocio
        public BeneficiarioController(VehiculoLN vehiculoLN, BeneficiarioLN beneficiarioLN)
        {
            _vehiculoLN = vehiculoLN;
            _beneficiarioLN = beneficiarioLN;
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
                    // 2. Actualizar el id_vehiculo del último beneficiario insertado
                    _beneficiarioLN.ActualizarIdVehiculoBeneficiario(idVehiculo);
                }
                catch (Exception ex)
                {
                    // Manejo de errores si ocurre un problema al ejecutar el procedimiento almacenado
                    Console.WriteLine(ex.Message);
                    return View("Error"); // Redirigir a una vista de error si lo prefieres
                }


                // Redirigir a una página después de la creación del beneficiario
                return RedirectToAction("PersonalDashboard", "Personal");
            }

            // Si hay errores, volver a mostrar la vista con los errores
            return View(vehiculo); // Devolver el vehículo en caso de errores
        }

    }
}
