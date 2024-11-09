using AccesoDatos;
using EntidadesProyecto;
using MiAplicacion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class BeneficiarioLN
    {
        private readonly BeneficiarioDA _beneficiarioDA;

        public BeneficiarioLN(MyDbContext context)
        {
            _beneficiarioDA = new BeneficiarioDA(context);
        }

        // Registrar un beneficiario con el id_usuario y el id_vehiculo
        public void RegistrarBeneficiario(int id_usuario, int id_vehiculo, string password)
        {
            _beneficiarioDA.RegistrarBeneficiario(id_usuario, id_vehiculo, password);
        }
        public void asignarIDVehiculo(int idBeneficiario, int idVehiculo)
        {
            _beneficiarioDA.AsignarIDVehiculo(idBeneficiario,idVehiculo);
        }

        public void ActualizarIdVehiculoBeneficiario(int idVehiculo)
        {
            _beneficiarioDA.ActualizarIdVehiculoBeneficiario(idVehiculo);
        }

    }
}
