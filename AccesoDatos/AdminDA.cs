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

        if (siniestro == null)
        {
            throw new Exception("No se encontró el siniestro especificado.");
        }

        siniestro.Presupuesto.Estado = "Pagado";
        _context.SaveChanges();
    }

    // Método para actualizar presupuesto sin interferir con otros métodos
    public void ActualizarPresupuesto(Presupuesto presupuesto)
    {
        var presupuestoExistente = _context.Presupuestos.Find(presupuesto.Id);
        if (presupuestoExistente != null)
        {
            presupuestoExistente.Detalles = presupuesto.Detalles;
            presupuestoExistente.MontoTotal = presupuesto.MontoTotal;
            presupuestoExistente.Estado = presupuesto.Estado;

            _context.SaveChanges();
        }
    }

    // Método para obtener siniestro con datos de presupuesto y vehículo
    public SiniestroPresupuestoViewModel ObtenerDetallesSiniestroPresupuesto(int idSiniestro)
    {
        return _context.Siniestros
                       .Where(s => s.IdSiniestro == idSiniestro)
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
                       .Select(spbv => new SiniestroPresupuestoViewModel
                       {
                           NumeroSiniestro = $"SIN-{spbv.Siniestro.IdSiniestro:D3}",
                           TipoSiniestro = spbv.Siniestro.Tipo,
                           Placa = spbv.Vehiculo.Placa,
                           IdSiniestro = spbv.Siniestro.IdSiniestro
                       })
                       .FirstOrDefault();
    }

    public int CrearPresupuesto(Presupuesto presupuesto)
    {
        _context.Presupuestos.Add(presupuesto);
        _context.SaveChanges();
        return presupuesto.Id; // Retorna el ID del nuevo presupuesto
    }

    public void ActualizarIdPresupuestoEnSiniestro(int idSiniestro, int idPresupuesto)
    {
        var siniestro = _context.Siniestros.Find(idSiniestro);
        if (siniestro != null)
        {
            siniestro.IdPresupuesto = idPresupuesto;
            _context.SaveChanges();
        }
    }
}