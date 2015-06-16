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
using readConfiguracion;

namespace PagoElectronico.Tarjetas
{
    public partial class abmTarjetas : Form
    {
        private string usuario;
        private DateTime fecha = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
        private int ban;
        public abmTarjetas(string user, string numTarjeta)
        {
            InitializeComponent();
            usuario = user;
            grpDatos.Enabled = false;
            btnBuscar.Enabled = true;
            btnDesasociar.Enabled = false;
            btnGrabar.Enabled = false;
            btnLimpiar.Enabled = false;
            btnModificar.Enabled = false;
            btnSalir.Enabled = true;

            //CARGA DE EMISORES
            Conexion con = new Conexion();
            string query = "SELECT emisor_descr FROM LPP.EMISORES";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            while (lector.Read())
            {
                cmbEmisor.Items.Add(lector.GetString(0));
            }
            con.cnn.Close();

            if (numTarjeta != "N")
            {
                //SE VA A MODIFICAR O DESASOCIAR UNA TARJETA
                cargarDatos(numTarjeta);
            }
            else
            {
                dtpEmision.Value = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
                dtpVencimiento.Value = dtpEmision.Value;
                dtpVencimiento.MinDate = dtpEmision.Value;
            }
            
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtNumTarjeta.Text = "";
            cmbEmisor.SelectedItem = null;
            dtpEmision.Value = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
            dtpVencimiento.Value = dtpEmision.Value;


        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            ban = 2;
            txtNumTarjeta.Focus();
            btnNuevo.Enabled = false;
            btnGrabar.Enabled = true;
            btnModificar.Enabled = false;
            btnBuscar.Enabled = false;
            grpDatos.Enabled = true;
            btnDesasociar.Enabled = false;
        }

