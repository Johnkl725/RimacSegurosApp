using System.Collections.Generic;
using EntidadesProyecto;

public class AdminLN
{
    private readonly AdminDA _adminDA;

    public AdminLN(AdminDA adminDA)
    {
        _adminDA = adminDA;
    }

    // Obtener todos los siniestros con presupuestos
    // Método para obtener siniestros con presupuestos asignados
    public List<SiniestroPresupuestoViewModel> ObtenerSiniestrosConPresupuestos()
    {
        return _adminDA.ObtenerSiniestrosConPresupuestos();
    }

    // Aprobar el presupuesto de un siniestro
    public bool AprobarPresupuesto(int idPresupuesto)
    {
        var presupuesto = _adminDA.ObtenerSiniestroPorId(idPresupuesto)?.Presupuesto;
        if (presupuesto == null || presupuesto.Estado == "Aprobado")
        {
            return false;
        }

        _adminDA.AprobarPresupuesto(idPresupuesto);
        return true;
    }

    // Obtener siniestros listos para pago
    public List<Siniestro> ObtenerSiniestrosParaPago()
    {
        return _adminDA.ObtenerSiniestrosParaPago();
    }

    // Procesar pago de indemnización
    public bool PagarIndemnizacion(int idSiniestro)
    {
        var siniestro = _adminDA.ObtenerSiniestroPorId(idSiniestro);

        if (siniestro == null || siniestro.Presupuesto.Estado != "Aprobado")
        {
            return false;
        }

        _adminDA.MarcarSiniestroComoPagado(idSiniestro);
        return true;
    }

    public Siniestro ObtenerSiniestroPorId(int id)
    {
        return _adminDA.ObtenerSiniestroPorId(id);
    }

    public SiniestroPresupuestoViewModel ObtenerDetallesSiniestroPresupuesto(int idSiniestro)
    {
        return _adminDA.ObtenerDetallesSiniestroPresupuesto(idSiniestro);
    }

    public void ActualizarPresupuesto(Presupuesto presupuesto)
    {
        _adminDA.ActualizarPresupuesto(presupuesto);
    }

    public int CrearPresupuesto(Presupuesto presupuesto)
    {
        return _adminDA.CrearPresupuesto(presupuesto);
    }

    public void ActualizarIdPresupuestoEnSiniestro(int idSiniestro, int idPresupuesto)
    {
        _adminDA.ActualizarIdPresupuestoEnSiniestro(idSiniestro, idPresupuesto);
    }
}