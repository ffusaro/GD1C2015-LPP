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
        public bool entro;
        public int intFallidos;
        public bool userHabilitado;
        public string pass = "";
        ABMS abm = new ABMS();

        public LogIn()
        {
            InitializeComponent();
            txtPass.Enabled = false;
                       
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();

            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Ingrese el usuario, por favor");
                return;
            }

            if (txtPass.Text == "")
            {
                MessageBox.Show("Ingrese la Contraseña, por favor");
                return;
            }

            /*VALIDA CONTRASEÑA*/
            this.busquedaDatosUsuario();

            if (!(pass == txtPass.Text.Sha256()))
            {
                
                if (intFallidos >= 3)
                { //SI HAY 3 INTENTOS FALLIDOS SE DESHABILITA AL USUARIO
                    if (intFallidos == 3)
                    {
                        string query2;
                        query2 = "UPDATE LPP.USUARIOS SET habilitado = 0, intentos = "+ intFallidos +" WHERE username = '" + txtUsuario.Text + "'";
                        MessageBox.Show("Se ha inhabilitado al usuario");
                        con.cnn.Open();
                        SqlCommand command1 = new SqlCommand(query2, con.cnn);
                        command1.ExecuteNonQuery();
                        con.cnn.Close();
                
                    }
                    else {
                        MessageBox.Show("Le recordamos que el usuario ha sido inhabilitado");
                        
                    }
                    this.insertarEnLog();

                }
                else
                {
                    
                    string query2;
                    query2 = "UPDATE LPP.USUARIOS SET intentos = " + intFallidos + " WHERE username = '" + txtUsuario.Text + "'";
                    con.cnn.Open();
                    SqlCommand command1 = new SqlCommand(query2, con.cnn);
                    command1.ExecuteNonQuery();
                    con.cnn.Close();
                    this.insertarEnLog();
                    MessageBox.Show("Contraseña Inválida");
                    
                }

                intFallidos++;
                entro = false;
                txtPass.Text = "";
                txtPass.Focus();

                return;
            }
            else
            {
                
                this.busquedaDatosUsuario();

                if (this.verificarHabilitacionUsuario() == 0)
                {
                    MessageBox.Show("Le recordamos que el usuario ha sido inhabilitado");
                    return;
                }
                else {

                    entro = true;

                    btnIngresar.Enabled = true;
                }
                
            }

            this.insertarEnLog();
            this.resetearIntentos();
            if (this.usuarioTieneVariosRoles()) {

                RolIngreso ri = new RolIngreso(txtUsuario.Text, this);
                ri.Show();
                this.Hide();

            }
            else
            {
                abm.ingresarAlSistema(txtUsuario.Text, this, mp, this, getRolUser());
            }          
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
           
            /*VERIFICA EXISTENCIA DE USUARIO Y CARGA LOS DATOS*/
            this.busquedaDatosUsuario();
            
            btnIngresar.Enabled = true;
            
        }


        public void resetearIntentos()
        {

            Conexion con = new Conexion();
            string query = "UPDATE LPP.USUARIOS SET intentos = " + 0 + " WHERE username = '" + txtUsuario.Text + "'";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.ExecuteNonQuery();
            con.cnn.Close();

        }

        public bool usuarioTieneVariosRoles() {

            Conexion con = new Conexion();
            string query = "SELECT COUNT(*) FROM LPP.ROLESXUSUARIO WHERE username = '" + txtUsuario.Text + "'";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            Int32 cantRoles = Convert.ToInt32(command.ExecuteScalar());
            con.cnn.Close();

            if (cantRoles > 1)
                return true;
            else
                return false;

        }

        public void busquedaDatosUsuario() {
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

            pass = lector.GetString(0);
            intFallidos = lector.GetInt32(1) + 1;
            userHabilitado = lector.GetBoolean(2);
            con.cnn.Close();
        }

        public void insertarEnLog()
        {
           Conexion con = new Conexion();
           DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
           if (entro)
           {
               //CARGO DATOS EN LOGUXSUARIO (Usuario correcto)
               string query6 = "INSERT INTO LPP.LOGSXUSUARIO (username,fecha,num_intento,logueo) VALUES ('" + txtUsuario.Text + "', convert(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103), " + intFallidos  + ", 1)";
               con.cnn.Open();
               SqlCommand command6 = new SqlCommand(query6, con.cnn);
               command6.ExecuteNonQuery();
               con.cnn.Close();

               if (getRolUser() == "Administrador")
               {
                   string query0 = "LPP.PRC_deshabilitacion_x_vencimiento_administrador";
                   con.cnn.Open();
                   SqlCommand command = new SqlCommand(query0, con.cnn);
                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.Add(new SqlParameter("@fecha_sist", fechaConfiguracion));
                   command.ExecuteNonQuery();
                   con.cnn.Close();

               }
               else
               {
                   string query0 = "LPP.PRC_deshabilitacion_x_vencimiento_clientes";
                   con.cnn.Open();
                   SqlCommand command = new SqlCommand(query0, con.cnn);
                   command.CommandType = CommandType.StoredProcedure;
                  // DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
                   command.Parameters.Add(new SqlParameter("@fecha_sist", fechaConfiguracion));
                   command.Parameters.Add(new SqlParameter("@user", txtUsuario.Text));
                   command.ExecuteNonQuery();
                   con.cnn.Close();
               }


           }
           else
           {
               //CARGO DATOS EN LOGUXSUARIO(Usuario incorrecto) AGREGAR TIPO INTENTO!
               string query4 = "INSERT INTO LPP.LOGSXUSUARIO (username,fecha,num_intento,logueo) VALUES ('" + txtUsuario.Text + "', convert(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103), " + intFallidos + ", 0 )";
               con.cnn.Open();
               SqlCommand command4 = new SqlCommand(query4, con.cnn);
               command4.ExecuteNonQuery();
               con.cnn.Close();
               
           }
       }

        private string getRolUser()
        {
            Conexion con = new Conexion();
            //OBTENGO USUARIO DEL ROL
            con.cnn.Open();
            string query = "SELECT R.nombre FROM LPP.ROLESXUSUARIO U JOIN LPP.ROLES R ON R.id_rol=U.rol WHERE U.username = '" + txtUsuario.Text + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            string rol = lector.GetString(0);
            con.cnn.Close();
            return rol;
        } 

        private bool verificoSiDebe()
        {
           
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "SELECT num_cuenta FROM LPP.CUENTAS WHERE id_cliente= "+getIdCliente()+" AND id_estado = 4";
            bool debe = false;
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            
            if (lector.Read())
            {
                debe = true;
                con.cnn.Close();
                
            }

            con.cnn.Close();
            return debe;
        }

        private Int32 verificarHabilitacionUsuario() {
            Conexion con = new Conexion();
            
            con.cnn.Open();
            string query = "SELECT habilitado FROM LPP.USUARIOS WHERE username = '" + txtUsuario.Text + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            Int32 habilitado = Convert.ToInt32(command.ExecuteScalar());
            con.cnn.Close();
            return habilitado;
        
        }

        private int getIdCliente()
        {
            Conexion con = new Conexion();
            //OBTENGO ID DE CLIENTE
            con.cnn.Open();
            string query = "SELECT id_cliente FROM LPP.CLIENTES WHERE username = '" + txtUsuario.Text + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            int id_cliente = lector.GetInt32(0);
            con.cnn.Close();
            return id_cliente;

        }

       
    }
}
