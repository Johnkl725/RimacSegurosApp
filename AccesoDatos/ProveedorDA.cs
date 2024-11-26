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

    // Método para verificar si un proveedor tiene talleres asociados
    public bool TieneTalleresAsociados(int idProveedor)
    {
        using (var connection = _context.Database.GetDbConnection())
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Taller WHERE id_proveedor = @IdProveedor";
            command.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
            connection.Open();

            int count = (int)command.ExecuteScalar();
            return count > 0; // Devuelve true si hay talleres asociados
        }
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
            new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output }
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC spAgregarProveedor @Nombre, @Ruc, @Telefono, @Correo, @Calificacion, @Descripcion, @Id OUTPUT",
            parameters);
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
        // Verificar si hay talleres asociados antes de intentar eliminar
        if (TieneTalleresAsociados(id))
        {
            throw new InvalidOperationException("No se puede eliminar el proveedor porque tiene talleres asociados.");
        }

        await _context.Database.ExecuteSqlRawAsync("EXEC spEliminarProveedor @Id", new SqlParameter("@Id", id));
    }
}
