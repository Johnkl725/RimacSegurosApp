using AccesoDatos;
using EntidadesProyecto;
using MiAplicacion.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            _context = context;
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

        public async Task<int> RegistrarDocumentoYObtenerId(DocumentosReclamacion documento)
        {
            if (documento == null)
                throw new ArgumentNullException(nameof(documento));

            return await _siniestroDA.RegistrarDocumentoYObtenerId(documento);
        }

        public async Task ActualizarSiniestroAsync(Siniestro siniestro)
        {
            await _siniestroDA.ActualizarSiniestroAsync(siniestro);
        }

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

        public async Task<List<Reclamacion>> ObtenerReclamacionesPorSiniestroAsync(int idSiniestro)
        {
            return await _siniestroDA.ObtenerReclamacionesPorSiniestroAsync(idSiniestro);
        }

        public async Task<Poliza> ObtenerPolizaActivaPorUsuarioAsync(int idUsuario)
        {
            var beneficiario = await _context.Beneficiarios
                .FirstOrDefaultAsync(b => b.IdUsuario == idUsuario);

            if (beneficiario == null)
            {
                throw new Exception("No se encontró un beneficiario para este usuario.");
            }

            var poliza = await _context.Polizas
                .FirstOrDefaultAsync(p => p.IdBeneficiario == beneficiario.Id && p.Estado == "Activo");

            if (poliza == null)
            {
                throw new Exception("No se encontró una póliza activa para este beneficiario.");
            }

            return poliza;
        }

        public async Task<Siniestro> ObtenerSeguimientoCompletoPorSiniestroAsync(int idSiniestro)
        {
            return await _context.Siniestros
                .Include(s => s.Presupuesto) // Incluye Presupuesto
                .Include(s => s.Taller)      // Incluye Taller
                .FirstOrDefaultAsync(s => s.IdSiniestro == idSiniestro);
        }

        public async Task<SeguimientoViewModel> ObtenerSeguimientoAsync(int idSiniestro)
        {
            return await _siniestroDA.ObtenerSeguimientoConSQLAsync(idSiniestro);
        }

        public async Task<List<Siniestro>> ObtenerSiniestrosSinTallerAsync()
        {
            return await _siniestroDA.ObtenerSiniestrosSinTallerAsync();
        }



    }
}
