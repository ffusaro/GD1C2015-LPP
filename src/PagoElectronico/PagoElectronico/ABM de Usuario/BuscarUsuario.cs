using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_de_Usuario
{
    public partial class BuscarUsuario : Form
         
    {
        ABMUsuario FormUsuario;
        ABMCliente FormCliente;
        public MenuPrincipal mp;
        public DataTable dt;
        public int ev;
        
        public BuscarUsuario(int evento)
        {
            InitializeComponent();
            ev = evento;
            btnBusca.Enabled = false;
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
            txtUsuarios.Text = "";
            txtName.Text = "";
            txtLname.Text = "";
            btnBusca.Enabled = false;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            Conexion con = new Conexion();
           

            string query = "SELECT nombre,apellido,U.username  FROM LPP.USUARIOS U JOIN LPP.CLIENTES C ON ( U.username = C.username) WHERE 1=1";

            if (txtName.Text != "")
            {
                query += "AND nombre LIKE '%" + txtName.Text + "%'";
            }

            if (txtLname.Text != "")
            {
                query += "AND apellido LIKE '%" + txtLname.Text + "%'";
            }

            if (txtUsuarios.Text != "")
            {
                query += "AND U.username LIKE '%" + txtUsuarios.Text + "%'";
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

        private void txtUsuarios_TextChanged(object sender, EventArgs e)
        {
            btnBusca.Enabled = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnBusca.Enabled = true;
        }

        private void txtLname_TextChanged(object sender, EventArgs e)
        {
            btnBusca.Enabled = true;
        }

       

       

        
    }
}
