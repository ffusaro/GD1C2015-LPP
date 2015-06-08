using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using PagoElectronico;
using Helper;
using readConfiguracion;

namespace PagoElectronico
{
    public partial class Depositos : Form
    {
        string usuario;
        public DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
        
        public Depositos(string user)
        {
            InitializeComponent();
            usuario = user;
            grpDepositos.Enabled = false;
            btnNuevo.Enabled = true;
            btnSalir.Enabled = true;
            btnGrabar.Enabled = false;
            btnLimpiar.Enabled = false;

            //CARGA DE NUMEROS DE CUENTA
            
            Conexion con = new Conexion();
            string query = "SELECT DISTINCT C.num_cuenta" +
                            "FROM LPP.CLIENTES U JOIN LPP.CUENTAS C ON C.id_cliente = U.id_cliente AND U.username = '" + usuario + "' " +
                                                "JOIN LPP.ESTADOS_CUENTA E ON E.id_estadocuenta = C.id_estado" +
                            "WHERE E.descripcion ='Habilitada'";

            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();

            while (lector.Read())
            {
                cmbNroCuenta.Items.Add(lector.GetString(0));
            }

            con.cnn.Close();

            //CARGA DE TIPOS DE MONEDAS

            string query2 = "SELECT DISTINCT M.descripcion FROM LPP.MONEDAS M ";
            con.cnn.Open();
            SqlCommand command2 = new SqlCommand(query2, con.cnn);
            SqlDataReader lector2 = command2.ExecuteReader();
            while (lector2.Read())
            {
                cmbNroCuenta.Items.Add(lector2.GetString(0));
            }
            con.cnn.Close();

            //CARGA DE TARJETAS DE CREDTIOS ASOCIADAS
            string query3 = "SELECT DISTINCT C.num_tarjeta" +
                            "FROM LPP.CLIENTES C JOIN LPP.TARJETAS T ON C.id_cliente = C.id_cliente AND U.username = '" + usuario + "' " +
                            "WHERE T.fecha_vencimiento > '"+fechaConfiguracion+"'";

            con.cnn.Open();
            SqlCommand command3 = new SqlCommand(query3, con.cnn);
            SqlDataReader lector3 = command3.ExecuteReader();
            while (lector3.Read())
            {
                cmbNroCuenta.Items.Add(lector3.GetString(0));
            }
            con.cnn.Close();
       }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpDepositos.Enabled = true;
            btnLimpiar.Enabled = true;
            btnGrabar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbMoneda.SelectedItem = null;
            cmbNroCuenta.SelectedItem = null;
            cmbTarjeta.SelectedItem = null;
            txtImporte.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (cmbNroCuenta.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un Número de Cuenta, por favor");
                return;
            }
            if (cmbMoneda.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un tipo de moneda, por favor");
                return;
            }
            if (cmbTarjeta.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un Número de Tarjeta de Crédito, por favor");
                return;
            }
            if (txtImporte.Text == "")
            {
                MessageBox.Show("Ingrese un Importe, por favor");
                return;
            }
            int temp;
            try
            {
                if (txtImporte.Text != "")
                temp = Convert.ToInt32(txtImporte.Text);

            }
            catch (Exception h)
            {
                MessageBox.Show("Importe solo puede contener números");
                return;
            }

            Conexion con = new Conexion();
            //CONSIGO ID DE MONEDA
            string query = "SELECT M.id_moneda FROM LPP.MONEDAS WHERE M.descripcion = '"+cmbMoneda.Text+"'";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            int id_moneda = lector.GetInt32(0);
            con.cnn.Close();

            //CONSIGO ID DE EMISOR TARJETA (?)
            string query1 = "SELECT T.id_emisor FROM LPP.TARJETAS WHERE T.num_tarjeta = " + Convert.ToInt32(cmbTarjeta.Text) + "";
            con.cnn.Open();
            SqlCommand command1 = new SqlCommand(query1, con.cnn);
            SqlDataReader lector1 = command1.ExecuteReader();
            int id_emisor = lector.GetInt32(0);
            con.cnn.Close();

            //INSERTO DATOS EN DEPOSITOS
            string query4 = "INSERT INTO LPP.DEPOSITOS (num_cuenta,importe,id_moneda,num_tarjeta,id_emisor,fecha_deposito) VALUES (" + Convert.ToInt32(cmbNroCuenta.Text) + ", "+Convert.ToInt32(txtImporte.Text)+", "+id_moneda+", "+Convert.ToInt32(cmbTarjeta.Text)+","+id_emisor+", '" + fechaConfiguracion + "' )";
            con.cnn.Open();
            SqlCommand command4 = new SqlCommand(query4, con.cnn);
            command4.ExecuteNonQuery();
            con.cnn.Close();

            //FALTA LO DEL COMPROBANTE DE DEPOSITO QUE NO ENTENDI BIEN

        }
    }
}
