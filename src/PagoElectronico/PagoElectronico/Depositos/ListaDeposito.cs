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
        public ListaDeposito(decimal id_deposito)
        {
            InitializeComponent();
            num_deposito = id_deposito;

            //CARGO EL DATAGRIDVIEW CON LOS DATOS DEL DEPOSITO
            Conexion con = new Conexion();
            string query = "SELECT * FROM LPP.DEPOSITOS WHERE num_deposito = " + num_deposito + " ";
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dt = dtDatos;
            dgvDepositos.DataSource = dtDatos;
            con.cnn.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

       


       
    }
}
