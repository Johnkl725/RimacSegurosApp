using System.Collections.Generic;
using System.Linq;
using MiAplicacion.Data;
using EntidadesProyecto;
using Microsoft.EntityFrameworkCore;

public class AdminDA
{
    private readonly MyDbContext _context;

    public AdminDA(MyDbContext context)
    {
        _context = context;
    }

    public List<SiniestroPresupuestoViewModel> ObtenerSiniestrosConPresupuestos()
    {
        return _context.Siniestros
                       .Where(s => s.IdPresupuesto != null) // Solo siniestros con presupuesto asignado
                       .Join(_context.Polizas,
                             s => s.IdPoliza,
                             p => p.Id,
                             (s, p) => new { Siniestro = s, Poliza = p })
                       .Join(_context.Beneficiarios,
                             sp => sp.Poliza.IdBeneficiario,
                             b => b.Id,
                             (sp, b) => new { sp.Siniestro, sp.Poliza, Beneficiario = b })
                       .Join(_context.Vehiculos,
                             spb => spb.Beneficiario.IdVehiculo,
                             v => v.Id,
                             (spb, v) => new { spb.Siniestro, spb.Poliza, spb.Beneficiario, Vehiculo = v })
                       .Join(_context.Talleres,
                             spbv => spbv.Siniestro.IdTaller,
                             t => t.Id,
                             (spbv, t) => new SiniestroPresupuestoViewModel
                             {
                                 NumeroSiniestro = $"SIN-{spbv.Siniestro.IdSiniestro:D3}", // Formato "SIN-001"
                                 FechaAsignacion = spbv.Siniestro.FechaSiniestro, // Asumiendo que esta es la fecha de asignación
                                 NombreTaller = t.Nombre,
                                 TipoSiniestro = spbv.Siniestro.Tipo,
                                 Placa = spbv.Vehiculo.Placa, // Obteniendo la placa del vehículo
                                 IdSiniestro = spbv.Siniestro.IdSiniestro
                             })
                       .ToList();
    }





    // Obtener un siniestro por su Id con presupuesto detallado
    public Siniestro ObtenerSiniestroPorId(int id)
    {
        return _context.Siniestros
                       .Include(s => s.Presupuesto)
                       .FirstOrDefault(s => s.IdSiniestro == id);
    }

    // Actualizar y aprobar el presupuesto
    public void AprobarPresupuesto(int idPresupuesto)
    {
        var presupuesto = _context.Presupuestos.Find(idPresupuesto);
        if (presupuesto != null)
        {
            presupuesto.Estado = "Aprobado";
            _context.SaveChanges();
        }
    }

    // Obtener siniestros listos para pago de indemnización
    public List<Siniestro> ObtenerSiniestrosParaPago()
    {
        return _context.Siniestros
                       .Include(s => s.Presupuesto) // Incluye Presupuesto
                       .Where(s => s.Presupuesto != null)
                       .ToList();
    }



    // Marcar siniestro como pagado
    public void MarcarSiniestroComoPagado(int idSiniestro)
    {
        var siniestro = _context.Siniestros
                               .Include(s => s.Presupuesto)
                               .FirstOrDefault(s => s.IdSiniestro == idSiniestro);
        if (siniestro != null && siniestro.Presupuesto.Estado == "Aprobado")
        {
            siniestro.Presupuesto.Estado = "Pagado";
            _context.SaveChanges();
        }
    }
}
