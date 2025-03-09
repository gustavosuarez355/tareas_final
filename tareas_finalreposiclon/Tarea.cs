using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tareas_final
{
    /// <summary>
    /// Obtiene o establece el identificador único de la tarea.
    /// </summary>
    public class Tarea
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la tarea.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Obtiene o establece el título de la tarea.
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Obtiene o establece la descripción de la tarea.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Obtiene o establece la categoría a la que pertenece la tarea.
        /// </summary>
        public Categoria Categoria { get; set; }

        /// <summary>
        /// Obtiene o establece el estado actual de la tarea.
        /// </summary>
        public Estado Estado { get; set; }

        /// <summary>
        /// Obtiene o establece el usuario asignado a la tarea.
        /// </summary>
        public Usuario AsignadoA { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Tarea"/> con los valores especificados.
        /// </summary>
        /// <param name="id">El identificador único de la tarea.</param>
        /// <param name="titulo">El título de la tarea.</param>
        /// <param name="descripcion">La descripción de la tarea.</param>
        /// <param name="categoria">La categoría a la que pertenece la tarea.</param>
        /// <param name="estado">El estado actual de la tarea.</param>
        /// <param name="asignadoA">El usuario asignado a la tarea.</param>

        public Tarea(int id, string titulo, string descripcion, Categoria categoria, Estado estado, Usuario asignadoA)
        {
            Id = id;
            Titulo = titulo;  
            Descripcion = descripcion;
            Categoria = categoria;
            Estado = estado;
            AsignadoA = asignadoA;
        }
        /// <summary>
        /// Devuelve una representación en cadena de la tarea.
        /// </summary>
        /// <returns>Una cadena que incluye el título, la descripción y el nombre del estado de la tarea.</returns>
        public override string ToString()
        {
            return $"{Titulo} - {Descripcion} - {Estado.Nombre}";
        }
    }
}