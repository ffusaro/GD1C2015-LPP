using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Transferencias
{
    public partial class BuscarCuentas : Form
    {
        private DataTable dt;
        private int nroDoc;
        private string tipoDoc;
        private Transferencias tr;
        public string usuario;
        public decimal num_cuenta_origen;
        public decimal importe;


        public BuscarCuentas(string user)
        {
            InitializeComponent();
            usuario = user;
            Conexion con1 = new Conexion();
            con1.cnn.Open();
            //Pregunto todos los TipoDoc de la DB
            string query = "SELECT tipo_descr FROM LPP.TIPO_DOCS";
            SqlCommand command = new SqlCommand(query, con1.cnn);
            SqlDataReader lector = command.ExecuteReader();
            while (lector.Read())
            {
                // Los cargo en la lista
                cbTipo.Items.Add(lector.GetString(0));
            }

            con1.cnn.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtApellido.Text = "";
            txtCuenta.Text = "";
            cbTipo.SelectedItem = false;
            txtNumeroID.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            Conexion con = new Conexion();

            //HACER CONSULTA
            string query = " SELECT T.num_cuenta,C.nombre + C.apellido, C.id_tipo_doc, C.num_doc"+
                             " FROM LPP.CLIENTES C JOIN LPP.CUENTAS T ON C.id_cliente = T.id_cliente"+
                             " JOIN LPP.ESTADOS_CUENTA e ON T.id_estado = e.id_estadocuenta"+
                             " WHERE (e.id_estadocuenta = 1 OR e.id_estadocuenta = 4) ";

            // Cargo todos los Clientes en el DATAGRIDVIEW

            if (txtCuenta.Text != "")
            {
                query += " AND T.num_cuenta = " + Convert.ToDecimal(txtCuenta.Text)+" ";
            }
            if (txtApellido.Text != "")
            {
                query += " AND C.apellido LIKE '%" + txtApellido.Text + "%'";
            }
            if (cbTipo.SelectedItem != null)
            {
                query += " AND id_tipo_doc = '" + getIdTipo() + "'";
            }
            if (txtNumeroID.Text != "")
            {
                query += " AND num_doc = " + Convert.ToDecimal(txtNumeroID.Text) + "";
            }
            con.cnn.Open();
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dt = dtDatos;
            dgvCuentas.DataSource = dtDatos;
            con.cnn.Close();
        }

        private decimal getIdTipo()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //OBTENGO ID del Tipo de Documento
            string query = "SELECT tipo_cod FROM LPP.TIPO_DOCS WHERE tipo_descr = '"+cbTipo.Text+"'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            decimal id_tipo = lector.GetDecimal(0);
            con.cnn.Close();
            return id_tipo;
        }

        private void dgvCuentas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int id;
            int indice = e.RowIndex;
            decimal num_cuenta = Convert.ToDecimal(dgvCuentas.Rows[indice].Cells["num_cuenta"].Value);
            tr = new Transferencias(usuario,num_cuenta);
            tr.txtImporte.Text = importe.ToString();
            tr.cmbNroCuenta.Text = num_cuenta_origen.ToString();
            tr.Show();
            this.Close();
        }
    }
}
