using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    public partial class AsignarUsuario : Form
    {
        private ABMCliente FormCliente;
        private string usuario;
        public MenuPrincipal padre_mp;

        public AsignarUsuario(string evento, string username)
        {
            
            InitializeComponent();
            if (evento == "A")
            {
                btCliente.Enabled = false;
            }
            else 
            {
                btCliente.Enabled = true;
                txtUsuario.Text = username;
                txtUsuario.Enabled = false;
                btnAsociar.Enabled = false;
            }
            
        }

        private void btLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtUsuario.Enabled = false;
            btnAsociar.Enabled = true;
            btCliente.Enabled = false;
            txtUsuario.Enabled = true;
        }

        private void btCliente_Click(object sender, EventArgs e)
        {
            if(verificoUsuario())
            {
                MessageBox.Show("El usuario ya se encuentra relacionado con un Cliente");
                txtUsuario.Text = "";
                btnAsociar.Enabled = true;
                btCliente.Enabled = false;
                return;
            }
            else
            {
                FormCliente = new ABMCliente("A", txtUsuario.Text);
                FormCliente.Show();
                this.Close();
            }
        }

        private void btnAsociar_Click(object sender, EventArgs e)
        {
            ABM_de_Usuario.BuscarUsuario bu = new ABM_de_Usuario.BuscarUsuario(0);
            bu.Show();
            this.Close();
        }

 
        private bool verificoUsuario()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //VERIFICO SI EL USUARIO ESTA UNIDO A UN CLIENTE
            string query = "SELECT 1 FROM LPP.CLIENTES WHERE username = '" +txtUsuario.Text + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            if (lector.Read())
            {
                con.cnn.Close();
                return true;
            }
            else
            {
                con.cnn.Close();
                return false;
            }
         
            
        }
     

       

      
    }
}
