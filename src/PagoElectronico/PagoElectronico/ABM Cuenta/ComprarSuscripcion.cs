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
    public partial class ComprarSuscripcion : Form
    {

        private int id_cliente;
        private decimal num_cuenta;
        private decimal id_tipocuenta;
        private decimal id_moneda;
        private int ban;
        private decimal id_pais;
        private int duracion;
        private bool cuentaCambio;
        public decimal tipocuenta_final;

        public ComprarSuscripcion(int cliente, decimal moneda, int tipocuenta, decimal cuenta, int bandera, decimal pais, int dur, bool cambio)
        {
            InitializeComponent();
            id_cliente = cliente;
            id_moneda = moneda;
            id_tipocuenta = tipocuenta;
            num_cuenta = cuenta;
            ban = bandera;
            id_pais = pais;
            duracion = dur;
            cuentaCambio = cambio;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion con1 = new Conexion();
            if (ban == 1)
            {
                //INSERTO LA CUENTA
                string query1 = "INSERT INTO LPP.CUENTAS (id_pais, " +
                                "id_cliente, id_moneda, fecha_apertura, id_tipo,saldo,id_estado) VALUES " +
                                "(" + id_pais + "," + id_cliente + "," + id_moneda + ",CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103)," + id_tipocuenta + ",0,2)";
                con1.cnn.Open();
                SqlCommand command = new SqlCommand(query1, con1.cnn);
                command.ExecuteNonQuery();
                con1.cnn.Close();

                num_cuenta = getNumCuenta();
                string query2 = "INSERT INTO LPP.SUSCRIPCIONES (num_cuenta, fecha_vencimiento)"
                               + " VALUES (" + num_cuenta + ", DATEADD(day," + duracion * Convert.ToInt32(numericUpDown1.Value) + " ,  CONVERT(DATETIME, '" + readConfiguracion.Configuracion.fechaSystem() + "', 103 )))";
                con1.cnn.Open();
                SqlCommand command2 = new SqlCommand(query2, con1.cnn);
                command2.ExecuteNonQuery();
                con1.cnn.Close();

                this.insertarItemFacturaPorApertura();

                MessageBox.Show("Alta de Cuenta Exitosa, su Numero de cuenta es:  " + getNumCuenta());
                

                this.Close();
            }
            if (ban == 2)
            {
                if (cuentaCambio)
                {
                    this.updateMoneda();
                    this.insertarCambio_Cuenta();
                }
                else
                {
                    this.insertarItem_extensionCuenta();
                }

                MessageBox.Show("La cuenta fue modificada con éxito");
                this.Close();

            }


    
       }


        private void updateMoneda() {
            Conexion con1 = new Conexion();
            string query9 = "UPDATE LPP.CUENTAS SET " +
                                  " id_moneda = " + id_moneda +
                                  " WHERE num_cuenta = " + num_cuenta + "";

            con1.cnn.Open();
            SqlCommand command = new SqlCommand(query9, con1.cnn);
            command.ExecuteNonQuery();
            con1.cnn.Close();
        }

        private void insertarCambio_Cuenta()
        {
            Conexion con = new Conexion();

            con.cnn.Open();
            string query = "LPP.PRC_CambioCuenta";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_cuenta", num_cuenta));
            command.Parameters.Add(new SqlParameter("@tipocuenta_origen", id_tipocuenta));
            command.Parameters.Add(new SqlParameter("@tipocuenta_final", tipocuenta_final));
            DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
            command.Parameters.Add(new SqlParameter("@fecha", fechaConfiguracion));
            command.Parameters.Add(new SqlParameter("@cantsuscripciones", Convert.ToInt32(numericUpDown1.Value)));
            command.ExecuteNonQuery();
            con.cnn.Close();
            
        }
        private void insertarItem_extensionCuenta() {

            Conexion con = new Conexion();

            con.cnn.Open();
            string query = "LPP.PRC_ItemFactura_x_ExtensionCuenta";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_cuenta", num_cuenta));
            command.Parameters.Add(new SqlParameter("@id_tipo", id_tipocuenta));
            DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
            command.Parameters.Add(new SqlParameter("@fecha", fechaConfiguracion));
            command.Parameters.Add(new SqlParameter("@cantsuscripciones", Convert.ToInt32(numericUpDown1.Value)));
            command.ExecuteNonQuery();
            con.cnn.Close();
        }

        private void insertarItemFacturaPorApertura()
        {
            Conexion con = new Conexion();

            con.cnn.Open();
            string query = "LPP.PRC_ItemFactura_x_AperturaCuenta";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_cuenta", num_cuenta));
            command.Parameters.Add(new SqlParameter("@id_tipo", id_tipocuenta));
            DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
            command.Parameters.Add(new SqlParameter("@fecha", fechaConfiguracion));
            command.Parameters.Add(new SqlParameter("@cantsuscripciones", Convert.ToInt32(numericUpDown1.Value)));
            command.ExecuteNonQuery();
            con.cnn.Close();
        }

        private decimal getNumCuenta()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "SELECT TOP 1 num_cuenta,fecha_apertura FROM LPP.CUENTAS WHERE id_cliente = " + id_cliente + " ORDER BY fecha_apertura DESC, num_cuenta DESC";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            decimal num_cuenta = lector.GetDecimal(0);
            con.cnn.Close();
            return num_cuenta;
        }
    
    }
}
