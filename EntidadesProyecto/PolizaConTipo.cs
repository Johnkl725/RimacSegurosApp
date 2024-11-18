using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class PolizaConTipo
    {
        public int IdPoliza { get; set; }
        public string Vigencia { get; set; }
        public string Cobertura { get; set; } // Descripción del tipo de póliza
        public string Estado { get; set; }
    }

}
