using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PagoElectronico.ABM_Cliente
{
    public partial class BuscarCliente : Form
    {
        public ABMCliente _formcliente;
        public MenuPrincipal padre_mp;
        public DataTable dt;
        public int nroDoc;
        public string tipoDoc;
        
        public BuscarCliente()
        {
            InitializeComponent();
           
            // Conectar a DB
            Conexion con1 = new Conexion();
            con1.cnn.Open();

            // Pregunto todos los TipoDoc de la DB
            string query = "SELECT tipo_descr FROM LPP.TIPO_DOCS";

            SqlCommand command = new SqlCommand(query, con1.cnn);
            SqlDataReader lector = command.ExecuteReader();
            while (lector.Read())
            {
                // Los cargo en la lista
                cbTipo.Items.Add(lector.GetString(0));
            }

            con1.cnn.Close();
        }

       

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtMail.Text = "";
            txtNumeroID.Text = "";
            cbTipo.Text="Elija una opcion";
            txtNombre.Focus();
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            Conexion con = new Conexion();
            

            //HACER CONSULTA
            //string query = "SELECT id_cliente, nombre, apellido, mail, id_tipo_doc, num_doc FROM LPP.CLIENTES WHERE ";
            string query = String.Format("SELECT nombre, apellido, d.tipo_descr, num_doc, " + 
                                " p.pais, fecha_nac,id_domicilio, mail "+
                                " FROM LPP.CLIENTES cl LEFT JOIN LPP.PAISES p ON cl.id_pais=p.id_pais "+
                                " LEFT JOIN LPP.TIPO_DOCS d ON cl.id_tipo_doc = d.tipo_cod WHERE habilitado = 1");
            // Cargo todos los Clientes en el DATAGRIDVIEW

            if (txtNombre.Text != "")
            {
                query += "AND nombre LIKE '%" + txtNombre.Text + "%'";
            }
            if (txtApellido.Text != "")
            {
                query += " AND apellido LIKE '%" + txtApellido.Text + "%'";
            }
            if (cbTipo.Text != "Elija una opcion")
            {
                query += " AND id_tipo_doc = (select tipo_cod from LPP.TIPO_DOCS where tipo_descr = '" + cbTipo.Text + "')";
            }
            if (txtNumeroID.Text != "")
            {
                query += " AND num_doc = " + txtNumeroID.Text + "";
            }
            if (txtMail.Text != "")
            {
                query += " AND mail LIKE '%" + txtMail.Text + "%'";
            }

            con.cnn.Open();
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dt = dtDatos;
            
            dgvCliente.DataSource = dtDatos;
            con.cnn.Close();



        }

        private void dgvCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int id;
            int indice = e.RowIndex;

           
                string mail = dgvCliente.Rows[indice].Cells["mail"].Value.ToString();
                _formcliente = new ABMCliente(mail,"U");
                _formcliente.Show();
                _formcliente.padre_buscar = this;
                this.Close();
            
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BuscarCliente_Load(object sender, EventArgs e)
        {

        }

      

      


      
    }
}
