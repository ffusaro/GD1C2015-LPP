using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using Helper;
using readConfiguracion;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class BuscarCuentas : Form
    {
        public MenuPrincipal mp = new MenuPrincipal();
        public BuscarCuentas()
        {
            InitializeComponent();

            /*Cargo datos iniciales */
            cmbListado.Items.Add("Ultimos 5 depositos");
            cmbListado.Items.Add("Ultimos 5 retiros");
            cmbListado.Items.Add("Ultimas 10 transferencias ");

            cmbListado.Enabled = false;
            btnLimpiar.Enabled = true;
            btnBuscar.Enabled = false;


        }

        private void ConsultaDeSaldos_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
            
            mp.Show();
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //dgvDatos.Rows.Clear();

            if (txtCuenta.Text == "")
            {
                MessageBox.Show("Ingrese una cuenta por favor");
                return;
            }

            if (cmbListado.Text == "")
            {
                MessageBox.Show("Elija un Listado por favor");
                return;
            }


            /* Verifico numero de cuenta */

            Conexion con = new Conexion();
            string query = "SELECT num_cuenta FROM LPP.CUENTAS WHERE num_cuenta = '" + txtCuenta.Text + "'";
            con.cnn.Open();
            SqlCommand command = new SqlCommand (query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();;
            if (!(lector.Read()))
            {
                MessageBox.Show("Numero de cuenta incorrecto");
            }
            con.cnn.Close();

            Conexion con1 = new Conexion();
            string query1 = "PRC_obtener_saldo_de_una_cuenta";

             con.cnn.Open();
             SqlCommand command1 = new SqlCommand(query1, con.cnn);
             command1.CommandType = CommandType.StoredProcedure;
             command1.Parameters.AddWithValue("@num_cuenta", Convert.ToInt32(txtCuenta.Text));
             SqlDataReader lector1 = command1.ExecuteReader();
             txtSaldo.Text = lector1.GetString(0);
             con.cnn.Close();


             if (cmbListado.Text == "Ultimos 5 depositos")
             {
                 
             }





                 
             if (cmbListado.Text == "Ultimos 5 retiros")
             {
                 
 
             }

             if (cmbListado.Text == "Ultimas 10 transferencias")
             {
                 

             }

        }

        private void cmbListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbListado.Text != "")
            {
                btnBuscar.Enabled = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = "";
            cmbListado.SelectedItem = null;
        }
    }
}
