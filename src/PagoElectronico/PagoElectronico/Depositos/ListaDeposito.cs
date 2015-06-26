using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Helper;
using readConfiguracion;
using System.Data.SqlClient;

namespace PagoElectronico.Depositos
{
    public partial class ListaDeposito : Form
    {
        public decimal num_deposito;
        public DataTable dt;
        private string ultimosCuatro;
        public ListaDeposito(decimal id_deposito)
        {
            InitializeComponent();
            num_deposito = id_deposito;

            dgvDepositos.AllowUserToAddRows = false;
            dgvDepositos.AllowUserToDeleteRows = false;
            dgvDepositos.ReadOnly = true;

            //CARGO EL DATAGRIDVIEW CON LOS DATOS DEL DEPOSITO
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "SELECT * FROM LPP.DEPOSITOS WHERE num_deposito = " + num_deposito + " ";
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dt = dtDatos;
            dgvDepositos.DataSource = dtDatos;

            //CAMBIO COLUMNA DE NUM_TARJETA
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();

            foreach (DataGridViewRow row in dgvDepositos.Rows)
            {
                ultimosCuatro = lector.GetString(4);
                row.Cells["num_tarjeta"].Value = "XXXX-XXXX-XXXX-" + ultimosCuatro.Remove(0, 12);
            }

            con.cnn.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

       


       
    }
}
