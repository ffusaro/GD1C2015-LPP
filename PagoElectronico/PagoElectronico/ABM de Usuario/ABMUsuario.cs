using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Helper;
using readConfiguracion;
using System.Timers;

namespace PagoElectronico
{
    public partial class ABMUsuario : Form
    {


        public BuscarUsuario padre_buscarUsuario;
        public MenuPrincipal mp;
        public string evento;
        public int ban;
        public string usuario;
        
      
        public ABMUsuario(string user,string ev)
        {
            evento = user;
            usuario = ev;

            InitializeComponent();
            boxDatos.Enabled = false;
            btnModificar.Enabled = false;
            btnLimpiar.Enabled = false;
            btnSalir.Enabled = true;
            btnEliminar.Enabled = false;
            btnContinuar.Enabled = false;
            btnBuscar.Enabled = true;
          
            Conexion con = new Conexion();
            cbRol.Items.Add("");
            string query1 = "SELECT nombre FROM LPP.ROLES WHERE habilitado = 1";

            con.cnn.Open();
            SqlCommand command = new SqlCommand (query1, con.cnn);
            SqlDataReader lector = command.ExecuteReader();;
            while (lector.Read())
            {
                cbRol.Items.Add(lector.GetString(0));
            }
            con.cnn.Close();

            if (usuario == "A")
            {
                ckbHabilitado.Visible = false;
            }
            else 
            {
                string query2 = "SELECT U.username, R.rol, U.habilitado, U.pregunta_secreta, U.respuesta_secreta " +
                                "FROM LPP.USUARIOS U JOIN LPP.ROLESxUSUARIO R ON R.username = U.username WHERE U.username = '" + evento + "'";
                              

                con.cnn.Open();
                SqlCommand command2 = new SqlCommand (query2, con.cnn);
                SqlDataReader lector2 = command2.ExecuteReader();;
                if (lector2.Read())
                {
                    txtUsuario.Text = lector2.GetString(0);
                    cbRol.Text = lector2.GetString(1);
                    txtPass.Enabled = false;
                    txtPass.Text = "++++++++";
                    txtConfirmarPass.Text = "++++++++";
                    txtConfirmarPass.Enabled = false;
                    ckbHabilitado.Checked = lector2.GetBoolean(2);
                    cmbpregunta_secreta.Text = lector2.GetString(3);
                    txtrespuesta_secreta.Text = lector2.GetString(4);
                }
                con.cnn.Close();
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                btnNuevo.Enabled = false;
                btnLimpiar.Enabled = true;
               
            }
        }

        public static bool fechaMenorA(int fecha)
        {
            bool ret;
            int hoy;
            DateTime fechaInicio = DateTime.Today;
            hoy = Convert.ToInt32(fechaInicio.ToString("yyyy-mm-dd").Replace('-','0'));
            if(fecha<=hoy)
                ret= true;
            else
                ret=false;
            return ret;
        }
        public static bool fechaMayorA(int fecha)
        {
            bool ret;
            int hoy;
            DateTime fechaInicio = DateTime.Today;
            hoy = Convert.ToInt32(fechaInicio.ToString("yyyy-mm-dd").Replace('-','0'));
            if(fecha>=hoy)
                ret= true;
            else
                ret=false;
            return ret;
        }
      
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (evento == "A")
            {
                mp.Show();
                this.Close();
            }
            else
            {
                padre_buscarUsuario.mp.Show();
                padre_buscarUsuario.Close(); 
                this.Close();
            }
        }

        private void btnLim_Click(object sender, EventArgs e)
        {
            cbRol.Text = "";
            ckbHabilitado.Checked = false;
            cmbpregunta_secreta.Text = "";
            txtrespuesta_secreta.Text = "";

            if (evento == "A")
            {
                txtUsuario.Text = "";
                txtPass.Text = "";
                txtConfirmarPass.Text = "";
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtUsuario.Focus();
            boxDatos.Enabled = true;
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnBuscar.Enabled = false;
            btnLimpiar.Enabled = true;
            btnContinuar.Enabled = true;
            ban = 1;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            boxDatos.Enabled = true;
            txtPass.Enabled = false;
            txtConfirmarPass.Enabled = false;
            txtUsuario.Enabled = false;
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnBuscar.Enabled = false;
            btnLimpiar.Enabled = true;
            btnContinuar.Enabled = true;
            ban = 2;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
           
            txtPass.Text = "";
            txtConfirmarPass.Text = "";
            cmbpregunta_secreta.Text = "";
            txtrespuesta_secreta.Text = "";
            txtUsuario.Text = "";
            cbRol.SelectedItem = null;
            ckbHabilitado.Checked = false;
            btnNuevo.Enabled=true;

       }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
           
            BuscarUsuario bu = new BuscarUsuario(1);
            bu.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            string salida = "Se deshabilito el Usuario correctamente";
            con.cnn.Open();
            try
            {

                string query = " UPDATE LPP.USUARIOS SET habilitado = 0 WHERE username = '" + txtUsuario.Text + "' ";
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                salida = " No se puede deshabilitar el Usuario" + ex.ToString();

            }
            con.cnn.Close();
            MessageBox.Show("" + salida);

            //Habilito/Deshabilito botones
            btnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = true;
            btnLimpiar.Enabled = false;
            btnContinuar.Enabled = false;

           //Limpio los campos
            txtPass.Text = "";
            txtConfirmarPass.Text = "";
            btnEliminar.Enabled = false;
            txtUsuario.Text = "";
            cbRol.SelectedItem = null;
            cmbpregunta_secreta.Text = "";
            txtrespuesta_secreta.Text = "";
            ckbHabilitado.Checked = false;

        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            
            /*VERIFICA QUE NO HAYA NINGUN CAMPO VACIO*/
            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Ingrese Usuario");
                return;
            }

