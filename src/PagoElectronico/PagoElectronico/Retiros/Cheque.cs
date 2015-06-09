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
using PagoElectronico;
using Helper;
using readConfiguracion;

namespace PagoElectronico.Retiros
{
    public partial class Cheque : Form
    {
        public int num_cuenta;
        public string usuario;
        public Cheque(int cuenta, string user)
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

            //CARGO COMBO BOX BANCOS
            con1.cnn.Close();
            con1.cnn.Open();
            
            string query2 = "SELECT nombre FROM LPP.BANCOS";

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
            if (cmbTipo.SelectedItem == null)
            {
                MessageBox.Show("Debe elegir algun tipo de documento");
                return;
            }
            if (txtDNI.Text=="")
            {
                MessageBox.Show("Debe ingresar Numero de Documento");
                return;
            }
            int temp;
            try
            {
             if (txtDoc.Text != "")
                    temp = Convert.ToInt32(txtDoc.Text);
            }
            catch (Exception h)
            {
                MessageBox.Show("Numero de Documento debe ser numerico");
                return;
            }
            Conexion con = new Conexion();
            //SELECCIONO ID_TIPO_DOC
            string query1 = "SELECT tipo_cod FROM LPP.TIPO_DOCS WHERE tipo_descr = '"+cbID.Text+"'";
            con.cnn.Open();
            SqlCommand command1 = new SqlCommand(query1, con.cnn);
            SqlDataReader lector1 = command1.ExecuteReader();
            int tipo = lector1.GetInt32(0);
            con.cnn.Close();

            //CORROBORO SI LOS DATOS INGRESADOR COINCIDEN CON EL USUARIO LOGUEADO
            string query2 = "SELECT 1 FROM LPP.USUARIOS U JOIN LPP.CLIENTES C ON U.username = '"+usuario+"' " +
                             "WHERE C.num_doc = "+Convert.ToInt32(txtDoc.Text)+" AND C.id_tipo_doc = "+tipo+"";
            con.cnn.Open();
            SqlCommand command2 = new SqlCommand(query2, con.cnn);
            SqlDataReader lector2 = command2.ExecuteReader();

            if (lector2.Read())
            {
                MessageBox.Show("Datos correctos, elija el Banco al cual pertenece el Cheque por favor");
                grpBanco.Enabled = true;
            }
            else
            {
                MessageBox.Show("Tipo Documento y/o Numero de Documento incorrecto/s");
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
                string banco = cmbBanco.Text;
                RetiroDeEfectivo re = new RetiroDeEfectivo(usuario);
                re.GuardarDatos(banco);
                this.Close();
            }
        }

        

       
    }
}
