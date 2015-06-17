using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PagoElectronico.ABM_Cuenta
{
    public partial class Buscar : Form
    {
       
        public ABM_Cuenta.AltaCuenta cuenta;
        private int id_cliente;
        public string usuario;
        public int idcliente;
        public Buscar(int cliente, string user)
        {
            InitializeComponent();
            usuario = user;


            if (getRolUser() == "Administrador")
            {
                Conexion con = new Conexion();
                string query = "SELECT L.username,C.id_cliente,C.num_cuenta,C.saldo,T.descripcion as TipoCuenta,E.descripcion as EstadoCuenta,C.fecha_apertura " +
                            "FROM LPP.TIPOS_CUENTA T JOIN LPP.CUENTAS C ON C.id_tipo=T.id_tipocuenta " +
                                                    "JOIN LPP.ESTADOS_CUENTA E  ON E.id_estadocuenta=C.id_estado " +
                                                    "JOIN LPP.CLIENTES L ON L.id_cliente=c.id_cliente  "+
                            "WHERE E.descripcion <> 'Cerrada'";
                con.cnn.Open();
                DataTable dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
                da.Fill(dtDatos);
                //dt = dtDatos;
                dgvCuentas.DataSource = dtDatos;
                con.cnn.Close();
            }
            else
            {
                MessageBox.Show(""+usuario);
                if (cliente == 0)
                    id_cliente = getIdCliente();
                else
                    id_cliente = cliente;

                Conexion con = new Conexion();
                string query = "SELECT C.id_cliente,C.num_cuenta,C.saldo,T.descripcion as TipoCuenta,E.descripcion as EstadoCuenta,C.fecha_apertura " +
                            "FROM LPP.TIPOS_CUENTA T JOIN LPP.CUENTAS C ON C.id_tipo=T.id_tipocuenta " +
                                                    "JOIN LPP.ESTADOS_CUENTA E  ON E.id_estadocuenta=C.id_estado " +
                            "WHERE C.id_cliente = " + id_cliente + " AND E.descripcion <> 'Cerrada'";
                con.cnn.Open();
                DataTable dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
                da.Fill(dtDatos);
                //dt = dtDatos;
                dgvCuentas.DataSource = dtDatos;
                con.cnn.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void dgvCuentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            if (getRolUser() == "Administrador")
            {
                string username = dgvCuentas.Rows[indice].Cells["username"].Value.ToString();
                decimal Cuenta = Convert.ToDecimal(dgvCuentas.Rows[indice].Cells["num_cuenta"].Value.ToString());
                ABM_Cuenta.Pregunta p = new ABM_Cuenta.Pregunta(usuario, Cuenta);
                p.Show();
                this.Close();
            }
            else
            {
                decimal Cuenta = Convert.ToDecimal(dgvCuentas.Rows[indice].Cells["num_cuenta"].Value.ToString());
                ABM_Cuenta.Pregunta p = new ABM_Cuenta.Pregunta(usuario, Cuenta);
                p.Show();
                this.Close();

            }
            
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