            if (txtPass.Text == "")
            {
                MessageBox.Show("Ingrese una contraseña");
                return;
            }
            if (txtConfirmarPass.Text == "")
            {
                MessageBox.Show("Confirme contraseña");
                return;
            }
            if (txtConfirmarPass.Text != txtPass.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden");
                return;
            }
            if (cbRol.Text == "")
            {
                MessageBox.Show("Elija un rol");
                return;
            }

          if (cmbpregunta_secreta.Text== "")
            {
                MessageBox.Show("Elija una pregunta_secreta");
                return;
            }
          if (txtrespuesta_secreta.Text == "")
          {
              MessageBox.Show("Escriba una respuesta_secreta");
              return;
          }
          
            if (ban == 1)
            {

                /*VERIFICA EXISTENCIA DE USUARIO Y CARGA LOS DATOS*/
                string query = "SELECT 1 " +
                               "FROM LPP.USUARIOS WHERE username = '" + txtUsuario.Text + "'";
                Conexion con = new Conexion();
                con.cnn.Open();
                SqlCommand command6 = new SqlCommand(query, con.cnn);
                SqlDataReader lector6 = command6.ExecuteReader();

                if (lector6.Read())
                {
                    con.cnn.Close();
                    MessageBox.Show("Nombre de Usuario existente, por favor elija otro");
                    return;
                }
                con.cnn.Close();


                string query1 = "INSERT INTO LPP.USUARIO (username, pass, " +
                                "pregunta_secreta,respuesta_secreta) VALUES " +
                                "('" + txtUsuario.Text + "','" + Helper.Help.Sha256(txtPass.Text) + "'" +
                                "'"+cmbpregunta_secreta.Text+"', '"+txtrespuesta_secreta.Text+"')";

                Conexion con1 = new Conexion();
                con1.cnn.Open();
                
                SqlCommand command = new SqlCommand(query1, con1.cnn);
                command.ExecuteNonQuery();
                con1.cnn.Close();

                string query9 = "INSERT INTO LPP.ROLESXUSUARIO (rol, username) VALUES ('" + cbRol.Text +
                                "','" + txtUsuario.Text + "')";
                con.cnn.Open();
                
                SqlCommand command2 = new SqlCommand(query9, con.cnn);
                command2.ExecuteNonQuery();
                con.cnn.Close();

                MessageBox.Show("Alta de Usuario Exitosa");

                mp.Show();
                this.Close();
            }
            if (ban == 2)
            {
                string query10 = "UPDATE LPP.USUARIOS SET " +
                                " pregunta_secreta = '" + cmbpregunta_secreta.Text +
                                "', respuesta_secreta = '" + txtrespuesta_secreta.Text+ "'" +
                                ", Habilitado = '" + ckbHabilitado.Checked + "' " +
                                "WHERE username = '" + txtUsuario.Text + "'";
                Conexion con = new Conexion();
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query10, con.cnn);
                command.ExecuteNonQuery();
                con.cnn.Close(); ;

                //Cambio dependencias
                string query13 = "DELETE LPP.ROLESXUSUARIO WHERE username = '" + txtUsuario.Text + "'";
                con.cnn.Open();
                SqlCommand command13 = new SqlCommand(query13, con.cnn);
                command13.ExecuteNonQuery();
                con.cnn.Close(); ;

                string query14 = "INSERT INTO LPP.ROLESXUSUARIO (rol, username) VALUES ('" + cbRol.Text +
                            "','" + txtUsuario.Text + "')";
                MessageBox.Show("Usuario Modificado exitosamente");
                con.cnn.Open();
                SqlCommand command14 = new SqlCommand(query14, con.cnn);
                command14.ExecuteNonQuery();
                con.cnn.Close(); ;

                padre_buscarUsuario.mp.Show();
                padre_buscarUsuario.Close();
                this.Close();
            }
        }

        private void ABMUsuario_Load(object sender, EventArgs e)
        {

        }

       
        
    }
}



           
     
        
       