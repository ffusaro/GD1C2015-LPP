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

namespace PagoElectronico.Transferencias
{
    public partial class Transferencias : Form
    {
        public string usuario;
        public decimal num_cuenta_origen;
        public decimal num_cuenta_destino;
        public decimal importe;
        private bool ban = true;

        public Transferencias(string user,decimal num_cuenta)
        {
            InitializeComponent();
            usuario = user;
            if (ban)
            {
                grpDatos.Enabled = false;
                btnNuevo.Enabled = true;
                btnSalir.Enabled = true;
                btnGrabar.Enabled = false;
                btnLimpiar.Enabled = false;
            }
            //CARGA DE NUMEROS DE CUENTA DE ORIGEN
            Conexion con = new Conexion();
            string query = "LPP.PRC_cuentas_de_un_cliente";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id_cliente", getIdCliente()));
            SqlDataReader lector = command.ExecuteReader();
            while (lector.Read())
            {
                cmbNroCuenta.Items.Add(lector.GetDecimal(0));
            }

            con.cnn.Close();
            if(num_cuenta != 0)
            {
                txtCuentaDestino.Text = Convert.ToString(num_cuenta);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled = true;
            btnLimpiar.Enabled = true;
            btnGrabar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbNroCuenta.SelectedItem = null;
            txtCuentaDestino.Text = "";
            txtImporte.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtImporte.Text != "")
            {
                decimal temp;
                try
                {
                        temp = Convert.ToDecimal(txtImporte.Text);
                        if (temp < 0)
                        {
                            MessageBox.Show("El importe debe ser positivo.");
                            return;
                        }
                        else {
                            BuscarCuentas bc = new BuscarCuentas(usuario);
                            bc.num_cuenta_origen = Convert.ToDecimal(cmbNroCuenta.Text);
                            bc.importe = Convert.ToDecimal(txtImporte.Text);
                            bc.Show();
                            this.Close();
                        
                        }

                }
                catch (Exception h)
                {
                    MessageBox.Show("Importe solo puede contener números", h.ToString());
                    return;
                }

            }
            else {
                MessageBox.Show("Ingrese un Importe por favor");
                return;
            }
            
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (cmbNroCuenta.SelectedItem == null)
            {
                MessageBox.Show("Elija Número de Cuenta Origen por favor");
                return;
            }
            if(txtImporte.Text == "")
            {
                MessageBox.Show("Ingrese un Importe por favor");
                return;
            }
            decimal temp;
            try
            {
                if (txtImporte.Text != "")
                {
                    temp = Convert.ToDecimal(txtImporte.Text);
                    if (!(temp > 0 ))
                    {
                        MessageBox.Show("El importe debe ser positivo.");
                        return;
                    }
                }

            }
            catch (Exception h)
            {
                MessageBox.Show("Importe solo puede contener números", h.ToString());
                return;
            }
            
            if (txtCuentaDestino.Text == "")
            {
                MessageBox.Show("Elija un Numero de Cuenta Destino por favor");
                return;
            }
            try
            {
                if (txtCuentaDestino.Text != "")
                    temp = Convert.ToDecimal(txtCuentaDestino.Text);

            }
            catch (Exception h)
            {
                MessageBox.Show("Cuenta destino solo puede contener números", h.ToString());
                return;
            }

            if (this.tieneSaldo(Convert.ToDecimal(cmbNroCuenta.SelectedItem), Convert.ToDecimal(txtImporte.Text)))
            {
                Int32 id_trans = grabarTransferencia(Convert.ToDecimal(cmbNroCuenta.SelectedItem), Convert.ToDecimal(txtCuentaDestino.Text), Convert.ToDecimal(txtImporte.Text));

                DialogResult dialogResult = MessageBox.Show("Su Transferencia se realizo correctamente. ¿Desea ver el comprobante?", "Retiro de Efectivo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    cmbNroCuenta.SelectedItem = null;
                    txtImporte.Text = "";
                    grpDatos.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnLimpiar.Enabled = false;
                    btnSalir.Enabled = true;
                    btnGrabar.Enabled = false;
                    ListaTransferencias lt = new ListaTransferencias(id_trans);
                    lt.Show();
                    this.Close();
                }
                else
                {
                    this.Close();
                }

               // if (Helper.Help.VerificadorDeDeudas(getIdCliente()))
               //     MessageBox.Show("A partir de este momento, su cuenta se encuentra inhabilitada por tener mas de 5 transacciones sin facturar");

            }
            else {
                MessageBox.Show("Saldo insuficiente para esta transferencia.");
            }
            

        }

        private bool tieneSaldo(decimal num_cuenta, decimal importe) {
            Conexion con = new Conexion();
            string query = "SELECT saldo FROM LPP.CUENTAS WHERE num_cuenta = " + num_cuenta + "";
            
            con.cnn.Open();
            SqlCommand command2 = new SqlCommand(query, con.cnn);
            decimal saldo = Convert.ToDecimal(command2.ExecuteScalar());
            con.cnn.Close();

            if (saldo >= importe) {
                return true;
            } else {
                return false;
            }
        
        }
        private Int32 grabarTransferencia(decimal origen, decimal destino, decimal importe)
        {
            
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "LPP.PRC_realizar_transferencia";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_cuenta_origen", origen));
            command.Parameters.Add(new SqlParameter("@num_cuenta_destino", destino));
            command.Parameters.Add(new SqlParameter("@importe", importe));
            DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
            command.Parameters.Add(new SqlParameter("@fecha",fechaConfiguracion));
            //SqlDataReader lector = command.ExecuteReader();
            int rowsAffected = command.ExecuteNonQuery();
            con.cnn.Close();

            //OBTENGO ID TRANSFERENCIA
            string query2 = "SELECT id_transferencia FROM LPP.TRANSFERENCIAS"
                            +" WHERE num_cuenta_origen = " + origen 
                            +" AND num_cuenta_destino = " + destino
                            +" AND importe = " + importe
                            + " AND fecha = CONVERT(DATETIME, '" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103)";
            con.cnn.Open();
            SqlCommand command2 = new SqlCommand(query2, con.cnn);
            Int32 id_transferencia = Convert.ToInt32(command2.ExecuteScalar());
            con.cnn.Close();

            
            return id_transferencia;
        }
        
        private int getIdCliente()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //OBTENGO ID CLIENTE
            string query = "SELECT id_cliente FROM LPP.CLIENTES WHERE username = '" + usuario + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            int id_cliente = lector.GetInt32(0);
            con.cnn.Close();
            return id_cliente;

        }
       
        public void habilitarTools() {
            ban = false;
            grpDatos.Enabled = true;
            btnLimpiar.Enabled = true;
            btnGrabar.Enabled = true;
            btnNuevo.Enabled = false;
            txtCuentaDestino.Enabled = false;
        
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
      



    }
}
