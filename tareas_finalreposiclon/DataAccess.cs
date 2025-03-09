using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace tareas_final
{

    /// <summary>
    /// Proporciona métodos para acceder a la base de datos y realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar)
    /// sobre las entidades de la aplicación (Tareas, Categorías, Estados y Usuarios).
    /// </summary>
    public class DataAccess
    {
        private readonly string connectionString;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DataAccess"/> con la cadena de conexión especificada.
        /// </summary>
        /// <param name="connectionString">La cadena de conexión a la base de datos.</param>
        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DataAccess"/> con la cadena de conexión especificada.
        /// </summary>
        /// <param name="connectionString">La cadena de conexión a la base de datos.</param>
        public DataTable ObtenerTareas()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = @"SELECT T.id, T.titulo, T.descripcion, 
                                           C.nombre AS categoria, E.nombreEstado AS estado
                                    FROM Tareas T
                                    INNER JOIN Categorias C ON T.categoriaId = C.id
                                    INNER JOIN Estados E ON T.estadoId = E.id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Agrega una nueva tarea a la base de datos.
        /// </summary>
        /// <param name="tarea">El objeto <see cref="Tarea"/> que contiene la información de la tarea a insertar.</param>
        /// <returns>El identificador único de la tarea insertada.</returns>
        public int AgregarTarea(Tarea tarea)
        {
            int nuevoId = 0;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = @"
            INSERT INTO Tareas (titulo, descripcion, categoriaId, estadoId) 
            OUTPUT INSERTED.id 
            VALUES (@titulo, @descripcion, @categoriaId, @estadoId)";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@titulo", tarea.Titulo);
                    cmd.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
                    cmd.Parameters.AddWithValue("@categoriaId", tarea.Categoria.Id);
                    cmd.Parameters.AddWithValue("@estadoId", tarea.Estado.Id);

                    nuevoId = (int)cmd.ExecuteScalar();
                }
            }
            return nuevoId;
        }
        /// <summary>
        /// Actualiza los datos de una tarea existente en la base de datos.
        /// </summary>
        /// <param name="id">El identificador de la tarea a actualizar.</param>
        /// <param name="titulo">El nuevo título de la tarea.</param>
        /// <param name="descripcion">La nueva descripción de la tarea.</param>
        /// <param name="categoriaId">El identificador de la nueva categoría.</param>
        /// <param name="estadoId">El identificador del nuevo estado.</param>
        public void ActualizarTarea(int id, string titulo, string descripcion, int categoriaId, int estadoId)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = @"
                UPDATE Tareas 
                SET titulo = @titulo, 
                    descripcion = @descripcion, 
                    categoriaId = @categoriaId, 
                    estadoId = @estadoId
                WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@titulo", titulo);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                    cmd.Parameters.AddWithValue("@estadoId", estadoId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina una tarea de la base de datos.
        /// </summary>
        /// <param name="idTarea">El identificador de la tarea a eliminar.</param>
        public void EliminarTarea(int idTarea)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = "DELETE FROM Tareas WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idTarea);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Obtiene todas las categorías de la base de datos.
        /// </summary>
        /// <returns>Un objeto <see cref="DataTable"/> que contiene las categorías.</returns>
        public DataTable ObtenerCategorias()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = "SELECT * FROM Categorias";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Agrega una nueva categoría a la base de datos.
        /// </summary>
        /// <param name="categoria">El objeto <see cref="Categoria"/> que contiene la información de la categoría a insertar.</param>
        public void AgregarCategoria(Categoria categoria)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = "INSERT INTO Categorias (nombre) VALUES (@nombre)";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Obtiene todos los estados de la base de datos.
        /// </summary>
        /// <returns>Un objeto <see cref="DataTable"/> que contiene los estados.</returns>
        public DataTable ObtenerEstados()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = "SELECT * FROM Estados";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Agrega un nuevo estado a la base de datos.
        /// </summary>
        /// <param name="estado">El objeto <see cref="Estado"/> que contiene la información del estado a insertar.</param>
        public void AgregarEstado(Estado estado)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = "INSERT INTO Estados (nombre) VALUES (@nombre)";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", estado.Nombre);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios de la base de datos.
        /// </summary>
        /// <returns>Un objeto <see cref="DataTable"/> que contiene los usuarios.</returns>
        public DataTable ObtenerUsuarios()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = "SELECT * FROM Usuarios";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Agrega un nuevo usuario a la base de datos.
        /// </summary>
        /// <param name="usuario">El objeto <see cref="Usuario"/> que contiene la información del usuario a insertar.</param>
        public void AgregarUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = "INSERT INTO Usuarios (nombre) VALUES (@nombre)";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

