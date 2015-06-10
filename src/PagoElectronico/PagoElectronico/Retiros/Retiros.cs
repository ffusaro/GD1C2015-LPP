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

namespace PagoElectronico.Retiros
{
    public partial class RetiroDeEfectivo : Form
    {
        string usuario;
        public DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
        int bandera;

        public RetiroDeEfectivo(string user)
        {
            InitializeComponent();
            usuario = user;
            grpRetiros.Enabled = false;
            btnNuevo.Enabled = true;
            btnSalir.Enabled = true;
            btnGrabar.Enabled = false;
            btnLimpiar.Enabled = false;

            //CARGA DE NUMEROS DE CUENTA

            Conexion con = new Conexion();
            string query = "SELECT DISTINCT C.num_cuenta" +
                            "FROM LPP.CLIENTES U JOIN LPP.CUENTAS C ON C.id_cliente = U.id_cliente AND U.username = '" + usuario + "' " +
                                                "JOIN LPP.ESTADOS_CUENTA E ON E.id_estadocuenta = C.id_estado" +
                            "WHERE E.descripcion ='Habilitada' AND C.saldo > 0";

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
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpRetiros.Enabled = true;
            btnLimpiar.Enabled = true;
            btnGrabar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbNroCuenta.SelectedItem = null;
            cmbMoneda.SelectedItem = null;
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
            else
            {
                if (cmbMoneda.Text != "Dólares")
                {
                    MessageBox.Show("El importe debe estar expresado en Dolares");
                    return;
                }
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
            //CORROBORO SALDO
            string query = "SELECT C.saldo FROM LPP.CUENTAS WHERE C.num_cuenta = "+Convert.ToInt32(cmbNroCuenta.Text)+" AND C.saldo >= "+Convert.ToInt32(txtImporte.Text)+"";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            if (lector.Read())
            {
                Cheque form_cheque = new Cheque(Convert.ToInt32(cmbNroCuenta.SelectedItem), usuario);
                form_cheque.Show();

            }
            else
            {
                MessageBox.Show("La cuenta tiene saldo insuficiente");
                return; 
            }
            con.cnn.Close();


        }
        public void GuardarDatos(string banco)
        {
            Conexion con = new Conexion();

            //CONSIGO ID DE MONEDA
            string query0 = "SELECT M.id_moneda FROM LPP.MONEDAS WHERE M.descripcion = '" + cmbMoneda.Text + "'";
            con.cnn.Open();
            SqlCommand command0 = new SqlCommand(query0, con.cnn);
            SqlDataReader lector0 = command0.ExecuteReader();
            int id_moneda = lector0.GetInt32(0);
            con.cnn.Close();

            //INSERTO RETIRO
            string query = "INSERT INTO LPP.RETIROS (num_cuenta,importe,fecha,id_moneda) VALUES ("+Convert.ToInt32(cmbNroCuenta.Text)+", "+Convert.ToInt32(txtImporte.Text)+", '"+fechaConfiguracion+"', "+id_moneda+")";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            con.cnn.Close();

            //OBTENGO ID DE RETIRO
            string query3 = "SELECT TOP 1 id_retiro FROM LPP.RETIROS WHERE num_cuenta = " + Convert.ToInt32(cmbNroCuenta.Text)+" ORDER BY fecha DESC";
            con.cnn.Open();
            SqlCommand command3 = new SqlCommand(query3, con.cnn);
            SqlDataReader lector3 = command3.ExecuteReader();
            int id_retiro = lector3.GetInt32(0);
            con.cnn.Close();

            //OBTENGO ID DE BANCO
            string query5 = "SELECT id_banco FROM LPP.BANCOS WHERE nombre = '" + banco + "'";
            con.cnn.Open();
            SqlCommand command5 = new SqlCommand(query5, con.cnn);
            SqlDataReader lector5 = command5.ExecuteReader();
            int id_banco = lector5.GetInt32(0);
            con.cnn.Close();

            //OBTENGO CLIENTE_RECPEPTOR
            string query4 = "SELECT CONCAT(nombre,apellido) FROM LPP.CLIENTES WHERE username = '"+usuario+"' ";
            con.cnn.Open();
            SqlCommand command4 = new SqlCommand(query4, con.cnn);
            SqlDataReader lector4 = command4.ExecuteReader();
            string cliente = lector4.GetString(0);
            con.cnn.Close();

            //INSERTO EN CHEQUE
            string query2 = "INSERT INTO LPP.CHEQUES (id_retiro,importe,fecha,id_banco,cliente_receptor) VALUES ("+id_retiro+", "+Convert.ToInt32(txtImporte.Text)+", '"+fechaConfiguracion+"', "+id_banco+", '"+cliente+"')";
            con.cnn.Open();
            SqlCommand command2 = new SqlCommand(query2, con.cnn);
            SqlDataReader lector2 = command2.ExecuteReader();
            con.cnn.Close();

            //OBTENGO ID CHEQUE Y LO MUESTRO
            DialogResult dialogResult = MessageBox.Show("Su retiro se realizo correctamente. ¿Desea ver el comprobante?", "Retiro de Efectivo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                
                ListaRetiros lr = new ListaRetiros(Convert.ToInt32(cmbNroCuenta.SelectedItem));
                lr.Show();
                this.Close();
            }
            else
            {
                this.Close();
            }
        
        }
    }
}
