using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using EntidadesProyecto;
using System.Threading.Tasks;

namespace AplicaciónWeb.Controllers
{
    public class SeguimientoController : Controller
    {
        private readonly SiniestroLN _siniestroLN;

        public SeguimientoController(SiniestroLN siniestroLN)
        {
            _siniestroLN = siniestroLN;
        }

        public async Task<IActionResult> GestionarSeguimiento()
        {
            // Obtener el idUsuario, asumiendo que está autenticado correctamente
            int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            // Obtener los siniestros asociados al usuario
            var siniestros = await _siniestroLN.ObtenerSiniestrosPorBeneficiarioAsync(idUsuario);

            // Asegúrate de pasar un modelo no nulo
            if (siniestros == null)
            {
                siniestros = new List<Siniestro>(); // Devuelve una lista vacía si no hay siniestros
            }

            return View(siniestros);
        }



        public async Task<IActionResult> DetalleSeguimiento(int idSiniestro)
        {
            var seguimiento = await _siniestroLN.ObtenerSeguimientoCompletoAsync(idSiniestro);
            return View(seguimiento);
        }
    }
}
