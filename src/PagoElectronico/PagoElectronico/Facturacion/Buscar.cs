using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Helper;
using readConfiguracion;

namespace PagoElectronico.Facturacion
{
    public partial class Buscar : Form
    {
        public string user;
        public Buscar(string usuario)
        {
            InitializeComponent();
            user = usuario;
            Conexion con1 = new Conexion();
            con1.cnn.Open();
            // CARGO EL COMBO BOX ITEMS
            string query = "SELECT descripcion FROM LPP.ITEMS";
            SqlCommand command = new SqlCommand(query, con1.cnn);
            SqlDataReader lector1 = command.ExecuteReader();
            while (lector1.Read())
            {
                // Cargo la descripciones en la lista
                cbItems.Items.Add(lector1.GetString(0));
            }
            con1.cnn.Close();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            decimal id_item = getIdItem();
            if (cbItems.Text == "")
            {
                MessageBox.Show("Ingrese un tipo de item pendiente");
            }
            else {
                Facturacion formF = new Facturacion(id_item, user);
                formF.Show();
                this.Close();
            }
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private decimal getIdItem()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //OBTENGO ID ITEM
            string query = "SELECT id_item FROM LPP.ITEMS WHERE descripcion = '"+cbItems.SelectedItem.ToString()+"'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            decimal id_item = Convert.ToDecimal(command.ExecuteScalar());
            con.cnn.Close();
            return id_item;
        }

        private void btTodosPendientes_Click(object sender, EventArgs e)
        {
            Facturacion formF = new Facturacion(0, user);
            formF.Show();
            this.Close();
        }

        

      
    }
}
