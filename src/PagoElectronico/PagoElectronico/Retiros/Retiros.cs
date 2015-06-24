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
        public decimal num_cuenta;
        public decimal importe;
        public decimal id_moneda;

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
            string query = "SELECT DISTINCT C.num_cuenta" 
                           + " FROM LPP.CUENTAS C"
                           + " JOIN LPP.CLIENTES CL ON CL.id_cliente = C.id_cliente"
                           + " JOIN LPP.ESTADOS_CUENTA E ON E.id_estadocuenta = C.id_estado"
                           + " WHERE CL.username  = '"+ usuario +"' AND E.id_estadocuenta = 1 AND C.saldo > 0";

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
            string query = "SELECT saldo FROM LPP.CUENTAS WHERE num_cuenta = "+Convert.ToDecimal(cmbNroCuenta.Text)+" AND saldo >= "+Convert.ToDecimal(txtImporte.Text)+"";
            importe = Convert.ToDecimal(txtImporte.Text);
            id_moneda = this.getIdMoneda();
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            if (lector.Read())
            {
                Cheque form_cheque = new Cheque(Convert.ToDecimal(cmbNroCuenta.Text), usuario);
                form_cheque.importe = importe;
                form_cheque.id_moneda = id_moneda;
                form_cheque.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("La cuenta tiene saldo insuficiente");
                return; 
            }
            con.cnn.Close();


        }

        public decimal getIdMoneda() {
            Conexion con = new Conexion();

            //CONSIGO ID DE MONEDA
            string query0 = "SELECT id_moneda FROM LPP.MONEDAS WHERE descripcion = '" + cmbMoneda.Text + "'";
            con.cnn.Open();
            SqlCommand command0 = new SqlCommand(query0, con.cnn);
            decimal id_moneda = Convert.ToDecimal(command0.ExecuteScalar());
            con.cnn.Close();
            return id_moneda;
        }

        public void GuardarDatos(Int32 cliente_receptor, decimal id_banco)
        {
            Conexion con = new Conexion();

            //INSERTO RETIRO
            string query = "INSERT INTO LPP.RETIROS (num_cuenta, importe, id_moneda,fecha)"
                            + " VALUES (" + num_cuenta + ", " + importe + ", " + id_moneda + ", CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103))";
                
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.ExecuteNonQuery();
            con.cnn.Close();

            //ACTUALIZO SALDO EN CUENTA
            string query4 = "UPDATE LPP.CUENTAS SET saldo = saldo - "+importe+" " +
                            "WHERE num_cuenta = "+num_cuenta+" ";
            MessageBox.Show(""+query4);
            con.cnn.Open();
            SqlCommand command4 = new SqlCommand(query4, con.cnn);
            command4.ExecuteNonQuery();
            con.cnn.Close();

            //OBTENGO ID DE RETIRO
            string query3 = "SELECT id_retiro FROM LPP.RETIROS"
                            +" WHERE num_cuenta = " + num_cuenta 
                            +" AND importe =" +importe
                            +" AND fecha = CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103)"
                            +" AND id_moneda ="+ id_moneda+ " ";
            con.cnn.Open();
            SqlCommand command3 = new SqlCommand(query3, con.cnn);
            decimal id_retiro = Convert.ToDecimal(command3.ExecuteScalar());
            con.cnn.Close();

            //INSERTO EN CHEQUE
            string query2 = "INSERT INTO LPP.CHEQUES (id_retiro,importe,fecha,id_banco,cliente_receptor) VALUES "
                +"(" +id_retiro +", "+ importe+", "
                + "convert(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103)"
                +", "+id_banco+", '"+cliente_receptor+"')";
            con.cnn.Open();
            SqlCommand command2 = new SqlCommand(query2, con.cnn);
            SqlDataReader lector2 = command2.ExecuteReader();
            con.cnn.Close();

            DialogResult dialogResult = MessageBox.Show("Su retiro se realizo correctamente. ¿Desea ver el comprobante?", "Retiro de Efectivo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                
                ListaRetiros lr = new ListaRetiros(id_retiro);
                this.Close();
                lr.Show();
                
            }
            else
            {
                this.Close();
            }
        
        }
    }
}
