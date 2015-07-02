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

namespace PagoElectronico.ABM_Cuenta
{
    public partial class ConsultaSaldos : Form
    {
        private string usuario;
        private bool ban;

        public ConsultaSaldos(string user)
        {
            InitializeComponent();

            usuario = user;
            ban = true;
            Conexion con = new Conexion();
            if (getRolUser() == "Administrador") {
                string query = "SELECT num_cuenta FROM LPP.CUENTAS WHERE id_estado = 1 OR id_estado = 4";
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);

                SqlDataReader lector = command.ExecuteReader();
                while (lector.Read())
                {
                    cmbNroCuenta.Items.Add(lector.GetDecimal(0));
                }

                con.cnn.Close();
            }
            else
            {
                
                string query ="SELECT num_cuenta FROM LPP.CUENTAS WHERE (id_estado = 1 OR id_estado = 4) AND id_cliente = "+getIdCliente()+"";
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);

                SqlDataReader lector = command.ExecuteReader();
                while (lector.Read())
                {
                    cmbNroCuenta.Items.Add(lector.GetDecimal(0));
                }

                con.cnn.Close();
            }
        }

        private Int32 getIdCliente()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //OBTENGO ID CLIENTE
            string query = "SELECT id_cliente FROM LPP.CLIENTES WHERE username = '" + usuario + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            Int32 id_cliente = Convert.ToInt32(command.ExecuteScalar());
            con.cnn.Close();
            return id_cliente;

        }

        private void btSaldo_Click(object sender, EventArgs e)
        {
            this.validarNroCuenta();
            if (ban)
            {
                this.abrirResultados("S");
            }
        }

        private void btDepositos_Click(object sender, EventArgs e)
        {
            this.validarNroCuenta();
            if (ban)
            {
                this.abrirResultados("D");
            }
        }

        private void btRetiros_Click(object sender, EventArgs e)
        {
            this.validarNroCuenta();
            if (ban)
            {
                this.abrirResultados("R");
            }
        }

        private void btTransf_Click(object sender, EventArgs e)
        {
            this.validarNroCuenta();
            if (ban)
            {

                this.abrirResultados("T");
            }
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btLimpiar_Click(object sender, EventArgs e)
        {
            cmbNroCuenta.SelectedItem = null;
        }

        private string getRolUser()
        {
            MenuPrincipal mp = new MenuPrincipal();
            return mp.getRolLogueado();
        }
        private void validarNroCuenta()
        {
            if (cmbNroCuenta.SelectedItem == null)
            {
                MessageBox.Show("Elija Número de Cuenta por favor");
                ban = false;
                return;
            }
            else {
                ban = true;
            }
        }

        private void abrirResultados(string evento) 
        {
            ABM_Cuenta.ResultadosConsulta re = new ABM_Cuenta.ResultadosConsulta(evento, Convert.ToDecimal(cmbNroCuenta.Text));
            re.user = usuario;
            re.Show();
            this.Hide();
        }
    }
}
