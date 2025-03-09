using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;


namespace tareas_final
{

    /// <summary>
    /// Representa el formulario principal de la aplicación, el cual permite gestionar las tareas.
    /// En este formulario se pueden visualizar, agregar, editar y eliminar tareas.
    /// </summary>
    public partial class Form2 : Form
    {
        /// <summary>
        /// Almacena el ID de la tarea seleccionada para edición.
        /// </summary>
        private int idTareaSeleccionada = -1;

        /// <summary>
        /// Instancia para acceder a la base de datos.
        /// </summary>
        private readonly DataAccess dataAccess;


        /// <summary>
        /// Inicializa una nueva instancia del formulario <see cref="Form2"/>.
        /// Configura la conexión a la base de datos y carga los datos iniciales.
        /// </summary>
        public Form2()
        {
            InitializeComponent();

            
            string connectionString = ConfigurationManager.ConnectionStrings["TareasDB"].ConnectionString;

            dataAccess = new DataAccess(connectionString);

            ConfigurarDataGridView();
            CargarCategorias();
            MostrarTareas();
            LimpiarCampos();
        }

        /// <summary>
        /// Configura el DataGridView, definiendo las columnas y agregando botones para editar y borrar.
        /// </summary>
        private void ConfigurarDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Name = "Tarea";
            dataGridView1.Columns[2].Name = "Descripción";
            dataGridView1.Columns[3].Name = "Categoría";

            var btnEditar = new DataGridViewButtonColumn { Name = "Editar", Text = "Editar", UseColumnTextForButtonValue = true };
            var btnBorrar = new DataGridViewButtonColumn { Name = "Borrar", Text = "Borrar", UseColumnTextForButtonValue = true };

            dataGridView1.Columns.Add(btnEditar);
            dataGridView1.Columns.Add(btnBorrar);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }
        /// <summary>
        /// Carga las categorías desde la base de datos y las asigna al ComboBox.
        /// Si existen registros, se selecciona el primer elemento.
        /// </summary>
        private void CargarCategorias()
        {
            DataTable dt = dataAccess.ObtenerCategorias();
            cmbCategoria.DataSource = dt;
            cmbCategoria.DisplayMember = "nombre";
            cmbCategoria.ValueMember = "id";

            
            if (cmbCategoria.Items.Count > 0)
            {
                cmbCategoria.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No se encontraron categorías en la base de datos.");
            }

            
        }


        /// <summary>
        /// Carga y muestra las tareas en el DataGridView.
        /// </summary>
        private void MostrarTareas()
        {
            dataGridView1.Rows.Clear();
            DataTable tareas = dataAccess.ObtenerTareas();

            foreach (DataRow row in tareas.Rows)
            {
                dataGridView1.Rows.Add(row["id"], row["titulo"], row["descripcion"], row["categoria"], "Editar", "Borrar");
            }
        }


        /// <summary>
        /// Maneja el evento click del botón Agregar.
        /// Valida los campos, crea una nueva tarea y la inserta en la base de datos, 
        /// actualizando el DataGridView posteriormente.
        /// </summary>
        /// <param name="sender">El objeto que envía el evento.</param>
        /// <param name="e">Datos del evento.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTarea.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Complete todos los campos antes de agregar la tarea.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataRowView row = cmbCategoria.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Seleccione una categoría válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idCategoria = Convert.ToInt32(row["id"]);
            string nombreCategoria = row["nombre"].ToString();
            Categoria categoriaSeleccionada = new Categoria(idCategoria, nombreCategoria);

            Estado estadoPendiente = new Estado(1, "Pendiente");
            Usuario usuarioSinAsignar = new Usuario(0, "Sin asignar");

            Tarea nuevaTarea = new Tarea(0, txtTarea.Text, txtDescripcion.Text, categoriaSeleccionada, estadoPendiente, usuarioSinAsignar);
            int nuevoId = dataAccess.AgregarTarea(nuevaTarea);
            nuevaTarea.Id = nuevoId;

            dataGridView1.Rows.Add(nuevaTarea.Id, nuevaTarea.Titulo, nuevaTarea.Descripcion, nuevaTarea.Categoria.Nombre, "Editar", "Borrar");
            LimpiarCampos();
        }



        /// <summary>
        /// Maneja el evento CellClick del DataGridView.
        /// Permite editar o eliminar una tarea según la columna seleccionada.
        /// </summary>
        /// <param name="sender">El objeto que envía el evento.</param>
        /// <param name="e">Datos del evento, que incluyen la fila y la columna clickeada.</param>
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            idTareaSeleccionada = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Editar")
            {
                txtTarea.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDescripcion.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                cmbCategoria.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                btnAgregar.Visible = false;
                btnGuardarCambios.Visible = true;
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Borrar")
            {
                if (MessageBox.Show("¿Seguro que deseas eliminar esta tarea?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dataAccess.EliminarTarea(idTareaSeleccionada);
                    MostrarTareas();
                }
            }
        }


        /// <summary>
        /// Maneja el evento click del botón Guardar Cambios.
        /// Actualiza los datos de la tarea seleccionada en la base de datos.
        /// </summary>
        /// <param name="sender">El objeto que envía el evento.</param>
        /// <param name="e">Datos del evento.</param>
        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (idTareaSeleccionada == -1)
            {
                MessageBox.Show("Seleccione una tarea para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int estadoId = 0; // Declaración inicial

            // Intentamos obtener el valor seleccionado del ComboBox
            if (int.TryParse(cmbCategoria.SelectedValue?.ToString(), out int estId))
            {
                estadoId = estId;
            }

            // Usamos estadoId después de asegurarnos de que tiene un valor válido
            Console.WriteLine(estadoId);

            int categoriaId = Convert.ToInt32(cmbCategoria.SelectedValue);
            dataAccess.ActualizarTarea(idTareaSeleccionada, txtTarea.Text, txtDescripcion.Text, categoriaId,estadoId);
            MostrarTareas();
            LimpiarCampos();
            btnGuardarCambios.Visible = false;
            btnAgregar.Visible = true;
            idTareaSeleccionada = -1;        }


        /// <summary>
        /// Limpia los campos de entrada y resetea el estado del formulario.
        /// </summary>
        private void LimpiarCampos()
        {
            txtTarea.Clear();
            txtDescripcion.Clear();
            cmbCategoria.SelectedIndex = -1;
            idTareaSeleccionada = -1;
            btnGuardarCambios.Visible = false;
            btnAgregar.Visible = true;
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


