using System.ComponentModel.DataAnnotations;

namespace EntidadesProyecto
{
    public class Taller
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; } // Clave foránea que referencia a Proveedor
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

    }
}