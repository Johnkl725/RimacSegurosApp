using MiAplicacion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using EntidadesProyecto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class ReclamacionDA
    {
        private readonly MyDbContext _context;

        public ReclamacionDA(MyDbContext context)
        {
            _context = context;
        }

        public void RegistrarReclamacion(Reclamacion reclamacion)
        {
            try
            {
                var idParam = new SqlParameter("@IdReclamacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_RegistrarReclamacion @IdSiniestro, @FechaReclamacion, @Estado, @Tipo, @Descripcion, @IdReclamacion OUTPUT",
                    new SqlParameter("@IdSiniestro", reclamacion.IdSiniestro),
                    new SqlParameter("@FechaReclamacion", reclamacion.FechaReclamacion),
                    new SqlParameter("@Estado", reclamacion.Estado ?? "Pendiente"),
                    new SqlParameter("@Tipo", reclamacion.Tipo),
                    new SqlParameter("@Descripcion", reclamacion.Descripcion),
                    idParam
                );

                reclamacion.Id = (int)idParam.Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar la reclamación: {ex.Message}");
            }
        }


        public List<Reclamacion> ObtenerReclamacionesPorSiniestro(int idSiniestro)
        {
            return _context.Reclamaciones
                .Where(r => r.IdSiniestro == idSiniestro)
                .ToList();
        }

        public int ObtenerIdBeneficiarioPorUsuario(int idUsuario)
        {
            var beneficiario = _context.Beneficiarios
                .FirstOrDefault(b => b.IdUsuario == idUsuario);

            return beneficiario?.Id ?? 0; // Retorna 0 si no se encuentra
        }
    }
}
