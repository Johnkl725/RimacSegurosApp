using EntidadesProyecto;
using System.ComponentModel.DataAnnotations.Schema;

public class Taller
{
    public int Id { get; set; }

    [ForeignKey("Proveedor")] // Especifica explícitamente que esta es la clave externa
    [Column("id_proveedor")] // Si el nombre en la base de datos es distinto
    public int IdProveedor { get; set; }

    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    public string Ciudad { get; set; }
    public string Tipo { get; set; }
    public int Capacidad { get; set; }
    public string Descripcion { get; set; }
    public string Calificacion { get; set; }
    public string Estado { get; set; }

    // Propiedad de navegación para Proveedor
    public Proveedor Proveedor { get; set; }

    // Relación con Siniestro
    public ICollection<Siniestro> Siniestros { get; set; } = new List<Siniestro>();
}
