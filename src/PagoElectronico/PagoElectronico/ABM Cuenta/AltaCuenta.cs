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
            Conexion con1 = new Conexion();
            // Verifica que no haya campos vacios 
            if (cmbMoneda.SelectedItem == null)
            {
                MessageBox.Show("Elija un tipo de moneda");
                return;
            }

            if (cmbTipoCuenta.SelectedItem == null)
            {
                MessageBox.Show("Elija un tipo de cuenta");
            }
            
             if (cmbPaises.SelectedItem == null)
            {
                MessageBox.Show("Elija un Pais");
            }

             if (ban == 1)
             {
                 //INSERTO LA CUENTA
                 string query1 = "INSERT INTO LPP.CUENTAS (id_pais, " +
                                 "id_cliente, id_moneda, fecha_apertura, id_tipo,saldo,id_estado) VALUES " +
                                 "(" + getIdPais() + "," + getIdCliente() + "," + getIdMoneda() + ",CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103)," + getIdTipoCuenta(cmbTipoCuenta.Text) + ",0,2)";
                 con1.cnn.Open();
                 SqlCommand command = new SqlCommand(query1, con1.cnn);
                 command.ExecuteNonQuery();
                 con1.cnn.Close();

                 Int32 duracion = getDuracionCuenta(getIdTipoCuenta(cmbTipoCuenta.Text));
                 string query2 = "INSERT INTO LPP.SUSCRIPCIONES (num_cuenta, fecha_vencimiento)"
                                + " VALUES (" + getNumCuenta() + ", DATEADD(day," + duracion * Convert.ToInt32(numericUpDown1.Value) + " ,  CONVERT(DATETIME, '" + readConfiguracion.Configuracion.fechaSystem() + "', 103 )))";
                 con1.cnn.Open();
                 SqlCommand command2 = new SqlCommand(query2, con1.cnn);
                 command2.ExecuteNonQuery();
                 con1.cnn.Close();
                 
                 this.insertarItemFacturaPorApertura();

                 MessageBox.Show("Alta de Cuenta Exitosa, su Numero de cuenta es:  "+getNumCuenta());
                 btnLimpiar.Enabled = true;
                 btnContinuar.Enabled = false;
                 btnBuscar.Enabled = true;
                 btnNuevo.Enabled = true;
                 btnSalir.Enabled = true;
                 gbDatosCuenta.Enabled = false;
                 gbTipoCuenta.Enabled = false;
                 cmbPaises.SelectedItem = null;
                 cmbTipoCuenta.SelectedItem = null;
                 cmbMoneda.SelectedItem = null;
                 numericUpDown1.Enabled = false;
                 
                 this.Close();
             }
             if (ban == 2)
             {
                 string query9 = "UPDATE LPP.CUENTAS SET " +
                                 " id_moneda = " + getIdMoneda() + ","+
                                 " id_tipo = " +getIdTipoCuenta(cmbTipoCuenta.Text) + "  " +
                                 "WHERE num_cuenta = " + Convert.ToDecimal(txtCuenta.Text)+ "";
                
                 con1.cnn.Open();
                 SqlCommand command = new SqlCommand(query9, con1.cnn);
                 command.ExecuteNonQuery();
                 con1.cnn.Close();
                 if (cuentaCambio != cmbTipoCuenta.Text)
                     insertarCambio_Cuenta();
                 MessageBox.Show("La cuenta fue modificada con éxito");
                 btnLimpiar.Enabled = true;
                 btnContinuar.Enabled = false;
                 btnBuscar.Enabled = true;
                 btnNuevo.Enabled = true;
                 btnSalir.Enabled = true;
                 gbDatosCuenta.Enabled = false;
                 gbTipoCuenta.Enabled = false;
                 cmbPaises.SelectedItem = null;
                 cmbTipoCuenta.SelectedItem = null;
                 cmbMoneda.SelectedItem = null;
                 numericUpDown1.Enabled = false;
                
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
        
       
        private void insertarCambio_Cuenta()
        {
            Conexion con = new Conexion();

            con.cnn.Open();
            string query = "LPP.PRC_CambioCuenta";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_cuenta", num_cuenta));
            command.Parameters.Add(new SqlParameter("@tipocuenta_origen",getIdTipoCuenta(cuentaCambio)));
            command.Parameters.Add(new SqlParameter("@tipocuenta_final", getIdTipoCuenta(cmbTipoCuenta.Text)));
            DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
            command.Parameters.Add(new SqlParameter("@fecha", fechaConfiguracion));
            command.Parameters.Add(new SqlParameter("@cantsuscripciones", Convert.ToInt32(numericUpDown1.Value)));
            command.ExecuteNonQuery();
            con.cnn.Close();
            if (Helper.Help.VerificadorDeDeudas(getIdCliente()))
                MessageBox.Show("Al tener mas de 5 transacciones sin facturar su cuenta se encuentra inhabilitada");
         }

        private void insertarItemFacturaPorApertura() 
        {
            Conexion con = new Conexion();

            con.cnn.Open();
            string query = "LPP.PRC_ItemFactura_x_AperturaCuenta";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_cuenta", getNumCuenta()));
            command.Parameters.Add(new SqlParameter("@id_tipo", getIdTipoCuenta(cmbTipoCuenta.Text)));
            DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
            command.Parameters.Add(new SqlParameter("@fecha", fechaConfiguracion));
            command.Parameters.Add(new SqlParameter("@cantsuscripciones", Convert.ToInt32(numericUpDown1.Value)));
            command.ExecuteNonQuery();
            con.cnn.Close();
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
        
       


        

        
    
