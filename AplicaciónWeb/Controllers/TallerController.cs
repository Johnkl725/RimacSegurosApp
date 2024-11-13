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
            List<Taller> talleres = await Task.Run(() => tallerLN.ObtenerTodosLosTalleres());
            return View(talleres);
        }

        public IActionResult Detalle(int id)
        {
            Taller taller = tallerLN.ObtenerTallerPorId(id);
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
                return RedirectToAction("Index");
            }

            // Volver a cargar la lista de proveedores si hay un error en el modelo
            ViewBag.Proveedores = await proveedorLN.ObtenerProveedoresAsync();
            return View(taller);
        }

        // Acción para mostrar el formulario de edición
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Taller taller = await Task.Run(() => tallerLN.ObtenerTallerPorId(id));
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
                return RedirectToAction("Index");
            }

            // Volver a cargar la lista de proveedores si hay un error en el modelo
            ViewBag.Proveedores = await proveedorLN.ObtenerProveedoresAsync();
            return View(taller);
        }

        // Acción para manejar la eliminación de un taller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await Task.Run(() => tallerLN.EliminarTaller(id));
            return RedirectToAction("Index");
        }
    }
}
