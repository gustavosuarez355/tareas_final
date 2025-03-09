using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tareas_final
{
    /// <summary>
    /// Representa un usuario dentro de la aplicación.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Obtiene o establece el identificador único del usuario.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Obtiene o establece el nombre del usuario.
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Usuario"/> con el identificador y el nombre especificados.
        /// </summary>
        /// <param name="id">El identificador único del usuario.</param>
        /// <param name="nombre">El nombre del usuario.</param>

        public Usuario(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        /// <summary>
        /// Devuelve una representación en cadena del objeto Usuario.
        /// </summary>
        /// <returns>El nombre del usuario.</returns>
        public override string ToString()
        {
            return Nombre;
        }
    }
}
