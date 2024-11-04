using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class PerfilViewModel
    {
        public string Dni { get; set; }           // Documento de identidad del usuario
        public string Nombres { get; set; }       // Nombre completo del usuario
        public string Apellido1 { get; set; }     // Primer apellido del usuario
        public string Apellido2 { get; set; }     // Segundo apellido del usuario
    }

}
