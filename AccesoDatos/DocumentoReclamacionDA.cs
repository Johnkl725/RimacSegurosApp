using EntidadesProyecto;
using MiAplicacion.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DocumentoReclamacionDA
    {

        private readonly MyDbContext _context;

        public DocumentoReclamacionDA(MyDbContext context)
        {
            _context = context;
        }

        public void RegistrarDocumento(DocumentosReclamacion documento)
        {
            try
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_RegistrarDocumentoReclamacion @Nombre, @Archivo, @Extension, @IdReclamacion",
                    new SqlParameter("@Nombre", documento.Nombre),
                    new SqlParameter("@Archivo", documento.Archivo),
                    new SqlParameter("@Extension", documento.Extension),
                    new SqlParameter("@IdReclamacion", documento.IdReclamacion)
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar el documento: {ex.Message}");
            }
        }

        public List<DocumentosReclamacion> ObtenerDocumentosPorReclamacion(int idReclamacion)
        {
            return _context.DocumentosReclamacion
                .Where(d => d.IdReclamacion == idReclamacion)
                .ToList();
        }
    }
}
