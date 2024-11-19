using AccesoDatos;
using EntidadesProyecto;
using MiAplicacion.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class SiniestroLN
    {
        private readonly SiniestroDA _siniestroDA;
        private readonly MyDbContext _context;

        public SiniestroLN(MyDbContext context)
        {
            _siniestroDA = new SiniestroDA(context);
            _context = context; // Inyecta el DbContext

        }

        public async Task RegistrarSiniestro(Siniestro siniestro)
        {
            // Validaciones de negocio (por ejemplo, verificar que los campos requeridos no estén vacíos)
            if (string.IsNullOrEmpty(siniestro.Tipo) || string.IsNullOrEmpty(siniestro.Ubicacion))
            {
                throw new ArgumentException("El tipo de siniestro y la ubicación son obligatorios.");
            }

            // Registrar el siniestro en la base de datos
            await _siniestroDA.RegistrarSiniestro(siniestro);
        }
        // SiniestroLN.cs
        public Task<List<Departamento>> ObtenerDepartamentosAsync()
        {
            return _siniestroDA.ObtenerDepartamentosAsync();
        }

        public Task<List<Provincia>> ObtenerProvinciasPorDepartamentoAsync(int departamentoId)
        {
            return _siniestroDA.ObtenerProvinciasPorDepartamentoAsync(departamentoId);
        }

        public Task<List<Distrito>> ObtenerDistritosPorProvinciaAsync(int provinciaId)
        {
            return _siniestroDA.ObtenerDistritosPorProvinciaAsync(provinciaId);
        }
        public async Task<List<Siniestro>> ObtenerTodosLosSiniestros()
        {
            return await _siniestroDA.ObtenerTodosLosSiniestrosAsync();
        }
        public async Task<List<Siniestro>> ObtenerSiniestrosPorBeneficiarioAsync(int idBeneficiario)
        {
            return await _siniestroDA.ObtenerSiniestrosPorBeneficiarioAsync(idBeneficiario);
        }

        public Task<List<Siniestro>> ObtenerSiniestrosConTallerPorDefectoAsync(int idTallerPorDefecto)
        {
            return _siniestroDA.ObtenerSiniestrosConTallerPorDefectoAsync(idTallerPorDefecto);
        }

        public Task<Siniestro> ObtenerSiniestroPorIdAsync(int idSiniestro)
        {
            return _siniestroDA.ObtenerSiniestroPorIdAsync(idSiniestro);
        }

        public async Task ActualizarSiniestroAsync(Siniestro siniestro)
        {
            await _siniestroDA.ActualizarSiniestroAsync(siniestro);
        }

        public async Task<List<Reclamacion>> ObtenerReclamacionesPorSiniestroAsync(int idSiniestro)
        {
            return await _siniestroDA.ObtenerReclamacionesPorSiniestroAsync(idSiniestro);
        }



        public async Task<SeguimientoViewModel> ObtenerSeguimientoCompletoAsync(int idSiniestro)
        {
            var siniestro = await _siniestroDA.ObtenerSiniestroConDetallesAsync(idSiniestro);
            var reclamaciones = await _siniestroDA.ObtenerReclamacionesPorSiniestroAsync(idSiniestro);

            return new SeguimientoViewModel
            {
                Siniestro = siniestro,
                Reclamaciones = reclamaciones,
                Presupuesto = siniestro.Presupuesto,
                EstadoActual = siniestro.Presupuesto?.Estado ?? "Sin presupuesto"
            };

        }

        public async Task<Poliza> ObtenerPolizaActivaPorUsuarioAsync(int idUsuario)
        {
            var beneficiario = await _context.Beneficiarios
                .Where(b => b.IdUsuario == idUsuario)
                .FirstOrDefaultAsync();

            if (beneficiario == null)
            {
                throw new Exception("No se encontró un beneficiario para este usuario.");
            }

            var poliza = await _context.Polizas
                .Where(p => p.IdBeneficiario == beneficiario.Id && p.Estado == "Activo")
                .FirstOrDefaultAsync();

            if (poliza == null)
            {
                throw new Exception("No se encontró una póliza activa para este beneficiario.");
            }

            return poliza;
        }



    }
}