        private void btnDesasociar_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "PRC_desasociar_tarjeta";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_tarjeta", txtNumTarjeta.Text));
            command.Parameters.Add(new SqlParameter("@id_cliente", getIdCliente()));
            command.ExecuteNonQuery();
            con.cnn.Close();
            MessageBox.Show("Se desasoció la tarjeta correctamente");
            btnDesasociar.Enabled = false;
            btnGrabar.Enabled = false;
            btnModificar.Enabled = false;
            txtCodigo.Text = "";
            txtNumTarjeta.Text = "";
            cmbEmisor.SelectedItem = null;
            dtpEmision.Value = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
            dtpVencimiento.Value = dtpEmision.Value;
            btnNuevo.Enabled = true;
            grpDatos.Enabled = false;
            btnBuscar.Enabled = true;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtNumTarjeta.Text == "")
            {
                MessageBox.Show("Ingrese un Numero de Tarjeta");
                return;
            }
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Ingrese un Codigo de Seguridad");
                return;
            }
            if (cmbEmisor.SelectedItem == null)
            {
                MessageBox.Show("Elija un Emisor");
                return;
            }
            if (!((DateTime.Compare(dtpEmision.Value, dtpVencimiento.Value)) < 0))
            {

                MessageBox.Show("La fecha de emision debe ser menor a la de vencimiento");
                return;
            }
            
            if (!((DateTime.Compare(dtpEmision.Value,fecha)) < 0))
            {

                MessageBox.Show("La fecha de emision debe ser menor a "+fecha);
                return;
            }
          
            
                if (ban == 1)
                {
                    if (checkTarjetas())
                   {    grabarDatos();
                        txtCodigo.Text = "";
                        txtNumTarjeta.Text = "";
                        cmbEmisor.SelectedItem = null;
                        dtpEmision.Value = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
                        dtpVencimiento.Value = dtpEmision.Value;
                        btnGrabar.Enabled = false;
                        btnNuevo.Enabled = true;
                        grpDatos.Enabled = false;
                        btnBuscar.Enabled = true;
                        MessageBox.Show("Se asocio correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Numero de tarjeta invalido");
                        return;
                    }
                }
                else
                {
                    modificarDatos();
                    MessageBox.Show("Se modificaron correctamene los datos");
                    txtCodigo.Text = "";
                    txtNumTarjeta.Text = "";
                    cmbEmisor.SelectedItem = null;
                    dtpEmision.Value = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
                    dtpVencimiento.Value = dtpEmision.Value;
                    btnGrabar.Enabled = false;
                    btnNuevo.Enabled = true;
                    grpDatos.Enabled = false;
                    btnBuscar.Enabled = true;
                    
                }
            
           
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ban = 1;
            grpDatos.Enabled = true;
            btnLimpiar.Enabled = true;
            btnGrabar.Enabled = true;
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnDesasociar.Enabled = false;
            btnBuscar.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            Tarjetas.BuscarTarjetas bt = new Tarjetas.BuscarTarjetas(usuario);
            bt.Show();
            this.Close();
        }
        private void grabarDatos()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "PRC_insertar_nueva_tarjeta";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_tarjeta", txtNumTarjeta.Text));
            command.Parameters.Add(new SqlParameter("@id_emisor", getIdEmisor()));
            command.Parameters.Add(new SqlParameter("@cod_seguridad", txtCodigo.Text));
            command.Parameters.Add(new SqlParameter("@id_cliente", getIdCliente()));
            DateTime fechaEmision = DateTime.Parse(dtpEmision.Value.ToString("yyyy-MM-dd"));
            DateTime fechaVencimiento = DateTime.Parse(dtpVencimiento.Value.ToString("yyyy-MM-dd"));
            command.Parameters.Add(new SqlParameter("@fecha_emision", fechaEmision));
            command.Parameters.Add(new SqlParameter("@fecha_vencimiento", fechaVencimiento));
            command.ExecuteNonQuery();
            con.cnn.Close();
        }
        private decimal getIdEmisor()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //OBTENGO ID EMISOR
            string query = "SELECT id_emisor FROM LPP.EMISORES WHERE emisor_descr = '" + Convert.ToString(cmbEmisor.SelectedItem) + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            decimal id_emisor = lector.GetDecimal(0);
            con.cnn.Close();
            return id_emisor;
        }
        private int getIdCliente()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //OBTENGO ID CLIENTE
            string query = "SELECT id_cliente FROM LPP.CLIENTES WHERE username = '" + usuario + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            int id_cliente = lector.GetInt32(0);
            con.cnn.Close();
            return id_cliente;
        }
        private bool checkTarjetas()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //CHEKEO SI EL NUM DE TARJETA YA EXISTE
            string query = "SELECT 1 FROM LPP.TARJETAS WHERE num_tarjeta = '" + txtNumTarjeta.Text + "' AND id_emisor = "+getIdEmisor()+"";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            if (lector.Read())
            {
                con.cnn.Close();
                return false;
            }
            else
            {
                con.cnn.Close();
                return true;
            }

        }
        private void cargarDatos(string num_tarjeta)
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //CARGO LOS DATOS DE LA TARJETA
            string query = "SELECT num_tarjeta,id_emisor,cod_seguridad,fecha_emision,fecha_vencimiento FROM LPP.TARJETAS WHERE num_tarjeta = '" + num_tarjeta + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            txtNumTarjeta.Text = lector.GetString(0);
            cmbEmisor.Text = getDescrEmisor(lector.GetDecimal(1));
            cmbEmisor.Enabled = false;
            txtCodigo.Text = lector.GetString(2);
            dtpEmision.Value = lector.GetDateTime(3);
            dtpVencimiento.Value = lector.GetDateTime(4);
            con.cnn.Close();
            btnModificar.Enabled = true;
            btnDesasociar.Enabled = true;
        }
        private string getDescrEmisor(decimal id)
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //OBTENGO ID EMISOR
            string query = "SELECT emisor_descr FROM LPP.EMISORES WHERE id_emisor = '" +id  + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            string emisor = lector.GetString(0);
            con.cnn.Close();
            return emisor;
        }
        private void modificarDatos()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "PRC_modificar_tarjeta";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_tarjeta", txtNumTarjeta.Text));
            //command.Parameters.Add(new SqlParameter("@id_emisor", getIdEmisor()));
            command.Parameters.Add(new SqlParameter("@cod_seguridad", txtCodigo.Text));
            DateTime fechaEmision = DateTime.Parse(dtpEmision.Value.ToString("yyyy-MM-dd"));
            DateTime fechaVencimiento = DateTime.Parse(dtpVencimiento.Value.ToString("yyyy-MM-dd"));
            command.Parameters.Add(new SqlParameter("@fecha_emision", fechaEmision));
            command.Parameters.Add(new SqlParameter("@fecha_vencimiento", fechaVencimiento));
            command.ExecuteNonQuery();
            con.cnn.Close();
        }
       
    }
}
