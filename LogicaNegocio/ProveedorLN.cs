using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntidadesProyecto;
using MiAplicacion.Data;
using Microsoft.EntityFrameworkCore;

namespace LogicaNegocio
{
    public class ProveedorLN
    {
        private readonly MyDbContext _context;
        private static List<Proveedor> _proveedoresCache;

        public ProveedorLN(MyDbContext context)
        {
            _context = context;
        }

        // Método para obtener proveedores usando la propiedad estática
        public async Task<List<Proveedor>> ObtenerProveedoresAsync()
        {
            // Solo consulta la base de datos si el caché está vacío
            if (_proveedoresCache == null)
            {
                _proveedoresCache = await _context.Proveedor.ToListAsync();
                Console.WriteLine("Proveedores cargados desde la base de datos y almacenados en caché.");
            }
            else
            {
                Console.WriteLine("Proveedores obtenidos desde el caché.");
            }

            return _proveedoresCache;
        }

        public Task<Proveedor> ObtenerProveedorPorIdAsync(int id)
        {
            return _context.Proveedor.FindAsync(id).AsTask();
        }

        public async Task AgregarProveedorAsync(Proveedor proveedor)
        {
            await _context.Proveedor.AddAsync(proveedor);
            await _context.SaveChangesAsync();
            _proveedoresCache = null; // Limpiar el caché para actualizar la próxima vez
        }

        public async Task ActualizarProveedorAsync(Proveedor proveedor)
        {
            _context.Proveedor.Update(proveedor);
            await _context.SaveChangesAsync();
            _proveedoresCache = null; // Limpiar el caché para actualizar la próxima vez
        }

        public async Task EliminarProveedorAsync(int id)
        {
            var proveedor = await ObtenerProveedorPorIdAsync(id);
            if (proveedor != null)
            {
                _context.Proveedor.Remove(proveedor);
                await _context.SaveChangesAsync();
                _proveedoresCache = null; // Limpiar el caché para actualizar la próxima vez
            }
        }
    }
}
