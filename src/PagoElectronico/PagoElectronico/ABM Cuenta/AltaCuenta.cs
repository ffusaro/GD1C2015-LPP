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
using Helper;
using readConfiguracion;

namespace PagoElectronico.ABM_Cuenta
{

    public partial class AltaCuenta : Form
    {
        
        public MenuPrincipal mp;
        public string evento;
        public int ban;
        public string usuario;
        public DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
        public decimal num_cuenta;
        private string cuentaCambio;
        
        public AltaCuenta(string evnto,string user,decimal cuenta)
        {
            InitializeComponent();

            evento = evnto;
            usuario = user;
            num_cuenta = cuenta;
            txtUsuario.Enabled = false;
            lblCuenta.Visible = false;
            txtUsuario.Text = usuario;
            txtCuenta.Visible = false;
            gbDatosCuenta.Enabled = false;
            gbTipoCuenta.Enabled = false;
            btnBuscar.Enabled = true;
            btnLimpiar.Enabled = false;
            btnNuevo.Enabled = true;
            btnSalir.Enabled = true;
            btnContinuar.Enabled = false;

            // Busca y carga los tipos de moneda 
            Conexion con = new Conexion();
            string query1 = "SELECT descripcion FROM LPP.MONEDAS ";
            con.cnn.Open();
            SqlCommand command = new SqlCommand (query1, con.cnn);
            SqlDataReader lector = command.ExecuteReader();;
            while (lector.Read())
            {
                cmbMoneda.Items.Add(lector.GetString(0));
            }
            con.cnn.Close();
            // Busca y carga los paies
            
            string query3 = "SELECT pais FROM LPP.PAISES ORDER BY pais ";
            con.cnn.Open();
            SqlCommand command3 = new SqlCommand(query3, con.cnn);
            SqlDataReader lector3 = command3.ExecuteReader();
            while (lector3.Read())
            {
                cmbPaises.Items.Add(lector3.GetString(0));
            }
            con.cnn.Close();

            // Busca y carga los tipos de cuenta 
            string query5 = "SELECT descripcion FROM LPP.TIPOS_CUENTA ";
            con.cnn.Open();
            SqlCommand command1 = new SqlCommand (query5, con.cnn);
            SqlDataReader lector1 = command1.ExecuteReader();;
            while (lector1.Read())
            {
                cmbTipoCuenta.Items.Add(lector1.GetString(0));
            }
            con.cnn.Close();

           if(evento!= "A")
            {

            string query2 = "SELECT C.num_cuenta,C.id_cliente, M.descripcion, TC.descripcion,P.pais,L.username " +
                            "FROM  LPP.MONEDAS M JOIN LPP.CUENTAS C  ON C.id_moneda = M.id_moneda  " +
                            "JOIN LPP.ESTADOS_CUENTA E ON C.id_estado = E.id_estadocuenta  " +
                            "JOIN LPP.TIPOS_CUENTA TC ON C.id_tipo = TC.id_tipocuenta " +
                            "JOIN LPP.PAISES P ON P.id_pais = C.id_pais " +
                            "JOIN LPP.CLIENTES L ON L.id_cliente=c.id_cliente  "+
                            "WHERE C.num_cuenta = " + cuenta + " ";
                              

                con.cnn.Open();
                SqlCommand command2 = new SqlCommand (query2, con.cnn);
                SqlDataReader lector2 = command2.ExecuteReader();;
                if (lector2.Read())
                {
                    txtCuenta.Visible = true;
                    txtCuenta.Enabled = false;
                    txtCuenta.Text = Convert.ToString(lector2.GetDecimal(0));
                    cmbMoneda.Text = lector2.GetString(2);
                    cmbTipoCuenta.Text = lector2.GetString(3);
                    cuentaCambio = lector2.GetString(3);
                    cmbPaises.Text = lector2.GetString(4);
                    usuario = lector2.GetString(5);
                    

                }
                con.cnn.Close();
                lblCuenta.Visible = true;
                gbDatosCuenta.Enabled = false;
                gbTipoCuenta.Enabled = true;
                btnNuevo.Enabled = false;
                btnLimpiar.Enabled = true;
                btnContinuar.Enabled = true;
                ban = 2;
            }
        }

        

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            gbTipoCuenta.Enabled = true;
            gbDatosCuenta.Enabled = true;
            btnLimpiar.Enabled = true;
            btnContinuar.Enabled = true;
            btnNuevo.Enabled = false;
            btnBuscar.Enabled = false;
            ban = 1;
        }

       private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (getRolUser() == "Administrador")
            {
                ABM_Cuenta.Buscar buCuenta = new ABM_Cuenta.Buscar(0, usuario);
                this.Close();
                buCuenta.Show();
            }
            else
            {
                ABM_Cuenta.Buscar bc = new ABM_Cuenta.Buscar(getIdCliente(), usuario);
                this.Close();
                bc.Show();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbPaises.SelectedItem = null;
            cmbTipoCuenta.SelectedItem = null;
            cmbMoneda.SelectedItem = null;

        }

