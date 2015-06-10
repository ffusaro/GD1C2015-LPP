using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using readConfiguracion;
using System.Data.SqlClient;

namespace PagoElectronico.Listados

{
    public partial class Listados : Form
    {
        public MenuPrincipal mp = new MenuPrincipal();
        public Listados()
        {
            InitializeComponent();

            //CARGANDO DATOS INICIALES
            cmbListado.Items.Add("");
            cmbListado.Items.Add("Clientes con cuentas inhabilitadas por no pagar costos de transaccion");
            cmbListado.Items.Add("Cliente con mayor cantidad de comisiones facturadas en sus cuentas");
            cmbListado.Items.Add("Clientes con mayor cantidad de transacciones realizadas entre cuentas propias");
            cmbListado.Items.Add("Paises con mayor cantidad de movimientos");
            cmbListado.Items.Add("Total facturado para los distintos tipos de cuentas");

            cmbAño.Items.Add("");
            int i = 0;
            while (i < 50)
            {
                cmbAño.Items.Add((Convert.ToInt32(Configuracion.fechaSystem().Substring(0, 4)) - i + 5).ToString());
                i++;
            }
            cmbPeriodo.Items.Add("");
            cmbPeriodo.Items.Add("Enero-Febrero-Marzo");
            cmbPeriodo.Items.Add("Abril-Mayo-Junio");
            cmbPeriodo.Items.Add("Julio-Agosto-Septiembre");
            cmbPeriodo.Items.Add("Octubre-Nobiembre-Diciembre");
            
            //FIN CARGANDO DATOS INICIALES

            cmbListado.Enabled = false;
            cmbPeriodo.Enabled = false;
            btnLimpiar.Enabled = true;
            btnBuscar.Enabled = false;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            //dgvDatos.Rows.Clear();

            if (cmbAño.Text == "")
            {
                MessageBox.Show("Elija un año por favor");
                return;
            }

            if (cmbPeriodo.Text== "")
            {
                MessageBox.Show("Elija un Periodo por favor");
                return;
            }

            if (cmbListado.Text == "")
            {
                MessageBox.Show("Elija un Listado por favor");
                return;
            }

            Conexion con = new Conexion();
            int inicio;
            int fin;
            int anio = Convert.ToInt32(cmbAño.SelectedItem);

            if (cmbPeriodo.Text == "Enero-Febrero-Marzo")
            {
                inicio = 1;
                fin = 3;
            }
            else
            {
                if (cmbPeriodo.Text == "Abril-Mayo-Junio")
                {
                    inicio = 4;
                    fin = 6;
                }
                else
                {
                    if (cmbPeriodo.Text == "Julio-Agosto-Septiembre")
                    {
                        inicio = 7;
                        fin = 9;
                    }
                    else
                    {
                        inicio = 10;
                        fin = 12;
                    }
                }
            }
            if (cmbListado.Text == "Clientes con cuentas inhabilitadas por no pagar costos de transaccion")
            {
                string query = "PRC_estadistico_cuentas_inhabilitadas";

                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@desde", inicio);
                command.Parameters.AddWithValue("@hasta", fin);
                command.Parameters.AddWithValue("@anio", anio);
                SqlDataReader lector = command.ExecuteReader();

                if (!lector.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }
                con.cnn.Close();
                //---------
                con.cnn.Open();
                DataTable dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
                da.Fill(dtDatos);

                dgvDatos.DataSource = dtDatos;
                con.cnn.Close();
            }

            if (cmbListado.Text == "Cliente con mayor cantidad de comisiones facturadas en sus cuentas")
            {
                string query = "PRC_estadistico_comisiones_facturadas";

                 con.cnn.Open();
                 SqlCommand command = new SqlCommand(query, con.cnn);
                 command.CommandType = CommandType.StoredProcedure;
                 command.Parameters.AddWithValue("@desde", inicio);
                 command.Parameters.AddWithValue("@hasta", fin);
                 command.Parameters.AddWithValue("@anio", anio);
                 SqlDataReader lector = command.ExecuteReader();

                if (!lector.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }
                con.cnn.Close();

                con.cnn.Open();
                DataTable dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
                da.Fill(dtDatos);

                dgvDatos.DataSource = dtDatos;
                con.cnn.Close();


            }
            if (cmbListado.Text == "Clientes con mayor cantidad de transacciones realizadas entre cuentas propias")
            {
                string query = "PRC_estadistico_transacciones_cuentas_propias";

                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@desde", inicio);
                command.Parameters.AddWithValue("@hasta", fin);
                command.Parameters.AddWithValue("@anio", anio);
                SqlDataReader lector = command.ExecuteReader();

                if (!lector.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }
                con.cnn.Close();

                con.cnn.Open();
                DataTable dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
                da.Fill(dtDatos);

                dgvDatos.DataSource = dtDatos;
                con.cnn.Close();
            }

            if (cmbListado.Text == "Paises con mayor cantidad de movimientos")
            {
                
                string query2 = "PRC_estadistico_pais_mas_movimientos";

                con.cnn.Open();
                SqlCommand command = new SqlCommand(query2, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@desde", inicio);
                command.Parameters.AddWithValue("@hasta", fin);
                command.Parameters.AddWithValue("@anio", anio);
                SqlDataReader lector2 = command.ExecuteReader();

                if (!lector2.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }
                con.cnn.Close();

                con.cnn.Open();
                DataTable dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query2, con.cnn);
                da.Fill(dtDatos);

                dgvDatos.DataSource = dtDatos;
                con.cnn.Close();
            }

            if (cmbListado.Text == "Total facturado para los distintos tipos de cuentas")
            {
                //int anio = Convert.ToInt32(cmbAño.Text);
                string query2 = "PRC_estadistico_facturado_tipo_cuentas";

                con.cnn.Open();
                SqlCommand command = new SqlCommand(query2, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@desde", inicio);
                command.Parameters.AddWithValue("@hasta", fin);
                command.Parameters.AddWithValue("@anio", anio);
                SqlDataReader lector2 = command.ExecuteReader();

                if (!lector2.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }
                con.cnn.Close();

                con.cnn.Open();
                DataTable dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query2, con.cnn);
                da.Fill(dtDatos);

                dgvDatos.DataSource = dtDatos;
                con.cnn.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            
            cmbAño.Text = "";
            cmbListado.Text = "";
            cmbPeriodo.Text = "";

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            mp.Show();
            this.Close();
        }

        private void cmbAño_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbAño.Text != "")
            {
                cmbPeriodo.Enabled = true;
            }
            else
            {
                cmbPeriodo.Text = "";
                cmbPeriodo.Enabled = false;
            }
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPeriodo.Text != "")
            {
                cmbListado.Enabled = true;
            }
            else
            {
                cmbListado.Text = "";
                cmbListado.Enabled = false;
            }
        }

        private void cmbListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbListado.Text != "")
            {
                btnBuscar.Enabled = true;
            }
          
        }

        private void Listados_Load(object sender, EventArgs e)
        {

        }

        private Button btnSalir;
        private DataGridView dgvDatos;
        private GroupBox groupBox1;
        private Button btnLimpiar;
        private Button btnBuscar;
        private ComboBox cmbListado;
        private ComboBox cmbPeriodo;
        private ComboBox cmbAño;
        private Label lblListados;
        private Label lblPeriodo;
        private Label lblaño;

        private void InitializeComponent()
        {
            this.btnSalir = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cmbListado = new System.Windows.Forms.ComboBox();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
            this.cmbAño = new System.Windows.Forms.ComboBox();
            this.lblListados = new System.Windows.Forms.Label();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.lblaño = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(272, 390);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(111, 23);
            this.btnSalir.TabIndex = 11;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // dgvDatos
            // 
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(13, 184);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.Size = new System.Drawing.Size(629, 188);
            this.dgvDatos.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.cmbListado);
            this.groupBox1.Controls.Add(this.cmbPeriodo);
            this.groupBox1.Controls.Add(this.cmbAño);
            this.groupBox1.Controls.Add(this.lblListados);
            this.groupBox1.Controls.Add(this.lblPeriodo);
            this.groupBox1.Controls.Add(this.lblaño);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(629, 157);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de Búsqueda";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(349, 116);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(111, 23);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(168, 116);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(111, 23);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // cmbListado
            // 
            this.cmbListado.FormattingEnabled = true;
            this.cmbListado.Location = new System.Drawing.Point(86, 75);
            this.cmbListado.Name = "cmbListado";
            this.cmbListado.Size = new System.Drawing.Size(296, 21);
            this.cmbListado.TabIndex = 5;
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.FormattingEnabled = true;
            this.cmbPeriodo.Location = new System.Drawing.Point(379, 31);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(215, 21);
            this.cmbPeriodo.TabIndex = 4;
            // 
            // cmbAño
            // 
            this.cmbAño.FormattingEnabled = true;
            this.cmbAño.Location = new System.Drawing.Point(86, 31);
            this.cmbAño.Name = "cmbAño";
            this.cmbAño.Size = new System.Drawing.Size(215, 21);
            this.cmbAño.TabIndex = 3;
            // 
            // lblListados
            // 
            this.lblListados.AutoSize = true;
            this.lblListados.Location = new System.Drawing.Point(30, 78);
            this.lblListados.Name = "lblListados";
            this.lblListados.Size = new System.Drawing.Size(46, 13);
            this.lblListados.TabIndex = 2;
            this.lblListados.Text = "Listados";
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Location = new System.Drawing.Point(318, 34);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(43, 13);
            this.lblPeriodo.TabIndex = 1;
            this.lblPeriodo.Text = "Periodo";
            // 
            // lblaño
            // 
            this.lblaño.AutoSize = true;
            this.lblaño.Location = new System.Drawing.Point(30, 34);
            this.lblaño.Name = "lblaño";
            this.lblaño.Size = new System.Drawing.Size(26, 13);
            this.lblaño.TabIndex = 0;
            this.lblaño.Text = "Año";
            // 
            // Listados
            // 
            this.ClientSize = new System.Drawing.Size(653, 427);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Listados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listados Estadisticos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

      

        

        
    }
}
