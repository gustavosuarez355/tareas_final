using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tareas_final
{
    /// <summary>
    /// Representa una categoría dentro de la aplicación.
    /// </summary>
    public class Categoria
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la categoría.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Obtiene o establece el nombre de la categoría.
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Categoria"/> con el identificador y nombre especificados.
        /// </summary>
        /// <param name="id">El identificador único de la categoría.</param>
        /// <param name="nombre">El nombre de la categoría.</param>

        public Categoria(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        /// <summary>
        /// Devuelve una representación en cadena del objeto, que es el nombre de la categoría.
        /// </summary>
        /// <returns>El nombre de la categoría.</returns>
        public override string ToString()
        {
            return Nombre;
        }
    }
}