using Microsoft.AspNetCore.Mvc;
using EntidadesProyecto;
using System.Collections.Generic;

public class AdminController : Controller
{
    private readonly AdminLN _adminLN;

    public AdminController(AdminLN adminLN)
    {
        _adminLN = adminLN;
    }
    public IActionResult AdminDashboard()
    {
        return View();
    }
    public IActionResult GestionarPresupuestos()
    {
        var siniestros = _adminLN.ObtenerSiniestrosConPresupuestos();
        return View(siniestros);
    }

    // Aprobar presupuesto de un siniestro
    [HttpPost]
    public IActionResult AprobarPresupuesto(int idPresupuesto)
    {
        bool resultado = _adminLN.AprobarPresupuesto(idPresupuesto);

        if (resultado)
        {
            TempData["SuccessMessage"] = "Presupuesto aprobado correctamente.";
        }
        else
        {
            TempData["ErrorMessage"] = "No se pudo aprobar el presupuesto.";
        }

        return RedirectToAction("GestionarPresupuestos");
    }

    public IActionResult GestionarPagosIndemnizacion()
    {
        var siniestros = _adminLN.ObtenerSiniestrosParaPago()
                                 .Where(s => s.Presupuesto != null)
                                 .ToList();
        return View(siniestros);
    }


    [HttpPost]
    public IActionResult PagarIndemnizacion(int idSiniestro)
    {
        bool resultado = _adminLN.PagarIndemnizacion(idSiniestro);

        if (resultado)
        {
            TempData["SuccessMessage"] = "Pago realizado correctamente.";
        }
        else
        {
            TempData["ErrorMessage"] = "No se pudo realizar el pago.";
        }

        return RedirectToAction("GestionarPagosIndemnizacion");
    }

}
