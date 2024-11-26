using System.Collections.Generic;
using EntidadesProyecto;
using AccesoDatos;

namespace LogicaNegocio
{
    public class TallerLN
    {
        private readonly TallerDA tallerDA;

        // Constructor que recibe TallerDA para acceder a los datos
        public TallerLN(TallerDA tallerDA)
        {
            this.tallerDA = tallerDA;
        }

        // Método para agregar un taller
        public int AgregarTaller(Taller taller)
        {
            return tallerDA.AgregarTaller(taller);
        }

        // Método para actualizar un taller
        public void ActualizarTaller(Taller taller)
        {
            tallerDA.ActualizarTaller(taller);
        }    // Método para verificar si un taller tiene siniestros asociados
        public bool TallerTieneSiniestrosAsociados(int idTaller)
        {
            return tallerDA.TieneSiniestrosAsociados(idTaller);
        }

        // Método para eliminar un taller
        public void EliminarTaller(int id)
        {
            try
            {
                tallerDA.EliminarTaller(id);
            }
            catch (InvalidOperationException ex)
            {
                // Re-lanzar la excepción con un mensaje claro
                throw new InvalidOperationException($"No se puede eliminar el taller: {ex.Message}");
            }
        }


        // Método para obtener un taller por su ID
        public Taller ObtenerTallerPorId(int id)
        {
            return tallerDA.ObtenerTallerPorId(id);
        }

        public async Task<List<Taller>> ObtenerTodosLosTalleresAsync()
        {
            return await tallerDA.ObtenerTodosLosTalleresAsync();
        }

    }
}