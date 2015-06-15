using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using Helper;
using readConfiguracion;

namespace PagoElectronico.ABM_Cuenta
{

    public partial class AltaCuenta : Form
    {
        public BuscarCuenta padre_buscarCuenta;
        public MenuPrincipal mp;
        public string evento;
        public int ban;
        public string usuario;
        public DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);


        public AltaCuenta(string evnto,string user)
        {
            InitializeComponent();
            
            evento = user;
            usuario = evnto;
            
            txtUsuario.Enabled = false;
            txtPais.Enabled = false;
            cmbMoneda.Enabled = false;
            dtpFechaApertura.Enabled = false;
            cmbTipoCuenta.Enabled = false;
            dtpFechaCierre.Enabled = false;
            btnBuscar.Enabled = true;
            btnEliminar.Enabled = true;
            btnLimpiar.Enabled = false;

            dtpFechaApertura.Enabled = false;

            /* Busca y carga los tipos de moneda */

            Conexion con = new Conexion();
            cmbMoneda.Items.Add("");
            string query1 = "SELECT DISTINCT descripcion FROM LPP.MONEDA ";

            con.cnn.Open();
            SqlCommand command = new SqlCommand (query1, con.cnn);
            SqlDataReader lector = command.ExecuteReader();;
            while (lector.Read())
            {
                cmbMoneda.Items.Add(lector.GetString(0));
            }
            con.cnn.Close();

            /* Busca y carga los tipos de cuenta */
            Conexion con1 = new Conexion();
            cmbTipoCuenta.Items.Add("");
            string query5 = "SELECT DISTINCT description FROM LPP.TIPO_CUENTA ";

            con1.cnn.Open();
            SqlCommand command1 = new SqlCommand (query5, con1.cnn);
            SqlDataReader lector1 = command1.ExecuteReader();;
            while (lector1.Read())
            {
                cmbTipoCuenta.Items.Add(lector1.GetString(0));
            }
            con1.cnn.Close();


            if(usuario == "A"){
                ckbHabilitado.Enabled = false;
                dtpFechaCierre.Enabled = false;
            }
            else{
                 string query2 = "SELECT C.id_usuario, M.dscripcion, TC.tipo_cuenta, e.description " +
                                "FROM LPP.CUENTA C JOIN LPP.MONEDA  M ON C.id_moneda = M.id_moneda" +
                                                   "JOIN LPP.ESTADOCUENTA E C.id_estado = E.id_estado" +
                                                   "JOIN LPP.TIPOCUENTA TC C.id_tipo = TC.id_tipo WHERE C.username = '" + evento + "'"; 
                              

                con.cnn.Open();
                SqlCommand command2 = new SqlCommand (query2, con.cnn);
                SqlDataReader lector2 = command2.ExecuteReader();;
                if (lector2.Read())
                {
                    txtUsuario.Text = lector2.GetString(0);
                    cmbMoneda.Text = lector2.GetString(1);
                    cmbTipoCuenta.Text = lector2.GetString(2);
                    ckbHabilitado.Checked = lector2.GetBoolean(3);
                    dtpFechaApertura.Enabled = false;
                    dtpFechaCierre.Enabled= true;
                }
                con.cnn.Close();
                btnNuevo.Enabled = false;
                btnLimpiar.Enabled = true;
               
            }




           
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            /* Verifica que no haya campos vacios */

            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Ingrese Usuario");
                return;
            }
            if (cmbMoneda.Text == "")
            {
                MessageBox.Show("Elija una moneda");
                return;
            }

            if (cmbTipoCuenta.Text == "")
            {
                MessageBox.Show("Elija un tipo de cuenta");
            }
            
             if (txtPais.Text == "")
            {
                MessageBox.Show("Ingrese un pais");
            }


             if (dtpFechaCierre.Text == "")
            {
                MessageBox.Show("Elija una fecha de cierre");
            }

             if (dtpFechaApertura.Text == "")
            {
                MessageBox.Show("Elija una fecha de apertura");
            }

