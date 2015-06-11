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

namespace PagoElectronico.Login
{
    public partial class LogIn : Form
    {
        public MenuPrincipal mp = new MenuPrincipal();
        public DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
        
        public LogIn()
        {
            InitializeComponent();
            btnIngresar.Enabled = false;
            txtPass.Enabled = false;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (cmbRol.SelectedItem==null)
            {
                MessageBox.Show("Seleccione un Rol, por favor");
                return;
            }

            
            MessageBox.Show("Bienvenido/a   "+txtUsuario.Text,""+cmbRol.Text);
            
            mp.Show();
            mp.cargarUsuario(txtUsuario.Text, cmbRol.Text, this);
            txtPass.Text = "";
            txtUsuario.Text = "";
            txtPass.Enabled = false;
            txtUsuario.Focus();
            cmbRol.Text = "";
            btnIngresar.Enabled = false;
            this.Hide();
            
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            txtPass.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }


        private void btnRol_Click(object sender, EventArgs e)
        {

            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Ingrese un Nombre de Usuario, por favor");
                return;
            }
            if (txtPass.Text == "")
            {
                MessageBox.Show("Ingrese la Contraseña, por favor");
                return;
            }

            /*VERIFICA EXISTENCIA DE USUARIO Y CARGA LOS DATOS*/

            Conexion con = new Conexion();
            string query = "SELECT pass, intentos, habilitado " +
                           "FROM LPP.USUARIOS WHERE username = '" + txtUsuario.Text + "'";

            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();

            if (!lector.Read())
            {
                con.cnn.Close();
                MessageBox.Show("Usuario Inválido");
                txtUsuario.Text = "";
                txtPass.Text = "";
                return;
            }

            string pass = lector.GetString(0);
            int intFallidos = lector.GetInt32(1);
            bool userHabilitado = lector.GetBoolean(2);
           
            con.cnn.Close();

            /*VERIFICA HABILITACION*/

            if (!userHabilitado)
            {
                MessageBox.Show("Usuario Inhabilitado", "ERROR");
                return;
            }
            

            /*VALIDA CONTRASEÑA*/

            if (!(pass == txtPass.Text.Sha256()))
            {
                string query2;
               
                if (intFallidos >= 3)
                {
                    //SI HAY 3 INTENTOS FALLIDOS SE DESHABILITA AL USUARIO
                    query2 = "UPDATE LPP.USUARIOS SET habilitado = 0 WHERE username = '" + txtUsuario.Text + "'";
                    MessageBox.Show("Se ha inhabilitado al usuario");

                }
                else
                {

                    query2 = "UPDATE LPP.USUARIOS SET intentos = " + (intFallidos + 1) + " WHERE username = '" + txtUsuario.Text + "'";
                }


                //CARGO DATOS EN LOGUXSUARIO(Usuario incorrecto) AGREGAR TIPO INTENTO!
                DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
                string query4 = "INSERT INTO LPP.LOGSXUSUARIO (username,fecha,num_intento) VALUES ('" + txtUsuario.Text + "', '" + fechaConfiguracion + "', " + intFallidos + " )";
                con.cnn.Open();
                SqlCommand command4 = new SqlCommand(query4, con.cnn);
                command4.ExecuteNonQuery();
                con.cnn.Close();

                con.cnn.Open();
                MessageBox.Show("" + query2);
                SqlCommand command1 = new SqlCommand(query2, con.cnn);
                command1.ExecuteNonQuery();
                con.cnn.Close();
                MessageBox.Show("Contraseña Inválida");
                txtPass.Text = "";
                txtPass.Focus();
                return;
            }
            else
            {
                /*LIMPIA LOS INTENTOS FALLIDOS*/

                string query3 = "UPDATE LPP.USUARIOS SET intentos = 0 " +
                                "WHERE username = '" + txtUsuario.Text + "'";
                con.cnn.Open();
                SqlCommand command2 = new SqlCommand(query3, con.cnn);
                command2.ExecuteNonQuery();
                con.cnn.Close();

                //CARGO DATOS EN LOGUXSUARIO (Usuario correcto)
                string query6 = "INSERT INTO LPP.LOGSXUSUARIO (username,fecha,num_intento) VALUES ('" + txtUsuario.Text + "', '" + fechaConfiguracion + "', " + intFallidos + " )";
                con.cnn.Open();
                SqlCommand command6 = new SqlCommand(query6, con.cnn);
                command6.ExecuteNonQuery();
                con.cnn.Close();

                btnIngresar.Enabled = true;
                cmbRol.Enabled = true;
                btnRol.Enabled = false;

                /*CARGAR ROLES*/
                Conexion con1 = new Conexion();
                string query5 = "SELECT DISTINCT R.nombre FROM LPP.ROLES R JOIN LPP.ROLESXUSUARIO U " +
                                "ON U.rol = R.id_rol AND U.username = '" + txtUsuario.Text + " " +
                                "' AND R.habilitado = 1";

                con1.cnn.Open();
                SqlCommand command5 = new SqlCommand(query5, con1.cnn);
                SqlDataReader lector5 = command5.ExecuteReader();

                while (lector5.Read())
                {
                    cmbRol.Items.Add(lector5.GetString(0));
                }

                con1.cnn.Close();

            }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            btnRol.Enabled = true;
        }
      
       

       

     

       
    }
}
