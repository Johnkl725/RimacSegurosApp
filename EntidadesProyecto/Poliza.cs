using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntidadesProyecto
{
    public class Poliza
    {
        public int Id { get; set; } // Clave primaria

        [Column("id_usuario")] // Coincide con el nombre en la base de datos
        public int IdUsuario { get; set; }

        [Column("id_tipo")] // Coincide con el nombre en la base de datos
        public int IdTipo { get; set; }

        [Column("fecha_inicio")] // Coincide con el nombre en la base de datos
        public DateTime? FechaInicio { get; set; }

        [Column("fecha_fin")] // Coincide con el nombre en la base de datos
        public DateTime? FechaFin { get; set; }

        [Column("estado")] // Coincide con el nombre en la base de datos
        public string Estado { get; set; }

        // Propiedades de navegación
        public Usuario Usuario { get; set; }
        public TipoPoliza Tipo { get; set; }
    }

    public class TipoPoliza
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal MontoMensual { get; set; }
    }
}
