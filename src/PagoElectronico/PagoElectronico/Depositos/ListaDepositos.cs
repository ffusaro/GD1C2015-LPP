using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico;
using Helper;
using readConfiguracion;
using System.Data.SqlClient;

namespace PagoElectronico
{
    public partial class ListaDepositos : Form
    {
        public int num_cuenta;
        public DataTable dt;
        public ListaDepositos(int cuenta)
        {
            InitializeComponent();
            num_cuenta = cuenta;

            //CARGO EL DATAGRIDVIEW CON LOS DATOS DEL DEPOSITO
            Conexion con = new Conexion();
            string query = "SELECT * FROM LPP.DEPOSITOS WHERE num_cuenta = "+num_cuenta+" ";
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
