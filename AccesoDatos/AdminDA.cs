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

    // Obtener todos los siniestros con presupuestos para revisar
    public List<Siniestro> ObtenerSiniestrosConPresupuestos()
    {
        return _context.Siniestros
                       .Include(s => s.Presupuesto)
                       .Where(s => s.Presupuesto != null)
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
