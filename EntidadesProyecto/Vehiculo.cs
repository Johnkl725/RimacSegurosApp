using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class Vehiculo
    {
        public int Id { get; set; } // ID del vehículo (clave primaria)
        public string Placa { get; set; } // Placa del vehículo
        public string Marca { get; set; } // Marca del vehículo
        public string Modelo { get; set; } // Modelo del vehículo
        public string Tipo { get; set; } // Tipo de vehículo (Ejemplo: Sedan, SUV, etc.)
        public int TarjetaVehiculo { get; set; }
    }
}
