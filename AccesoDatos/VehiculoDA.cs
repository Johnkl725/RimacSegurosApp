using MiAplicacion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesProyecto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AccesoDatos
{
    public class VehiculoDA
    {
        private readonly MyDbContext _context;

        // Constructor que recibe el DbContext
        public VehiculoDA(MyDbContext context)
        {
            _context = context;
        }

        // Método para insertar un vehículo utilizando el procedimiento almacenado
        public int InsertarVehiculo(string placa, string marca, string modelo, string tipo, int tarjeta_vehiculo)
        {
            var placaParam = new SqlParameter("@Placa", placa);
            var marcaParam = new SqlParameter("@Marca", marca);
            var modeloParam = new SqlParameter("@Modelo", modelo);
            var tipoParam = new SqlParameter("@Tipo", tipo);
            var tarjetaVehiculoParam = new SqlParameter("@TarjetaVehiculo", tarjeta_vehiculo);
            var idVehiculoParam = new SqlParameter("@IdVehiculo", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            // Llamar al procedimiento almacenado
            _context.Database.ExecuteSqlRaw("EXEC [dbo].[sp_InsertarVehiculo] @Placa, @Marca, @Modelo, @Tipo, @TarjetaVehiculo, @IdVehiculo OUTPUT",
                placaParam, marcaParam, modeloParam, tipoParam, tarjetaVehiculoParam, idVehiculoParam);

            // Obtener el Id del vehículo
            return (int)idVehiculoParam.Value;
        }


    }

}