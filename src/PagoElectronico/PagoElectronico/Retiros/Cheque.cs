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

namespace PagoElectronico.Retiros
{
    public partial class Cheque : Form
    {
        public decimal num_cuenta;
        public string usuario;
        public decimal importe;
        public decimal id_moneda;
        private decimal id_banco;
        private Int32 cliente_receptor;

        public Cheque(decimal cuenta, string user)
        {
            InitializeComponent();
            grpBanco.Enabled = false;
            num_cuenta = cuenta;
            usuario = user;
            
            Conexion con1 = new Conexion();

            //CARGO COMBO BOX TIPO DOCS
            con1.cnn.Open();
            
            string query = "SELECT tipo_descr FROM LPP.TIPO_DOCS";

            SqlCommand command = new SqlCommand(query, con1.cnn);
            SqlDataReader lector1 = command.ExecuteReader();
            while (lector1.Read())
            {
                // Cargo la descripciones en la lista
                cbID.Items.Add(lector1.GetString(0));
            }
            con1.cnn.Close();

            //CARGO COMBO BOX BANCOS
            con1.cnn.Open();
            
            string query2 = "SELECT DISTINCT nombre FROM LPP.BANCOS";

            SqlCommand command2 = new SqlCommand(query2, con1.cnn);
            SqlDataReader lector2 = command2.ExecuteReader();
            while (lector2.Read())
            {
                // Cargo la descripciones en la lista
                cmbBanco.Items.Add(lector2.GetString(0));
            }

            con1.cnn.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (cbID.SelectedItem == null)
            {
                MessageBox.Show("Debe elegir algun tipo de documento");
                return;
            }
            if (txtDoc.Text=="")
            {
                MessageBox.Show("Debe ingresar Numero de Documento");
                return;
            }
            decimal temp;
            try
            {
             if (txtDoc.Text != "")
                    temp = Convert.ToDecimal(txtDoc.Text);
            }
            catch (Exception h)
            {
                MessageBox.Show("Numero de Documento debe ser numerico",h.ToString());
                return;
            }
            Conexion con = new Conexion();
            //SELECCIONO ID_TIPO_DOC
            string query1 = "SELECT tipo_cod FROM LPP.TIPO_DOCS WHERE tipo_descr = '"+cbID.Text+"'";
            con.cnn.Open();
            SqlCommand command1 = new SqlCommand(query1, con.cnn);
            //SqlDataReader lector1 = command1.ExecuteReader();
            decimal tipo = Convert.ToDecimal(command1.ExecuteScalar());
            con.cnn.Close();

            //CORROBORO SI LOS DATOS INGRESADOR COINCIDEN CON EL USUARIO LOGUEADO
            string query2 = "SELECT TOP 1 U.username FROM LPP.USUARIOS U "
                            +" JOIN LPP.CLIENTES C ON U.username = '"+usuario+"' " 
                            +" WHERE C.num_doc = "+Convert.ToDecimal(txtDoc.Text)+" AND C.id_tipo_doc = "+tipo+"";
            con.cnn.Open();
            SqlCommand command2 = new SqlCommand(query2, con.cnn);
            SqlDataReader lector2 = command2.ExecuteReader();

            if (lector2.Read())
            {
                MessageBox.Show("Datos correctos, elija el Banco al cual pertenece el Cheque por favor.");
                grpBanco.Enabled = true;
            }
            else
            {
                MessageBox.Show("Datos incorrectos, no ingreso los datos del cliente que esta logueado.");
                return;
            }
            con.cnn.Close();


        }

        private void btnBanco_Click(object sender, EventArgs e)
        {
            if (cmbBanco.SelectedItem == null)
            {
                MessageBox.Show("Elija un Banco por favor");
                return;
            }
            else
            {
                this.cargarDatosDeCheque();
                RetiroDeEfectivo re = new RetiroDeEfectivo(usuario);
                re.id_moneda = id_moneda;
                re.importe = importe;
                re.num_cuenta = num_cuenta;
                re.GuardarDatos(cliente_receptor, id_banco);
                this.Close();
            }
        }

        public void cargarDatosDeCheque() {
            Conexion con = new Conexion();
            //OBTENGO ID DE BANCO
            string banco = cmbBanco.Text;
            string query5 = "SELECT id_banco FROM LPP.BANCOS WHERE nombre = '" + banco + "'";
            con.cnn.Open();
            SqlCommand command5 = new SqlCommand(query5, con.cnn);
            id_banco = Convert.ToDecimal(command5.ExecuteScalar());
            con.cnn.Close();

            //OBTENGO CLIENTE_RECEPTOR
            string query4 = "SELECT id_cliente FROM LPP.CLIENTES WHERE username = '" + usuario + "' ";
            con.cnn.Open();
            SqlCommand command4 = new SqlCommand(query4, con.cnn);
            cliente_receptor = Convert.ToInt32(command4.ExecuteScalar());
            con.cnn.Close();
        }

        private void btLimpiar_Click(object sender, EventArgs e)
        {
            cbID.SelectedItem = null;
            cmbBanco.SelectedItem = null;
            txtDoc.Text = " ";
        }

        

       
    }
}
