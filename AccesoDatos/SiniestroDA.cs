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

        public async Task<List<Siniestro>> ObtenerSiniestrosConTallerPorDefectoAsync(int idTallerPorDefecto)
        {
            return await _context.Siniestros
                .Where(s => s.IdTaller == idTallerPorDefecto)
                .Select(s => new Siniestro
                {
                    IdSiniestro = s.IdSiniestro,
                    Tipo = s.Tipo,
                    FechaSiniestro = s.FechaSiniestro,
                    Ubicacion = s.Ubicacion,
                    Descripcion = s.Descripcion,
                    IdDepartamento = s.IdDepartamento,
                    IdProvincia = s.IdProvincia,
                    IdDistrito = s.IdDistrito,
                    IdTaller = s.IdTaller
                }) // Selección explícita de propiedades
                .ToListAsync();
        }




        public async Task<Siniestro?> ObtenerSiniestroPorIdAsync(int idSiniestro)
        {
            return await _context.Siniestros
                .Where(s => s.IdSiniestro == idSiniestro)
                .Include(s => s.Taller)
                .Select(s => new Siniestro
                {
                    IdSiniestro = s.IdSiniestro,
                    Descripcion = s.Descripcion,
                    FechaActualizacion = s.FechaActualizacion,
                    FechaCreacion = s.FechaCreacion,
                    FechaSiniestro = s.FechaSiniestro,
                    IdDepartamento = s.IdDepartamento,
                    IdDistrito = s.IdDistrito,
                    IdDocumento = s.IdDocumento,
                    IdPoliza = s.IdPoliza,
                    IdPresupuesto = s.IdPresupuesto,
                    IdProvincia = s.IdProvincia,
                    IdTaller = s.IdTaller,
                    Tipo = s.Tipo,
                    Ubicacion = s.Ubicacion,
                    Taller = s.Taller != null ? new Taller
                    {
                        Id = s.Taller.Id,
                        Calificacion = s.Taller.Calificacion,
                        Capacidad = s.Taller.Capacidad,
                        Ciudad = s.Taller.Ciudad,
                        Correo = s.Taller.Correo,
                        Descripcion = s.Taller.Descripcion,
                        Direccion = s.Taller.Direccion,
                        Estado = s.Taller.Estado,
                        IdProveedor = s.Taller.IdProveedor,
                        Nombre = s.Taller.Nombre,
                        Telefono = s.Taller.Telefono,
                        Tipo = s.Taller.Tipo
                    } : null
                })
                .FirstOrDefaultAsync();
        }




        public async Task ActualizarSiniestroAsync(Siniestro siniestro)
        {
            try
            {
                var entidadExistente = await _context.Siniestros
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.IdSiniestro == siniestro.IdSiniestro);

                if (entidadExistente == null)
                {
                    throw new Exception("El siniestro no existe en la base de datos.");
                }

                _context.Entry(siniestro).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el siniestro: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Siniestro>> ObtenerTodosLosSiniestrosAsync()
        {
            return await _context.Siniestros.ToListAsync();
        }
        

        

     
        public async Task<List<Siniestro>> ObtenerSiniestrosPorBeneficiarioAsync(int idBeneficiario)
        {
            var polizas = await _context.Polizas
                .Where(p => p.IdBeneficiario == idBeneficiario)
                .Select(p => p.Id)
                .ToListAsync();

            return await _context.Siniestros
                .Where(s => polizas.Contains(s.IdPoliza.Value))
                .ToListAsync();
        }

        public async Task<Siniestro> ObtenerSiniestroConDetallesAsync(int idSiniestro)
        {
            return await _context.Siniestros
                .Include(s => s.Presupuesto)
                .Include(s => s.Poliza)
                .ThenInclude(p => p.TipoPoliza)
                .Include(s => s.Taller)
                .FirstOrDefaultAsync(s => s.IdSiniestro == idSiniestro);
        }

        public async Task<List<Reclamacion>> ObtenerReclamacionesPorSiniestroAsync(int idSiniestro)
        {
            return await _context.Reclamaciones
                .Where(r => r.IdSiniestro == idSiniestro)
                .Include(r => r.Documentos)
                .ToListAsync();
        }


    }
}
