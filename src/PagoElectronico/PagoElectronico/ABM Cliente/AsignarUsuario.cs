using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            else {
                btCliente.Enabled = true;
                txtUsuario.Text = username;
                txtUsuario.Enabled = false;
                btnAsociar.Enabled = false;
                btClisinU.Enabled = false;
            }
            
        }

        private void btLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            btnAsociar.Enabled = true;
            btCliente.Enabled = false;
            txtUsuario.Enabled = true;
            btClisinU.Enabled = true;
        }

        private void btCliente_Click(object sender, EventArgs e)
        {
            FormCliente = new ABMCliente("A", txtUsuario.Text);
            FormCliente.Show();
            this.Close();
        }

        private void btnAsociar_Click(object sender, EventArgs e)
        {
            ABM_de_Usuario.BuscarUsuario bu = new ABM_de_Usuario.BuscarUsuario(0);
            bu.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ABMCliente abmCliente = new ABMCliente("A", "U");
            abmCliente.Show();
            abmCliente.padre_mp = padre_mp;
        }

     

       

      
    }
}
