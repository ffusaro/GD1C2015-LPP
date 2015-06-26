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

namespace PagoElectronico.Retiros
{
    public partial class ListaRetiros : Form
    {
        private decimal id_retiro;
        private DataTable dtRetiro;
        private DataTable dtCheque;

        public ListaRetiros(decimal retiro)
        {
            InitializeComponent();
            id_retiro = retiro;

            dgvRetiro.AllowUserToAddRows = false;
            dgvRetiro.AllowUserToDeleteRows = false;
            dgvRetiro.ReadOnly = true;

            dgvCheque.AllowUserToAddRows = false;
            dgvCheque.AllowUserToDeleteRows = false;
            dgvCheque.ReadOnly = true;

            Conexion con = new Conexion();
            
            string query = "SELECT * FROM LPP.RETIROS WHERE id_retiro = " + id_retiro + " ";                          
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dtRetiro = dtDatos;
            dgvRetiro.DataSource = dtDatos;
            con.cnn.Close();

            string query2 = "SELECT * FROM LPP.CHEQUES WHERE id_retiro = " + id_retiro + " ";
            DataTable dtCh = new DataTable();
            SqlDataAdapter dch = new SqlDataAdapter(query2, con.cnn);
            dch.Fill(dtCh);
            dtCheque = dtCh;
            dgvCheque.DataSource = dtCh;
            con.cnn.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
