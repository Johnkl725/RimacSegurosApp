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
                    "EXEC sp_RegistrarReclamacion @IdSiniestro, @FechaReclamacion, @Estado, @Descripcion,@Tipo, @IdReclamacion OUTPUT",
                    new SqlParameter("@IdSiniestro", reclamacion.IdSiniestro),
                    new SqlParameter("@FechaReclamacion", reclamacion.FechaReclamacion),
                    new SqlParameter("@Estado", reclamacion.Estado ?? "Pendiente"), 
                    new SqlParameter("@Descripcion", reclamacion.Descripcion),
                    new SqlParameter("@Tipo", reclamacion.Tipo),
                    idParam
                );

                reclamacion.Id = (int)idParam.Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar la reclamación: {ex.Message}");
            }
        }



        public int ObtenerIdBeneficiarioPorUsuario(int idUsuario)
        {
            var beneficiario = _context.Beneficiarios
                .FirstOrDefault(b => b.IdUsuario == idUsuario);

            return beneficiario?.Id ?? 0; 
        }


        public List<Reclamacion> ObtenerReclamacionesPorSiniestro(int idSiniestro)
        {
            return _context.Reclamaciones
                .Where(r => r.IdSiniestro == idSiniestro)
                .ToList();
        }

        public async Task<List<Reclamacion>> ObtenerReclamacionesPorSiniestroAsync(int idSiniestro)
        {
            return await _context.Reclamaciones
                .Where(r => r.IdSiniestro == idSiniestro)
                .ToListAsync();
        }

    }
}
