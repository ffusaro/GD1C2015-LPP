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


        public ABMUsuario(string user, string ev)
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

            Conexion con = new Conexion();
            Conexion con2 = new Conexion();

            string query = "SELECT nombre FROM LPP.ROLES WHERE habilitado = 1 ";

            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            int i = 0;
            string rol;

            while (lector.Read())
            {
                rol = lector.GetString(0);
                chlRol.Items.Add(rol);

                if (evento != "A")
                {
                    int id_rol = getIdRol(rol);
                    query = "SELECT 1 FROM LPP.ROLESXUSUARIO ro " +
                            "JOIN LPP.ROLES r ON r.id_rol = ro.rol " +
                            " WHERE ro.username = '" + evento + "' AND r.id_rol = "+ id_rol +" "; 

                    con2.cnn.Open();
                    SqlCommand command2 = new SqlCommand(query, con2.cnn);
                    SqlDataReader lector2 = command2.ExecuteReader();

                    if (lector2.Read())
                    {
                        chlRol.SetItemChecked(i, true);
                    }
                    con2.cnn.Close();
                }
                i++;
            }
            
            if (usuario == "A")
                {
                    ckbHabilitado.Visible = false;
                }
                else
                {
                    string query2 = "SELECT U.username, U.habilitado, U.pregunta_secreta, U.respuesta_secreta " +
                                    "FROM LPP.USUARIOS U WHERE U.username = '" + evento + "'";


                    con2.cnn.Open();
                    SqlCommand command3 = new SqlCommand(query2, con2.cnn);
                    SqlDataReader lectorcito = command3.ExecuteReader(); 

                    if (lectorcito.Read())
                    {
                        txtUsuario.Text = lectorcito.GetString(0);
                        txtPass.Enabled = false;
                        txtPass.Text = "++++++++";
                        txtConfirmarPass.Text = "++++++++";
                        txtConfirmarPass.Enabled = false;
                        if (lectorcito.GetBoolean(1))
                            ckbHabilitado.Checked = true;
                        else
                            ckbHabilitado.Checked = false;

                        if (!lectorcito.IsDBNull(2))
                        {
                            cmbpregunta_secreta.Text = lectorcito.GetString(2);
                        }
                        if (!lectorcito.IsDBNull(3))
                        {
                            txtrespuesta_secreta.Text = lectorcito.GetString(3);
                        }

                    }
                    con.cnn.Close();

                    btnEliminar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnLimpiar.Enabled = false;


                }
        }

        
        private Int32 getIdRol(string rol)
        {
            Conexion con = new Conexion();

            //CONSIGO ID_ROL
            string query3 = "SELECT id_rol FROM LPP.ROLES WHERE nombre = '" + rol + "'";
            con.cnn.Open();
            SqlCommand command3 = new SqlCommand(query3, con.cnn);
            Int32 id_rol = Convert.ToInt32(command3.ExecuteScalar());
            con.cnn.Close();
            return id_rol;

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
            cmbpregunta_secreta.Text = "";
            txtrespuesta_secreta.Text = "";
            ckbHabilitado.Checked = false;
            

            if (evento == "A")
            {
                txtUsuario.Text = "";
                txtPass.Text = "";
                txtConfirmarPass.Text = "";
            }

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
                salida = "No se puede deshabilitar el Usuario" + ex.ToString();

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
            if (chlRol.CheckedItems == null)
            {
                MessageBox.Show("Elija un rol");
                return;
            }

          if (cmbpregunta_secreta.Text== "")
            {
                MessageBox.Show("Elija una pregunta secreta");
                return;
            }
          if (txtrespuesta_secreta.Text == "")
          {
              MessageBox.Show("Escriba una respuesta secreta");
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
                
                //INSERTO EL USUARIO
                string query1 = "INSERT INTO LPP.USUARIOS (username, pass, " +
                                "pregunta_secreta,respuesta_secreta,fecha_creacion) VALUES " +
                                "('" + txtUsuario.Text + "','" + Helper.Help.Sha256(txtPass.Text)+ "','" + cmbpregunta_secreta.Text + "','" + txtrespuesta_secreta.Text + "',CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103))";
                con1.cnn.Open();
                SqlCommand command = new SqlCommand(query1, con1.cnn);
                command.ExecuteNonQuery();
                con1.cnn.Close();

                //INSERTO EN ROLESXUSUARIO
                foreach (object itemCheck in chlRol.CheckedItems)
                {
                    int id_rol = getIdRol(itemCheck.ToString());
                    string query9 = "INSERT INTO LPP.ROLESXUSUARIO (rol, username) VALUES (" + id_rol + ",'" + txtUsuario.Text + "')";
                    con.cnn.Open();
                    SqlCommand command2 = new SqlCommand(query9, con.cnn);
                    command2.ExecuteNonQuery();
                    con.cnn.Close();
                }
                

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
                                ", fecha_ultimamodif = CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103) " +
                                "  WHERE username = '" + usuario + "'";

                Conexion con = new Conexion();
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query10, con.cnn);
                command.ExecuteNonQuery();
                con.cnn.Close(); ;

                if (ckbHabilitado.Checked) {
                    this.resetearIntentos();
                }

                foreach (object item in chlRol.Items)
                {
                    if (!chlRol.CheckedItems.Contains(item)){
                        int id_rol = getIdRol(item.ToString());
                        string query13 = "DELETE LPP.ROLESXUSUARIO WHERE username = '" + usuario + "' AND rol = "+ id_rol+"";
                        con.cnn.Open();
                        SqlCommand command13 = new SqlCommand(query13, con.cnn);
                        command13.ExecuteNonQuery();
                        con.cnn.Close();
                    }
                }
                
                foreach (object itemCheck in chlRol.CheckedItems)
                {
                    int id_rol = getIdRol(itemCheck.ToString());
                    if (!(this.usuarioYaTieneElRol(id_rol))) {
                        string query14 = "INSERT INTO LPP.ROLESXUSUARIO (rol, username) VALUES (" + id_rol + ",'" + usuario + "')";
                        con.cnn.Open();
                        SqlCommand command2 = new SqlCommand(query14, con.cnn);
                        command2.ExecuteNonQuery();
                        con.cnn.Close();
                    } 
                    
                }

                MessageBox.Show("Usuario Modificado exitosamente"); 

                padre_buscarUsuario.mp.Show();
                padre_buscarUsuario.Close();
                this.Close();
            }
        }

        public void resetearIntentos()
        {

            Conexion con = new Conexion();
            string query = "UPDATE LPP.USUARIOS SET intentos = 0 WHERE username = '" + txtUsuario.Text + "'";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.ExecuteNonQuery();
            con.cnn.Close();

        }

        private bool usuarioYaTieneElRol(Int32 id_rol) {
            Conexion con = new Conexion();
            string query = "SELECT COUNT(*) FROM LPP.ROLESXUSUARIO WHERE username = '" + txtUsuario.Text + "' AND rol = "+ id_rol+"";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            int cant = Convert.ToInt32(command.ExecuteScalar());
            con.cnn.Close();

            if (cant == 1)
                return true;
            else
                return false;
        
        }
       
        
    }
}



           
     
        
       
