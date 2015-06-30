using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PagoElectronico.Tarjetas
{
    public partial class BuscarTarjetas : Form
    {
        public DataTable dt;
        public Tarjetas.abmTarjetas abmt;
        private string usuario;
        private string numTarjeta;

        public BuscarTarjetas(string user)
        {
            InitializeComponent();
            usuario = user;

            dgvTarjetas.AllowUserToAddRows = false;
            dgvTarjetas.AllowUserToDeleteRows = false;
            dgvTarjetas.ReadOnly = true;

            //CARGO DGV CON LAS TARJETAS ASOCIADAS AL CLIENTE ASOCIADO AL USUARIO
            Conexion con = new Conexion();
            string query = " SELECT C.apellido +', '+ C.nombre as 'Apellido, Nombre', T.num_tarjeta,E.emisor_descr,T.fecha_emision,T.fecha_vencimiento  "+
                            " FROM LPP.CLIENTES C JOIN LPP.TARJETAS T ON T.id_cliente=C.id_cliente  "+
					                             "JOIN LPP.EMISORES E ON E.id_emisor = T.id_emisor  "+
                             "WHERE C.username = '"+usuario+"'";
            con.cnn.Open();
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dt = dtDatos;
            dgvTarjetas.DataSource = dtDatos;
            con.cnn.Close();

            foreach (DataGridViewRow row in dgvTarjetas.Rows)
            {
                //string ultimosCuatro = lector.GetString(4);
                string ultimosCuatro = (row.Cells["num_tarjeta"].Value).ToString();
                row.Cells["num_tarjeta"].Value = "XXXX-XXXX-XXXX-" + ultimosCuatro.Remove(0, 12);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTarjetas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int indice = e.RowIndex;
            string num_tarjeta = getNumTarjeta(indice);
            
            abmt = new Tarjetas.abmTarjetas(usuario,num_tarjeta);
            abmt.txtNumTarjeta.Enabled = false;
            abmt.Show();
            this.Close();
        }

        private string getNumTarjeta(int indice) {
            Conexion con = new Conexion();
            string ultimosCuatro = dgvTarjetas.Rows[indice].Cells["num_tarjeta"].Value.ToString().Remove(0, 15);
            string emisor = dgvTarjetas.Rows[indice].Cells["emisor_descr"].Value.ToString();

            string query = " SELECT t.num_tarjeta" +
                            " FROM LPP.TARJETAS t JOIN LPP.CLIENTES c ON c.id_cliente = t.id_cliente JOIN LPP.EMISORES e ON e.id_emisor = t.id_emisor" +
                             " WHERE c.username = '" + usuario + "' AND t.num_tarjeta LIKE '%"+ultimosCuatro+"' AND e.emisor_descr = '"+emisor+"'";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            string num_tarjeta = (command.ExecuteScalar()).ToString();
            con.cnn.Close();
            return num_tarjeta;
       
        }
        
    }
}
