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

namespace PagoElectronico
{
    public partial class CambiarContraseña : Form
    {
        public MenuPrincipal padre_PostL;
        public string user;
        public string pass_act;
        public string pass_new;

        public CambiarContraseña(string padre)
        {
            InitializeComponent();
            user = padre;


            /*OBTIENE LA CONTRASEÑA ACTUAL*/
            Conexion con = new Conexion();
            string query = "SELECT pass FROM LPP.USUARIOS WHERE username = '" + user + "'";

            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            pass_act = lector.GetString(0);
            con.cnn.Close();

        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            /*VERIFICA QUE LOS CAMPOS NO ESTEN VACIOS*/
            if ((txtPass.Text == "") || (txtConfirmPass.Text == "") || (txtNuevaPass.Text == ""))
            {
                MessageBox.Show("Faltan Ingresar Datos", "ERROR");
                return;
            }

            /*VERIFICA LA CONTRASEÑA ANTERIOR*/
            if (!(pass_act == txtPass.Text.Sha256()))
            {
                MessageBox.Show("Contraseña Actual Incorrecta");
                txtConfirmPass.Text = "";
                txtNuevaPass.Text = "";
                txtPass.Text = "";
                return;
            }

            /*VERIFICA QUE LAS NUEVAS CONTRASEÑAS COINCIDAN*/
            if (txtNuevaPass.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("¡Las Nuevas Contraseñas No Coinciden!", "ERROR");
                txtConfirmPass.Text = "";
                txtNuevaPass.Text = "";
                txtPass.Text = "";
                return;
            }
            /*EFECTUA LA ACTUALIZACION DE PASS*/
            pass_new = txtNuevaPass.Text.Sha256();

            Conexion con = new Conexion();

            string query = "UPDATE LPP.USUARIOS SET pass = '" + pass_new + "' WHERE Usuario = '" + user + "'";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.ExecuteNonQuery();
            con.cnn.Close();
            MessageBox.Show("Contraseña Cambiada con Éxito");
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   



    }
}
