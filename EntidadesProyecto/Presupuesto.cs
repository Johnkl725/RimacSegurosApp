using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class Presupuesto
    {
        public int Id { get; set; } // Clave primaria

        // Propiedades de Presupuesto
        public DateTime FechaEmision { get; set; } // fecha_emision
        public decimal MontoTotal { get; set; } // monto_total
        public string Detalles { get; set; } // detalles
        public decimal Impuestos { get; set; } // impuestos
        public decimal CostoSinImpuestos { get; set; } // costo_sin_impuestos
        public string Estado { get; set; } // estado
        public decimal Descuento { get; set; } // descuento
        public string Enlace { get; set; } // enlace
    }
}
