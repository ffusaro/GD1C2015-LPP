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
    public partial class Pregunta : Form
    {
        private string usuario;
        private decimal num_cuenta;
        public Pregunta(string user,decimal numCuenta)
        {
            InitializeComponent();
            usuario = user;
            num_cuenta = numCuenta;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            string salida = "La cuenta fue cerrada correctamente";
            if (verificoSiDebe())
            {
                MessageBox.Show("Tiene items sin facturar","ERROR");
                return;
            }
            else
            {
                try
                {  //CIERRO LA CUENTA
                    con.cnn.Open();
                    string query = "UPDATE LPP.CUENTAS SET id_estado = 3, fecha_cierre = CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103) WHERE num_cuenta = " + num_cuenta + "";
                    SqlCommand command = new SqlCommand(query, con.cnn);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    salida = "No se pudo cerrar la Cuenta" + ex.ToString();
                }
                con.cnn.Close();
                this.Close();
                MessageBox.Show("" + salida);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ABM_Cuenta.AltaCuenta fc = new ABM_Cuenta.AltaCuenta("M", usuario, num_cuenta);
            fc.Show();
            this.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int getIdCliente()
        {
            Conexion con = new Conexion();
            //OBTENGO ID DE CLIENTE
            con.cnn.Open();
            string query = "SELECT id_cliente FROM LPP.CUENTAS WHERE num_cuenta = " + num_cuenta + "";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            int id_cliente = lector.GetInt32(0);
            con.cnn.Close();
            return id_cliente;

        }
        private bool verificoSiDebe()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            bool debe;
            string query = "SELECT COUNT(*) FROM LPP.ITEMS_FACTURA WHERE num_cuenta ="+num_cuenta
                           + " AND facturado = 0";
            SqlCommand command = new SqlCommand(query, con.cnn);
            Int32 impagos = Convert.ToInt32(command.ExecuteScalar());
            con.cnn.Close();
            if (impagos != 0 )
            {
                MessageBox.Show("Tiene items sin facturar");
                debe = true;
            }
            else{
                debe = false;
            }
            return debe;
        }
    }
}
