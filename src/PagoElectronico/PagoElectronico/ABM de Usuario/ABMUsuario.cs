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

namespace PagoElectronico.ABM_de_Usuario
{
    public partial class ABMUsuario : Form
    {


        public BuscarUsuario padre_buscarUsuario;
        public MenuPrincipal mp;
        public string evento;
        public int ban;
        public string usuario;
        public DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
        
      
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
            cmbpregunta_secreta.Items.Add("¿Nombre de la Madre?");
            cmbpregunta_secreta.Items.Add("¿Nombre de la primera mascota?");
            cmbpregunta_secreta.Items.Add("¿Lugar de nacimiento?");

            this.cargarRoles();

            Conexion con = new Conexion();

            if (usuario == "A")
            {
                ckbHabilitado.Visible = false;
            }
            else
            {
                string query2 = "SELECT U.username, R.rol, U.habilitado, U.pregunta_secreta, U.respuesta_secreta " +
                                "FROM LPP.USUARIOS U JOIN LPP.ROLESXUSUARIO R ON R.username = U.username WHERE U.username = '" + evento + "'";
                              

                con.cnn.Open();
                SqlCommand command2 = new SqlCommand (query2, con.cnn);
                SqlDataReader lector2 = command2.ExecuteReader();;
                if (lector2.Read())
                {
                    txtUsuario.Text = lector2.GetString(0);
                    int id_rol = lector2.GetInt32(1);
                    this.cargarRoles();
                    cbRol.SelectedIndex = id_rol;
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


        public void cargarRoles() {
            Conexion con = new Conexion();
            cbRol.Items.Add("");
            string query1 = "SELECT nombre FROM LPP.ROLES WHERE habilitado = 1";

            con.cnn.Open();
            SqlCommand command = new SqlCommand(query1, con.cnn);
            SqlDataReader lector = command.ExecuteReader(); ;
            while (lector.Read())
            {
                cbRol.Items.Add(lector.GetString(0));
            }
            con.cnn.Close();
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
            txtUsuario.Focus();
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
            if (txtUsuario.Text == "" && ban == 1)
            {
                MessageBox.Show("Ingrese Usuario");
                return;
            }

            if (txtPass.Text == "" && ban == 1)
            {
                MessageBox.Show("Ingrese una contraseña");
                return;
            }
            if (txtConfirmarPass.Text == "" && ban == 1)
            {
                MessageBox.Show("Confirme contraseña");
                return;
            }
            if (txtConfirmarPass.Text != txtPass.Text && ban == 1)
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


                Conexion con1 = new Conexion();
                //OBTENGO ID DE ROL
                con1.cnn.Open();
                string query2 = "SELECT id_rol FROM LPP.ROLES WHERE nombre = '"+Convert.ToString(cbRol.SelectedItem)+"'";
                SqlCommand command2 = new SqlCommand(query2, con1.cnn);
                SqlDataReader lector2 = command2.ExecuteReader();
                lector2.Read();
                int id_rol = lector2.GetInt32(0);
                con1.cnn.Close();

                //INSERTO EL USUARIO
                string query1 = "INSERT INTO LPP.USUARIOS (username, pass, " +
                                "pregunta_secreta,respuesta_secreta,fecha_creacion) VALUES " +
                                "('" + txtUsuario.Text + "','" + Helper.Help.Sha256(txtPass.Text) + "','" + cmbpregunta_secreta.Text + "','" + txtrespuesta_secreta.Text + "',CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103))";
                con1.cnn.Open();
                SqlCommand command = new SqlCommand(query1, con1.cnn);
                command.ExecuteNonQuery();
                con1.cnn.Close();
                //INSERTO EN ROLESXUSUARIO
                string query9 = "INSERT INTO LPP.ROLESXUSUARIO (rol, username) VALUES (" + id_rol +",'" + txtUsuario.Text + "')";
                con.cnn.Open();
                SqlCommand command9 = new SqlCommand(query9, con.cnn);
                command9.ExecuteNonQuery();
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
                                ", habilitado = '" + ckbHabilitado.Checked + "' " +
                                "WHERE username = '" + usuario + "'";
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

                string query14 = "INSERT INTO LPP.ROLESXUSUARIO (rol, username) VALUES ('" + cbRol.SelectedIndex +
                            "','" + usuario + "')";
                con.cnn.Open();
                SqlCommand command14 = new SqlCommand(query14, con.cnn);
                command14.ExecuteNonQuery();
                con.cnn.Close();
                MessageBox.Show("Usuario Modificado exitosamente"); 

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



           
     
        
       