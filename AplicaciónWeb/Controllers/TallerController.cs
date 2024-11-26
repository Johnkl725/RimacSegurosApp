using Microsoft.AspNetCore.Mvc;
using LogicaNegocio;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntidadesProyecto;

namespace AplicacionWeb.Controllers
{
    public class TallerController : Controller
    {
        private readonly TallerLN tallerLN;
        private readonly ProveedorLN proveedorLN;

        // Constructor que inyecta la lógica de negocio para Taller y Proveedor
        public TallerController(TallerLN tallerLN, ProveedorLN proveedorLN)
        {
            this.tallerLN = tallerLN;
            this.proveedorLN = proveedorLN;
        }

        // Acción que muestra la lista de talleres
        public async Task<IActionResult> Index()
        {
            var talleres = await tallerLN.ObtenerTodosLosTalleresAsync(); // Llamar al método correctamente
            return View(talleres);
        }

        // Acción para ver los detalles de un taller
        public async Task<IActionResult> Detalle(int id)
        {
            var taller = await Task.Run(() => tallerLN.ObtenerTallerPorId(id));
            if (taller == null)
                return NotFound();
            return View(taller);
        }

        // Acción para mostrar el formulario de creación
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            // Cargar la lista de proveedores de forma asíncrona para el menú desplegable
            ViewBag.Proveedores = await proveedorLN.ObtenerProveedoresAsync();
            return View();
        }

        // Acción para manejar el envío del formulario de creación
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Taller taller)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => tallerLN.AgregarTaller(taller));
                return RedirectToAction("ProveedoresYTalleres", "Proveedor");
            }

            // Volver a cargar la lista de proveedores si hay un error en el modelo
            ViewBag.Proveedores = await proveedorLN.ObtenerProveedoresAsync();
            return View(taller);
        }

        // Acción para mostrar el formulario de edición
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var taller = await Task.Run(() => tallerLN.ObtenerTallerPorId(id));
            if (taller == null) return NotFound();

            // Cargar la lista de proveedores de forma asíncrona para el menú desplegable
            ViewBag.Proveedores = await proveedorLN.ObtenerProveedoresAsync();
            return View(taller);
        }

        // Acción para manejar el envío del formulario de edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Taller taller)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => tallerLN.ActualizarTaller(taller));
                return RedirectToAction("ProveedoresYTalleres", "Proveedor");
            }

            // Volver a cargar la lista de proveedores si hay un error en el modelo
            ViewBag.Proveedores = await proveedorLN.ObtenerProveedoresAsync();
            return View(taller);
        }
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var taller = await Task.Run(() => tallerLN.ObtenerTallerPorId(id));
            if (taller == null)
            {
                return NotFound();
            }

            // Pasar mensaje de error (si existe) a la vista
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(taller);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            if (tallerLN.TallerTieneSiniestrosAsociados(id))
            {
                TempData["ErrorMessage"] = "No se puede eliminar el taller porque está asociado a uno o más siniestros.";
                return RedirectToAction("Eliminar", new { id });
            }

            try
            {
                await Task.Run(() => tallerLN.EliminarTaller(id));
                return RedirectToAction("ProveedoresYTalleres", "Proveedor");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Eliminar", new { id });
            }
        }


    }
}