             if (ban == 1)
             {

                 /*VERIFICA EXISTENCIA DE USUARIO Y CARGA LOS DATOS*/
                 string query = "SELECT 1 " +
                                "FROM LPP.USUARIOS WHERE username = '" + txtUsuario.Text + "'";
                 Conexion con = new Conexion();
                 con.cnn.Open();
                 SqlCommand command3 = new SqlCommand(query, con.cnn);
                 SqlDataReader lector3 = command3.ExecuteReader();

                 if (!(lector3.Read()))
                 {
                     con.cnn.Close();
                     MessageBox.Show("Nombre de Usuario no existente, por favor elija otro");
                     return;
                 }
                 con.cnn.Close();


                 Conexion con1 = new Conexion();
                 //OBTENGO ID DE PAIS
                 con1.cnn.Open();
                 string query2 = "SELECT id_pais FROM LPP.PAIS WHERE descripcion = '" + txtPais.Text + "'";
                 SqlCommand command2 = new SqlCommand(query2, con1.cnn);
                 SqlDataReader lector2 = command2.ExecuteReader();
                 lector2.Read();
                 int id_pais = lector2.GetInt32(0);
                 con1.cnn.Close();



                 //OBTENGO ID DE CLIENTE
                 con1.cnn.Open();
                 string query4 = "SELECT id_cliente FROM LPP.CLIENTE WHERE username = '" + txtUsuario.Text + "'";
                 SqlCommand command4 = new SqlCommand(query4, con1.cnn);
                 SqlDataReader lector4 = command2.ExecuteReader();
                 lector2.Read();
                 int id_cliente = lector4.GetInt32(0);
                 con1.cnn.Close();


                 //OBTENGO ID MONEDA
                 con1.cnn.Open();
                 string query5 = "SELECT id_moneda FROM LPP.PAIS WHERE descripcion = '" + Convert.ToString(cmbMoneda.SelectedItem) + "'";
                 SqlCommand command5 = new SqlCommand(query5, con1.cnn);
                 SqlDataReader lector5 = command5.ExecuteReader();
                 lector5.Read();
                 int id_moneda = lector5.GetInt32(0);
                 con1.cnn.Close();

                 //OBTENGO ID DE TIPO_CUENTA
                 con1.cnn.Open();
                 string query6 = "SELECT id_tipo FROM LPP.TIPO_CUENTA WHERE descripcion = '" + Convert.ToString(cmbTipoCuenta.SelectedItem) + "'";
                 SqlCommand command6 = new SqlCommand(query6, con1.cnn);
                 SqlDataReader lector6 = command6.ExecuteReader();
                 lector6.Read();
                 int id_tipoCuenta = lector6.GetInt32(0);
                 con1.cnn.Close();

                 //INSERTO LA CUENTA
                 string query1 = "INSERT INTO LPP.CUENTA (id_pais, " +
                                 "id_cliente,id_moneda,fecha_apertura,id_tipo) VALUES " +
                                 "('"+ id_pais +"','" + id_cliente + "','" + id_moneda + "', Convert(DateTime,'readConfiguracion.Configuracion.fechaSystem()+"00:00:00.000"',103) ,'" + id_tipoCuenta + "')";
                 con1.cnn.Open();
                 SqlCommand command = new SqlCommand(query1, con1.cnn);
                 command.ExecuteNonQuery();
                 con1.cnn.Close();

                 con1.cnn.Open();
                 string query11 = "SELECT num_cuenta FROM LPP.TIPO_CUENTA WHERE id_cliente = '" + id_cliente + "'";
                 SqlCommand command11 = new SqlCommand(query11, con1.cnn);
                 SqlDataReader lector11 = command6.ExecuteReader();
                 lector11.Read();
                 int id_cuenta = lector11.GetInt32(0);
                 con1.cnn.Close();

                 

                 MessageBox.Show("Alta de Cuenta Exitosa, su Numero de cuenta es: id_cuenta");

                 mp.Show();
                 this.Close();
             }
             if (ban == 2){

              //OBTENGO ID DE CLIENTE
             Conexion con1 = new Conexion();
             con1.cnn.Open();
             string query10 = "SELECT id_cliente FROM LPP.CLIENTE WHERE username = '" + txtUsuario.Text + "'";
             SqlCommand command10 = new SqlCommand(query10, con1.cnn);
             SqlDataReader lector10 = command10.ExecuteReader();
             lector10.Read();
             int id_clienteMod = lector10.GetInt32(0);
             con1.cnn.Close();

             //OBTENGO ID MONEDA
             con1.cnn.Open();
             string query7 = "SELECT id_moneda FROM LPP.PAIS WHERE descripcion = '" + Convert.ToString(cmbMoneda.SelectedItem) + "'";
             SqlCommand command7 = new SqlCommand(query7, con1.cnn);
             SqlDataReader lector7 = command7.ExecuteReader();
             lector7.Read();
             int id_monedaMod = lector7.GetInt32(0);
             con1.cnn.Close();

             //OBTENGO ID DE TIPO_CUENTA
             con1.cnn.Open();
             string query8 = "SELECT id_tipo FROM LPP.TIPO_CUENTA WHERE descripcion = '" + Convert.ToString(cmbTipoCuenta.SelectedItem) + "'";
             SqlCommand command8 = new SqlCommand(query8, con1.cnn);
             SqlDataReader lector8 = command8.ExecuteReader();
             lector8.Read();
             int id_tipoCuentaMod = lector8.GetInt32(0);
             con1.cnn.Close();


             string query9 = "UPDATE LPP.CUENTA SET " +
                                 " id_moneda = '" + id_monedaMod +
                                 "'id_tipo = '" + id_tipoCuentaMod + "'" +
                                 ", habilitado = '" + ckbHabilitado.Checked + "' " +
                                 "WHERE id_cliente = '" + id_clienteMod + "'";
                 Conexion con = new Conexion();
                 con.cnn.Open();
                 SqlCommand command = new SqlCommand(query9, con.cnn);
                 command.ExecuteNonQuery();
                 con.cnn.Close(); ;


                 padre_buscarCuenta.mp.Show();
                 padre_buscarCuenta.Close();
                 this.Close();
             }


}

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //OBTENGO ID DE TIPO_CUENTA
            Conexion con1 = new Conexion();
            con1.cnn.Open();
            string query6 = "SELECT id_tipo FROM LPP.TIPO_CUENTA WHERE descripcion = '" + Convert.ToString(cmbTipoCuenta.SelectedItem) + "'";
            SqlCommand command6 = new SqlCommand(query6, con1.cnn);
            SqlDataReader lector6 = command6.ExecuteReader();
            lector6.Read();
            int id_tipoCuenta = lector6.GetInt32(0);
            con1.cnn.Close();
            
            
            Conexion con = new Conexion();
            string salida = "Se deshabilito el Usuario correctamente";
            con.cnn.Open();
            try
            {

                string query = " UPDATE LPP.ESTADOCUENTA SET habilitado = 0 WHERE num_cuenta = '" + id_tipoCuenta + "' ";
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
            txtPais.Text = "";
            btnEliminar.Enabled = false;
            txtUsuario.Text = "";
            ckbHabilitado.Checked = false;
            cmbTipoCuenta.SelectedItem = null;
            cmbMoneda.SelectedItem = null;
      }

        
     

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtUsuario.Focus();
            txtPais.Enabled = true;
            btnNuevo.Enabled = false;
            btnBuscar.Enabled = false;
            btnLimpiar.Enabled = true;
            btnContinuar.Enabled = true;
            btnEliminar.Enabled = false;
            gbDatosCliente.Enabled = true;
            gbDatosCuenta.Enabled = true;
            cmbTipoCuenta.Enabled = true;
            dtpFechaCierre.Enabled = false;
            ckbHabilitado.Enabled = false;
            ban = 1;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            cmbMoneda.Enabled = true;
            cmbTipoCuenta.Enabled = true;
            txtUsuario.Enabled = false;
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnBuscar.Enabled = false;
            btnLimpiar.Enabled = true;
            btnContinuar.Enabled = true;
            txtPais.Enabled = true;
            dtpFechaCierre.Enabled = true;
            ckbHabilitado.Enabled = false;
            ban = 2;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarCuentas bc = new BuscarCuentas();
            bc.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPais.Text = "";
            txtUsuario.Text = "";
            ckbHabilitado.Checked = false;
            cmbTipoCuenta.SelectedItem = null;
            cmbMoneda.SelectedItem = null;

        }

        

        
}

}  
        
       


        

        
    
