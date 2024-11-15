using EntidadesProyecto;
using MiAplicacion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace LogicaNegocio
{
    public class PolizaLN
    {
        private readonly PolizaDA _polizaDA;

        // Constructor que recibe el contexto de la base de datos (DbContext)
        public PolizaLN(MyDbContext context)
        {
            _polizaDA = new PolizaDA(context);
        }

        // Método para crear y registrar una nueva póliza
        public int CrearPoliza(int idBeneficiario, int idTipo, DateTime fechaInicio, DateTime fechaFin, string estado)
        {
            return _polizaDA.RegistrarPoliza(idBeneficiario, idTipo, fechaInicio, fechaFin, estado);
        }

        // Método para asignar una póliza existente a un beneficiario
        public void AsignarPolizaABeneficiario(int idPoliza, int idBeneficiario)
        {
            // Validación simple de IDs positivos
            if (idPoliza <= 0 || idBeneficiario <= 0)
            {
                throw new ArgumentException("IDs de póliza y beneficiario deben ser positivos.");
            }

            _polizaDA.AsignarPolizaABeneficiario(idPoliza, idBeneficiario);
        }


        public Task<List<TipoPoliza>> ObtenerTipoPolizaAsync()
        {
            return _polizaDA.ObtenerTipoPolizaAsync();
        }

    }

}