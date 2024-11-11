using EntidadesProyecto;
using MiAplicacion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace LogicaNegocio
{
    public class VehiculoLN
    {
        private readonly VehiculoDA _vehiculoDA;

        // Constructor que recibe el contexto de la base de datos (DbContext)
        public VehiculoLN(MyDbContext context)
        {
            _vehiculoDA = new VehiculoDA(context);
        }

        // Método para registrar un vehículo
        public int CrearVehiculo(string placa, string marca, string modelo, string tipo, int tarjeta_vehiculo)
        {
            // Usar el procedimiento almacenado para insertar el vehículo y obtener el Id
            return _vehiculoDA.InsertarVehiculo(placa, marca, modelo, tipo, tarjeta_vehiculo);
        }


    }

}
