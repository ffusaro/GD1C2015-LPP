using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class AsignarUsuarioCuenta : Form
    {
        private ABM_Cuenta.AltaCuenta ac;
        private string usuario;
        public MenuPrincipal padre_mp;
        public AsignarUsuarioCuenta(string evento, string username)
        {
            InitializeComponent();
            btnCuenta.Enabled = false;
           if (evento == "A")
            {
                usuario = username;
                txtUsuario.Text = username;
                txtUsuario.Enabled = false;
                btnCuenta.Enabled = true;
            }
           
        }

        private void btLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            btnAsociar.Enabled = true;
        }

        private void txtCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAsociar_Click(object sender, EventArgs e)
        {
            ABM_de_Usuario.BuscarUsuario bu = new ABM_de_Usuario.BuscarUsuario(2);
            bu.Show();
            this.Close();
          
        }

        private void btnCuenta_Click(object sender, EventArgs e)
        {
            ABM_Cuenta.AltaCuenta abmC = new ABM_Cuenta.AltaCuenta("A",usuario,0);
            abmC.Show();
            this.Close();
        }

        
    }
}
