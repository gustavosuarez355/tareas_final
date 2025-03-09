using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tareas_final
{
    /// <summary>
    /// Proporciona un método para obtener una conexión a la base de datos utilizando la cadena de conexión definida en el archivo de configuración.
    /// </summary>
    public class DatabaseHelper
    {
        /// <summary>
        /// Cadena de conexión obtenida desde el archivo de configuración (App.config).
        /// </summary>
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TareasDB"].ConnectionString;
        /// <summary>
        /// Crea y devuelve una nueva instancia de <see cref="SqlConnection"/> utilizando la cadena de conexión configurada.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="SqlConnection"/>.</returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
