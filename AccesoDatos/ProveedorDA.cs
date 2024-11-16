using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntidadesProyecto;
using System.Collections.Generic;
using MiAplicacion.Data;
using System.Data;

public class ProveedorDA
{
    private readonly MyDbContext _context;

    public ProveedorDA(MyDbContext context)
    {
        _context = context;
    }

    // Método para obtener todos los proveedores
    public async Task<List<Proveedor>> ObtenerProveedoresAsync()
    {
        return await _context.Proveedor.FromSqlRaw("EXEC spObtenerProveedores").ToListAsync();
    }

    // Método para obtener un proveedor por ID
    public async Task<Proveedor> ObtenerProveedorPorIdAsync(int id)
    {
        var proveedores = await _context.Proveedor
            .FromSqlRaw("EXEC spObtenerProveedorPorId @Id", new SqlParameter("@Id", id))
            .ToListAsync();

        return proveedores.FirstOrDefault();
    }

    // Método para agregar un nuevo proveedor
    public async Task AgregarProveedorAsync(Proveedor proveedor)
    {
        var parameters = new[]
        {
        new SqlParameter("@Nombre", proveedor.Nombre),
        new SqlParameter("@Ruc", proveedor.Ruc),
        new SqlParameter("@Telefono", proveedor.Telefono),
        new SqlParameter("@Correo", proveedor.Correo),
        new SqlParameter("@Calificacion", proveedor.Calificacion ?? (object)DBNull.Value),
        new SqlParameter("@Descripcion", proveedor.Descripcion),
        new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output }  // Parámetro de salida
    };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC spAgregarProveedor @Nombre, @Ruc, @Telefono, @Correo, @Calificacion, @Descripcion, @Id OUTPUT",
            parameters);

        // Obtener el ID generado (opcional)
        var generatedId = (int)parameters[6].Value;
        Console.WriteLine($"Nuevo ID de proveedor: {generatedId}");
    }


    // Método para actualizar un proveedor existente
    public async Task ActualizarProveedorAsync(Proveedor proveedor)
    {
        var parameters = new[]
        {
            new SqlParameter("@Id", proveedor.Id),
            new SqlParameter("@Nombre", proveedor.Nombre),
            new SqlParameter("@Ruc", proveedor.Ruc),
            new SqlParameter("@Telefono", proveedor.Telefono),
            new SqlParameter("@Correo", proveedor.Correo),
            new SqlParameter("@Calificacion", proveedor.Calificacion ?? (object)DBNull.Value),
            new SqlParameter("@Descripcion", proveedor.Descripcion)
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC spActualizarProveedor @Id, @Nombre, @Ruc, @Telefono, @Correo, @Calificacion, @Descripcion", parameters);
    }

    // Método para eliminar un proveedor por ID
    public async Task EliminarProveedorAsync(int id)
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC spEliminarProveedor @Id", new SqlParameter("@Id", id));
    }
}