        private void btnContinuar_Click_1(object sender, EventArgs e)
        {
            
            // Verifica que no haya campos vacios 
            if (cmbMoneda.SelectedItem == null)
            {
                MessageBox.Show("Elija un tipo de moneda");
                return;
            }

            if (cmbTipoCuenta.SelectedItem == null)
            {
                MessageBox.Show("Elija un tipo de cuenta");
                return;
            }

            if (ban == 1)
            {
                if (cmbPaises.SelectedItem == null)
                {
                    MessageBox.Show("Elija un Pais");
                    return;
                }
            }
             if (ban == 1)
             {
                 int cliente = getIdCliente();
                 decimal moneda = getIdMoneda();
                 int tipocuenta = getIdTipoCuenta(cmbTipoCuenta.Text);
                 int bandera = ban;
                 decimal pais = getIdPais(); 
                 int dur = getDuracionCuenta(getIdTipoCuenta(cmbTipoCuenta.Text));
                 ComprarSuscripcion cs = new ComprarSuscripcion(cliente, moneda, tipocuenta , 0, ban, pais, dur, false);
                 cs.Show();
                 this.Close();
             }
             if (ban == 2)
             {
                 if (cuentaCambio != cmbTipoCuenta.Text)
                 {
                     ComprarSuscripcion cs = new ComprarSuscripcion(getIdCliente(), getIdMoneda(), getIdTipoCuenta(cuentaCambio), Convert.ToDecimal(txtCuenta.Text), ban, getIdPais(), getDuracionCuenta(getIdTipoCuenta(cmbTipoCuenta.Text)), true);
                     cs.tipocuenta_final = getIdTipoCuenta(cmbTipoCuenta.Text);
                     cs.Show();
                     this.Close();
                 }
                 else
                 {
                     DialogResult dialogResult = MessageBox.Show("Su tipo de cuenta no percibio cambios. Presione YES si desea solo modificar la moneda de su cuenta. Presione NO si desea extender la suscripcion de sus cuenta", "Opciones", MessageBoxButtons.YesNo);
                     if (dialogResult == DialogResult.Yes)
                     {
                         Conexion con1 = new Conexion();
                         string query9 = "UPDATE LPP.CUENTAS SET " +
                               " id_moneda = " + getIdMoneda() +
                               " WHERE num_cuenta = " + Convert.ToDecimal(txtCuenta.Text) + "";

                         con1.cnn.Open();
                         SqlCommand command = new SqlCommand(query9, con1.cnn);
                         command.ExecuteNonQuery();
                         con1.cnn.Close();
                         MessageBox.Show("La cuenta fue modificada con éxito");

                     }
                     if (dialogResult == DialogResult.No)
                     {
                         ComprarSuscripcion cs = new ComprarSuscripcion(getIdCliente(), getIdMoneda(), getIdTipoCuenta(cmbTipoCuenta.Text), Convert.ToDecimal(txtCuenta.Text), ban, getIdPais(), getDuracionCuenta(getIdTipoCuenta(cmbTipoCuenta.Text)), false);
                         cs.Show();
                         this.Close(); 
                     }
                     
                 }

                               
             }
        }
        private decimal getIdPais()
        {
            Conexion con = new Conexion();
            //OBTENGO ID DE PAIS
            con.cnn.Open();
            string query = "SELECT id_pais FROM LPP.PAISES WHERE pais = '" + Convert.ToString(cmbPaises.SelectedItem) + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            decimal id_pais = lector.GetDecimal(0);
            con.cnn.Close();
            return id_pais;
        }
        private int getIdCliente()
        {
            Conexion con = new Conexion();
            //OBTENGO ID DE CLIENTE
            con.cnn.Open();
            string query = "SELECT id_cliente FROM LPP.CLIENTES WHERE username = '" + usuario + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            int id_cliente = lector.GetInt32(0);
            con.cnn.Close();
            return id_cliente;

        }

        private Int32 getDuracionCuenta(Int32 id_tipo) {
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "SELECT duracion FROM LPP.TIPOS_CUENTA WHERE id_tipocuenta = " + id_tipo  + "";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            Int32 duracion = lector.GetInt32(0);
            con.cnn.Close();
            return duracion;        
        }

        private decimal getIdMoneda()
        {
            Conexion con = new Conexion();
            //OBTENGO ID MONEDA
            con.cnn.Open();
            string query = "SELECT id_moneda FROM LPP.MONEDAS WHERE descripcion = '" + Convert.ToString(cmbMoneda.SelectedItem) + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            decimal id_moneda = lector.GetDecimal(0);
            con.cnn.Close();
            return id_moneda;
        }
        private int getIdTipoCuenta(string tipo)
        {
            Conexion con = new Conexion();
            //OBTENGO ID DE TIPO_CUENTA
            con.cnn.Open();

            string query = "SELECT id_tipocuenta FROM LPP.TIPOS_CUENTA WHERE descripcion = '" +tipo  + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            int id_tipoCuenta = lector.GetInt32(0);
            con.cnn.Close();
            return id_tipoCuenta;
        }
        private decimal getNumCuenta()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "SELECT TOP 1 num_cuenta,fecha_apertura FROM LPP.CUENTAS WHERE id_cliente = " + getIdCliente() + " ORDER BY fecha_apertura DESC, num_cuenta DESC";
            SqlCommand command= new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            decimal num_cuenta = lector.GetDecimal(0);
            con.cnn.Close();
            return num_cuenta;
        }

        private string getRolUser()
        {
            Conexion con = new Conexion();
            //OBTENGO ID DE CLIENTE
            con.cnn.Open();
            string query = "SELECT R.nombre FROM LPP.ROLESXUSUARIO U JOIN LPP.ROLES R ON R.id_rol=U.rol WHERE U.username = '" + usuario + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            string rol = lector.GetString(0);
            con.cnn.Close();
            return rol;
        }

                
}

}  
        
       


        

        
    
