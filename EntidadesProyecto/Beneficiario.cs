using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class Beneficiario : Usuario
    {
        public int IdUsuario { get; set; }
        public int IdVehiculo { get; set; }
        public string Contraseña { get; set; }
    }
}
