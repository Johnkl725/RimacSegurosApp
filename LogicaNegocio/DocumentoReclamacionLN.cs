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
    public  class DocumentoReclamacionLN
    {
        private readonly DocumentoReclamacionDA _documentosReclamacionDA;

        private readonly DocumentoReclamacionDA _documentoReclamacionDA;

        public DocumentoReclamacionLN(MyDbContext context)
        {
            _documentosReclamacionDA = new DocumentoReclamacionDA(context);
        }

        public void RegistrarDocumento(DocumentosReclamacion documento)
        {
            if (documento == null)
                throw new ArgumentNullException(nameof(documento));

            if (string.IsNullOrEmpty(documento.Nombre))
                throw new ArgumentException("El nombre del documento es obligatorio.");

            _documentosReclamacionDA.RegistrarDocumento(documento);
        }


        public List<DocumentosReclamacion> ObtenerDocumentosPorReclamacion(int idReclamacion)
        {
            if (idReclamacion <= 0)
                throw new ArgumentException("Id de reclamación inválido.");

            return _documentosReclamacionDA.ObtenerDocumentosPorReclamacion(idReclamacion);
        }
    }
}
