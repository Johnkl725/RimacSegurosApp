using EntidadesProyecto;
using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiAplicacion.Models;
using AplicaciónWeb.Models;

namespace AplicaciónWeb.Controllers
{
    [Route("Personal/proveedor")]
    public class ProveedorController : Controller
    {
        private readonly ProveedorLN _proveedorLN;
        private readonly TallerLN _tallerLN;

        // Constructor que inyecta la lógica de negocio para Proveedor y Taller
        public ProveedorController(ProveedorLN proveedorLN, TallerLN tallerLN)
        {
            _proveedorLN = proveedorLN;
            _tallerLN = tallerLN;
        }

        // Acción que muestra la lista combinada de Proveedores y Talleres
        [HttpGet("ProveedoresYTalleres")]
        public async Task<IActionResult> ProveedoresYTalleres(string view = "Ambos")
        {
            // Guardamos la selección para que se mantenga en el dropdown
            ViewData["SelectedView"] = view;

            var viewModel = new ProveedorTallerViewModel
            {
                Proveedores = await _proveedorLN.ObtenerProveedoresAsync(),
                Talleres = await _tallerLN.ObtenerTodosLosTalleresAsync(),
            };

            return View(viewModel);
        }

        // GET: Muestra la vista de índice con todos los proveedores
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var proveedores = await _proveedorLN.ObtenerProveedoresAsync();
                Console.WriteLine("Proveedores obtenidos exitosamente.");
                return View(proveedores);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener proveedores: {ex.Message}");
                ModelState.AddModelError("", "No se pueden mostrar los proveedores en este momento.");
                return View("Error");
            }
        }

        // GET: Muestra la vista para crear un nuevo proveedor
        [HttpGet("crear")]
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Procesa el formulario de creación de proveedor
        [HttpPost("crear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("Intentando crear un proveedor...");
                    await _proveedorLN.AgregarProveedorAsync(proveedor);
                    Console.WriteLine("Proveedor creado exitosamente.");
                    return RedirectToAction("ProveedoresYTalleres", "Proveedor");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al crear proveedor: {ex.Message}");
                    ModelState.AddModelError("", "No se pudo crear el proveedor. Intente nuevamente.");
                }
            }
            else
            {
                Console.WriteLine("Modelo de proveedor no válido.");
            }
            return View(proveedor);
        }

        // GET: Muestra la vista para editar un proveedor existente
        [HttpGet("editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                var proveedor = await _proveedorLN.ObtenerProveedorPorIdAsync(id);
                if (proveedor == null)
                {
                    Console.WriteLine("Proveedor no encontrado para edición.");
                    return NotFound();
                }
                return View(proveedor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener proveedor para edición: {ex.Message}");
                ModelState.AddModelError("", "No se puede editar el proveedor en este momento.");
                return View("Error");
            }
        }

        // POST: Procesa el formulario de edición de proveedor
        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("Intentando actualizar proveedor...");
                    await _proveedorLN.ActualizarProveedorAsync(proveedor);
                    Console.WriteLine("Proveedor actualizado exitosamente.");
                    return RedirectToAction("ProveedoresYTalleres", "Proveedor");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar proveedor: {ex.Message}");
                    ModelState.AddModelError("", "No se pudo actualizar el proveedor. Intente nuevamente.");
                }
            }
            else
            {
                Console.WriteLine("Modelo de proveedor no válido para edición.");
            }
            return View(proveedor);
        }

        // GET: Muestra la vista de confirmación para eliminar un proveedor
        [HttpGet("eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var proveedor = await _proveedorLN.ObtenerProveedorPorIdAsync(id);
                if (proveedor == null)
                {
                    Console.WriteLine("Proveedor no encontrado para eliminación.");
                    return NotFound();
                }
                return View(proveedor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener proveedor para eliminación: {ex.Message}");
                ModelState.AddModelError("", "No se puede eliminar el proveedor en este momento.");
                return View("Error");
            }
        }

        // POST: Confirma la eliminación del proveedor
        [HttpPost("eliminar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            try
            {
                var proveedor = await _proveedorLN.ObtenerProveedorPorIdAsync(id);
                if (proveedor == null)
                {
                    return NotFound();
                }

                await _proveedorLN.EliminarProveedorAsync(id);
                Console.WriteLine("Proveedor eliminado exitosamente.");

                return RedirectToAction("ProveedoresYTalleres", "Proveedor");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar proveedor: {ex.Message}");
                ModelState.AddModelError("", "No se pudo eliminar el proveedor. Intente nuevamente.");
                return View("Error");
            }
        }
    }
}