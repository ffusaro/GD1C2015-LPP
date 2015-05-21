using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico
{
    public partial class BuscarUsuario : Form
         
    {
        ABM_de_Usuario.ABMUsuario FormUsuario;
        ABM_de_Cliente.ABMCliente FormCliente;
        public MenuPrincipal mp;
        public DataTable dt;
        public int ev;
        
        public BuscarUsuario(int evento)
        {
            InitializeComponent();
            ev = evento;
            if (ev==1)
            {
                label6.Text = "Doble click en el Usuario que quiera Modificar/Eliminar";
            
            }
            else{

                label6.Text = "Doble click en el Usuario que quiera Asociar";
            }
         }

       
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            btnBuscar.Enabled = false;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            Conexion con = new Conexion();
           

            string query = "SELECT nombre,apellido,username FROM LPP.USUARIOS U JOIN LPP.CLIENTES C ON ( U.username = C.username) WHERE 1=1";

            if (textBox2.Text != "")
            {
                query += "AND nombre LIKE '%" + textBox2.Text + "%'";
            }

            if (textBox3.Text != "")
            {
                query += "AND apellido LIKE '%" + textBox3.Text + "%'";
            }

            if (textBox1.Text != "")
            {
                query += "AND username LIKE '%" + textBox1.Text + "%'";
            }

            con.cnn.Open();
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dgvUsuario.DataSource = dtDatos;
            con.cnn.Close();
       }

        private void dgvUsuario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            string Usuario = dgvUsuario.Rows[indice].Cells["Usuario"].Value.ToString();

            if (ev == 1)
            {
                FormUsuario = new ABMUsuario(Usuario, "M_E");
                FormUsuario.Show();
                FormUsuario.padre_buscarUsuario = this;
                this.Close();
            }
            else
            {
                
                FormCliente = new ABMCliente("A",Usuario);
                FormCliente.Show();
                this.Close();
            
            
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BuscarUsuario_Load(object sender, EventArgs e)
        {

        }

       

       

        
    }
}
