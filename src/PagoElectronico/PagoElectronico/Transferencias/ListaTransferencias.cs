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

namespace PagoElectronico.Transferencias
{
    public partial class ListaTransferencias : Form
    {
        private decimal id_transferencia;
        private DataTable dtTrans;

        public ListaTransferencias(decimal trans)
        {
            InitializeComponent();
            id_transferencia = trans;
            Conexion con = new Conexion();
            string query = "SELECT * FROM LPP.TRANSFERENCIAS WHERE id_transferencia = " + id_transferencia + " ";
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dtTrans = dtDatos;
            dgvTrans.DataSource = dtDatos;
            con.cnn.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
