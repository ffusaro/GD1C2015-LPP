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
using Helper;
using readConfiguracion;

namespace PagoElectronico.Depositos
{
    public partial class Depositos : Form
    {
        string usuario;
        public DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
        private decimal id_emisor;
        private decimal id_moneda;
        private decimal num_deposito;

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
            string query = "SELECT DISTINCT C.num_cuenta FROM LPP.CUENTAS C" +
                           " JOIN LPP.CLIENTES CL ON CL.id_cliente = C.id_cliente "+
                           " JOIN LPP.ESTADOS_CUENTA E ON E.id_estadocuenta = C.id_estado " +
                           " WHERE E.id_estadocuenta = 1 AND CL.username = '" + usuario  + "'";

            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();

            while (lector.Read())
            {
                cmbNroCuenta.Items.Add(lector.GetDecimal(0));
            }

            con.cnn.Close();

            //CARGA DE TIPOS DE MONEDAS
            string query2 = "SELECT DISTINCT M.descripcion FROM LPP.MONEDAS M ";
            con.cnn.Open();
            SqlCommand command2 = new SqlCommand(query2, con.cnn);
            SqlDataReader lector2 = command2.ExecuteReader();
            while (lector2.Read())
            {
                cmbMoneda.Items.Add(lector2.GetString(0));
            }
            con.cnn.Close();

            //CARGA DE TARJETAS DE CREDTIOS ASOCIADAS
            string query3 = "SELECT DISTINCT T.num_tarjeta FROM LPP.TARJETAS T "
                            +" JOIN LPP.CLIENTES C ON C.id_cliente = T.id_cliente"
                            +" WHERE C.username = '"+usuario+"'"
                            +" AND T.fecha_vencimiento > convert(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103)";

            con.cnn.Open();
            SqlCommand command3 = new SqlCommand(query3, con.cnn);
            SqlDataReader lector3 = command3.ExecuteReader();
            while (lector3.Read())
            {
                cmbTarjeta.Items.Add(lector3.GetString(0));
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
                MessageBox.Show("Importe solo puede contener números",h.ToString());
                return;
            }

            Conexion con = new Conexion();
            //CONSIGO ID DE EMISOR TARJETA (?)
            string query1 = "SELECT id_emisor FROM LPP.TARJETAS WHERE num_tarjeta = '" + cmbTarjeta.Text + "'";
            con.cnn.Open();
            SqlCommand command1 = new SqlCommand(query1, con.cnn);
            SqlDataReader lector1 = command1.ExecuteReader();
            while (lector1.Read())
            {
                id_emisor = lector1.GetDecimal(0);
            }
            con.cnn.Close();

            //CONSIGO ID DE MONEDA
            string query = "SELECT id_moneda FROM LPP.MONEDAS WHERE descripcion = '"+cmbMoneda.Text+"'";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            while (lector.Read())
            {
                id_moneda = lector.GetDecimal(0);
            }
            con.cnn.Close();

           

            //INSERTO DATOS EN DEPOSITOS
            string query4 = "INSERT INTO LPP.DEPOSITOS (num_cuenta, importe, id_moneda, num_tarjeta, id_emisor, fecha_deposito)"
                            +" VALUES (" + Convert.ToDecimal(cmbNroCuenta.Text) + ", "+Convert.ToDecimal(txtImporte.Text) +", "+ id_moneda +", '"+ cmbTarjeta.Text +"', "+ id_emisor +", CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103))";
            con.cnn.Open();
            SqlCommand command4 = new SqlCommand(query4, con.cnn);
            command4.ExecuteNonQuery();
            con.cnn.Close();

            //SUMO IMPORTE EN CUENTA
            string query5 = "UPDATE LPP.CUENTAS SET saldo = saldo + "+Convert.ToDecimal(txtImporte.Text)+" WHERE num_cuenta = "+Convert.ToDecimal(cmbNroCuenta.SelectedItem)+"";
            con.cnn.Open();
            SqlCommand command5 = new SqlCommand(query5, con.cnn);
            command5.ExecuteNonQuery();
            con.cnn.Close();

            

             DialogResult dialogResult = MessageBox.Show("Su deposito se realizo correctamente. ¿Desea ver el comprobante?", "Despositos", MessageBoxButtons.YesNo);
             if (dialogResult == DialogResult.Yes)
             {
                 //Obtengo el numero del deposito que acabo de hacer
                string query6 = "SELECT num_deposito FROM LPP.DEPOSITOS WHERE "
                             +" num_cuenta = " + Convert.ToDecimal(cmbNroCuenta.Text) 
                             +" AND importe = "+Convert.ToDecimal(txtImporte.Text) 
                             +" AND id_moneda = "+ id_moneda 
                             +" AND num_tarjeta = '"+ cmbTarjeta.Text +"'"
                             +" AND id_emisor = "+ id_emisor 
                             +" AND fecha_deposito = CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103))";
                con.cnn.Open();
                SqlCommand command6 = new SqlCommand(query6, con.cnn);
                SqlDataReader lector6 = command.ExecuteReader();
                while (lector6.Read())
                {
                     num_deposito = lector6.GetDecimal(0);
                }
                
                 ListaDeposito ld = new ListaDeposito(num_deposito);
                 ld.Show();
                 this.Close();
             }
             else
             {
                 this.Close(); 
             }
           
           

        }
    }
}
