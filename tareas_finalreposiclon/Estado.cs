using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tareas_final
{
    /// <summary>
    /// Representa el estado de una tarea en la aplicación.
    /// </summary>
    public class Estado
    {
        /// <summary>
        /// Obtiene o establece el identificador único del estado.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del estado.
        /// </summary>
        public string Nombre { get; set; }


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Estado"/> con el identificador y el nombre especificados.
        /// </summary>
        /// <param name="id">El identificador único del estado.</param>
        /// <param name="nombre">El nombre del estado.</param>
        public Estado(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        /// <summary>
        /// Devuelve una representación en cadena del estado.
        /// </summary>
        /// <returns>El nombre del estado.</returns>
        public override string ToString()
        {
            return Nombre;
        }
    }   
}
            