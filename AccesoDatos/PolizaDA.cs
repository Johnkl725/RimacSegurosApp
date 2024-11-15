using EntidadesProyecto;
using MiAplicacion.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class PolizaDA
    {
        private readonly MyDbContext _context;
        public PolizaDA(MyDbContext context)
        {
            _context = context;
        }

        public int RegistrarPoliza(int id_beneficiario, int id_tipo, DateTime fecha_inicio, DateTime fecha_fin, string estado)
        {
            if (id_beneficiario <= 0)
            {
                id_beneficiario = 1; // Valor por defecto para id_beneficiario
            }

            if (id_tipo <= 0)
            {
                id_tipo = 1; // Valor por defecto para id_tipo
            }
            var idBeneficiarioParam = new SqlParameter("@id_beneficiario", id_beneficiario);
            var idTipoParam = new SqlParameter("@id_tipo", id_tipo);
            var fechaInicioParam = new SqlParameter("@fecha_inicio", fecha_inicio);
            var fechaFinParam = new SqlParameter("@fecha_fin", fecha_fin);
            var estadoParam = new SqlParameter("@estado", estado);

            var idPolizaParam = new SqlParameter("@id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            _context.Database.ExecuteSqlRaw(
                "EXEC [dbo].[sp_RegistrarPoliza] @id_beneficiario, @id_tipo, @fecha_inicio, @fecha_fin, @estado, @id OUTPUT",
                idBeneficiarioParam, idTipoParam, fechaInicioParam, fechaFinParam, estadoParam, idPolizaParam
            );

            int result = (int)idPolizaParam.Value;
            Console.WriteLine($"Poliza registrada con ID: {result}"); // <-- Aquí verificamos el ID retornado
            return result;
        }



        public void AsignarPolizaABeneficiario(int idPoliza, int idBeneficiario)
        {
            var idPolizaParam = new SqlParameter("@idPoliza", idPoliza);
            var idBeneficiarioParam = new SqlParameter("@idBeneficiario", idBeneficiario);

            _context.Database.ExecuteSqlRaw(
                "EXEC [dbo].[sp_AsignarPolizaABeneficiario] @idPoliza, @idBeneficiario",
                idPolizaParam, idBeneficiarioParam
            );
        }


        public async Task<List<TipoPoliza>> ObtenerTipoPolizaAsync()
        {
            return await _context.TipoPolizas.ToListAsync();
        }


        public async Task<List<PolizaConTipo>> ObtenerPolizasPorFiltroAsync(string filtro)
        {
            return await (from poliza in _context.Polizas
                          join tipoPoliza in _context.TipoPolizas on poliza.IdTipo equals tipoPoliza.Id
                          join beneficiario in _context.Beneficiarios on poliza.IdBeneficiario equals beneficiario.Id
                          join usuario in _context.Usuarios on beneficiario.IdUsuario equals usuario.Id // Relación con Usuario
                          where poliza.Id.ToString() == filtro || usuario.Dni == filtro // Filtro por DNI
                          select new PolizaConTipo
                          {
                              IdPoliza = poliza.Id,
                              Vigencia = $"{poliza.FechaInicio:yyyy-MM-dd} - {poliza.FechaFin:yyyy-MM-dd}",
                              Cobertura = tipoPoliza.Descripcion,
                              Estado = poliza.Estado
                          }).ToListAsync();
        }

        public async Task ActualizarEstadoPolizaAsync(int idPoliza, string nuevoEstado)
        {
            var poliza = await _context.Polizas.FindAsync(idPoliza);
            if (poliza != null)
            {
                poliza.Estado = nuevoEstado;
                await _context.SaveChangesAsync();
            }
        }





    }
}
