using MiAplicacion.Data;
using EntidadesProyecto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace AccesoDatos
{
    public class SiniestroDA
    {
        private readonly MyDbContext _context;

        public SiniestroDA(MyDbContext context)
        {
            _context = context;
        }

        // Método para registrar un siniestro
        public async Task RegistrarSiniestro(Siniestro siniestro)
        {
            try
            {
                // Asignar valores por defecto si están null
                siniestro.IdDocumento = siniestro.IdDocumento ?? 1;
                siniestro.IdPoliza = siniestro.IdPoliza ?? 1;
                siniestro.IdTaller = siniestro.IdTaller ?? 1;
                siniestro.IdPresupuesto = siniestro.IdPresupuesto ?? 1;

                // Crear los parámetros para el procedimiento almacenado
                var idParam = new SqlParameter("@IdSiniestro", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento para registrar el siniestro
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_RegistrarSiniestro @Tipo, @FechaSiniestro, @Departamento, @Provincia, @Distrito, @Ubicacion, @Descripcion, @IdDocumento, @IdPoliza, @IdTaller, @IdPresupuesto, @IdSiniestro OUTPUT",
                    new SqlParameter("@Tipo", siniestro.Tipo),
                    new SqlParameter("@FechaSiniestro", siniestro.FechaSiniestro),
                    new SqlParameter("@Departamento", siniestro.IdDepartamento),
                    new SqlParameter("@Provincia", siniestro.IdProvincia),
                    new SqlParameter("@Distrito", siniestro.IdDistrito),
                    new SqlParameter("@Ubicacion", siniestro.Ubicacion),
                    new SqlParameter("@Descripcion", siniestro.Descripcion),
                    new SqlParameter("@IdDocumento", siniestro.IdDocumento),
                    new SqlParameter("@IdPoliza", siniestro.IdPoliza),
                    new SqlParameter("@IdTaller", siniestro.IdTaller),
                    new SqlParameter("@IdPresupuesto", siniestro.IdPresupuesto),
                    idParam
                );

                // Obtener el ID del siniestro insertado
                siniestro.IdSiniestro = (int)idParam.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error al registrar el siniestro.");
            }
        }
        public async Task<List<Departamento>> ObtenerDepartamentosAsync()
        {
            return await _context.Departamento.ToListAsync();
        }

        public async Task<List<Provincia>> ObtenerProvinciasPorDepartamentoAsync(int departamentoId)
        {
            return await _context.Provincia
                .Where(p => p.id_departamento == departamentoId) // Asumiendo que el campo es IdDepartamento
                .ToListAsync();
        }

        public async Task<List<Distrito>> ObtenerDistritosPorProvinciaAsync(int provinciaId)
        {
            return await _context.Distrito
                .Where(d => d.id_provincia == provinciaId)
                .ToListAsync();
        }
        public async Task<List<Siniestro>> ObtenerTodosLosSiniestrosAsync()
        {
            try
            {
                return await _context.Siniestros.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error al obtener los siniestros.");
            }
        }
        public async Task<List<Siniestro>> ObtenerSiniestrosPorBeneficiarioAsync(int idBeneficiario)
        {
            // Materializamos la lista de IdPoliza antes de usar Contains
            var polizas = await _context.Polizas
                .Where(p => p.IdBeneficiario == idBeneficiario)
                .Select(p => p.Id)
                .ToListAsync(); // Materialización aquí

            return await _context.Siniestros
            .Where(s => s.IdPoliza.HasValue && polizas.Contains(s.IdPoliza.Value)) // Asegúrate de que IdPoliza no sea nulo
            .ToListAsync();

        }

    }
}
