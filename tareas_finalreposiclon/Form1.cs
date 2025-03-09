using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace tareas_final
{

    /// <summary>
    /// Representa el formulario de inicio de sesión de la aplicación.
    /// Permite validar las credenciales del usuario y redirigirlo al formulario principal (Form2).
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// Cadena de conexión a la base de datos obtenida desde el archivo de configuración.
        /// </summary>
        string connectionString = ConfigurationManager.ConnectionStrings["TareasDB"].ConnectionString;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Form1"/>.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Valida las credenciales del usuario comparándolas con los registros en la base de datos.
        /// </summary>
        /// <param name="usuario">El nombre de usuario ingresado.</param>
        /// <param name="contrasena">La contraseña ingresada.</param>
        /// <returns>
        /// <c>true</c> si las credenciales son válidas; de lo contrario, <c>false</c>.
        /// </returns>
        private bool ValidarUsuario(string usuario, string contrasena)
        {
            bool esValido = false;
                        
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string consulta = "SELECT COUNT(*) FROM Usuarios WHERE nombreUsuario = @Usuario AND contrasena = @Contrasena";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                    try
                    {
                        conexion.Open();
                        int count = (int)cmd.ExecuteScalar(); 
                        esValido = count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return esValido;
        }

        /// <summary>
        /// Maneja el evento click del botón de ingreso.
        /// Valida las credenciales del usuario y, de ser correctas, abre el formulario principal (Form2) y oculta este formulario.
        /// </summary>
        /// <param name="sender">El objeto que envía el evento.</param>
        /// <param name="e">Datos del evento.</param>
        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
          
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            
            if (ValidarUsuario(usuario,contraseña))
            {
                MessageBox.Show("Bienvenido, " + usuario, "Acceso permitido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir Form2
                Form2 formulario2 = new Form2();
                formulario2.Show();

                // Ocultar Form1
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Limpiar los campos
                txtUsuario.Clear();
                txtContraseña.Clear();
                txtUsuario.Focus();
            }
        }

        
    }
}


