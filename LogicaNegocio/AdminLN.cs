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

    public List<SiniestroPresupuestoViewModel> ObtenerSiniestrosSinPresupuesto()
    {
        return _adminDA.ObtenerSiniestrosSinPresupuesto();
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
    public void PagarIndemnizacion(int idSiniestro)
    {
        // Obtener el siniestro desde la capa de datos
        var siniestro = _adminDA.ObtenerSiniestroPorId(idSiniestro);

        if (siniestro == null)
        {
            throw new Exception("El siniestro no existe.");
        }

        // Marcar como pagado
        _adminDA.MarcarSiniestroComoPagado(idSiniestro);
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