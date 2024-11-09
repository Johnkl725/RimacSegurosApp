using EntidadesProyecto;
using MiAplicacion.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class BeneficiarioDA
    {
        private readonly MyDbContext _context;
        public BeneficiarioDA(MyDbContext context)
        {
            _context = context;
        }

        public void RegistrarBeneficiario(int id_usuario, int id_vehiculo, string password)
        {
            var idUsuarioParam = new SqlParameter("@id_usuario", id_usuario);
            var idVehiculoParam = new SqlParameter("@IdVehiculo", id_vehiculo);
            var passwordParam = new SqlParameter("@Password", password);

            // Llamar al procedimiento almacenado
            _context.Database.ExecuteSqlRaw(
                "EXEC [dbo].[sp_RegistrarBeneficiario] @id_usuario, @IdVehiculo, @Password",
                idUsuarioParam, idVehiculoParam, passwordParam
            );
        }

        public void AsignarIDVehiculo(int idBeneficiario, int idVehiculo)
        {
            // Crear los parámetros necesarios para el procedimiento almacenado
            var idBeneficiarioParam = new SqlParameter("@idBeneficiario", idBeneficiario);
            var idVehiculoParam = new SqlParameter("@IdVehiculo", idVehiculo);

            // Ejecutar el procedimiento almacenado para asignar el id_vehiculo
            _context.Database.ExecuteSqlRaw(
                "EXEC [dbo].[sp_AsignarIDVehiculo] @idBeneficiario, @IdVehiculo",
                idBeneficiarioParam, idVehiculoParam
            );


        }
        public void ActualizarIdVehiculoBeneficiario(int idVehiculo)
        {
            var idVehiculoParam = new SqlParameter("@IdVehiculo", idVehiculo);

            // Llamar al procedimiento para actualizar el id_vehiculo del último beneficiario insertado
            _context.Database.ExecuteSqlRaw(
                "EXEC [dbo].[sp_ActualizarIdVehiculoBeneficiario] @IdVehiculo",
                idVehiculoParam
            );
        }




    }
}
