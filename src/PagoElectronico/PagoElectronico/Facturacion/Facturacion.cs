using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Facturacion
{
    public partial class Facturacion : Form
    {
        private decimal idItem;
        public DataTable dt;
        private string usuario;
        private string salida;
        private decimal id_item_factura;
        public Facturacion(decimal id_item,string user)
        {
            InitializeComponent();
            usuario = user;
            idItem = id_item;
            //CARGO EL DATAGRIDVIEW CON LOS DATOS A FACTURAR
            Conexion con = new Conexion();
            string query = " SELECT I.fecha,I.monto,I.num_cuenta,T.descripcion, I.id_item_factura  "+
                            "FROM  LPP.ITEMS T JOIN LPP.ITEMS_FACTURA I ON T.id_item="+id_item+"  "+
                             "WHERE i.id_factura is NULL AND I.facturado = 0  "+
                             "ORDER BY i.num_cuenta";
            MessageBox.Show(""+query);
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            while (lector.Read())
            {
                dgvFactura.Rows.Add(new Object[] {lector.GetDateTime(0),lector.GetDecimal(1),lector.GetDecimal(2),lector.GetString(3),getIdCliente() });
                id_item_factura = lector.GetDecimal(4);
            }
            //id_item_factura = lector.GetDecimal(5);
            con.cnn.Close();
           
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow row in dgvFactura.Rows)
            {
                if (Convert.ToBoolean(row.Cells[5].Value) == true)
                {
                    bool algo = Convert.ToBoolean(row.Cells[5].Value);
                    salida = facturar();
                }
            }

            MessageBox.Show(""+salida);
           
        }
        private string facturar()
        {
            Conexion con = new Conexion();
            string salida = "Se facturo correctamente";
            try
            {
                string query = "PRC_facturar_item_factura";
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id_item_factura",id_item_factura));
                command.Parameters.Add(new SqlParameter("@id_factura", getIdFactura()));
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                salida = "No se pudo facturar" + ex.ToString();
            }
            con.cnn.Close();
            return salida;
        }
        private decimal getIdFactura()
        {
            Conexion con = new Conexion();
            string query = "PRC_obtener_factura";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@fecha", readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000"));
            command.Parameters.Add(new SqlParameter("@id_cliente", getIdCliente()));
            
            decimal id_Factura = Convert.ToDecimal(command.ExecuteScalar());
            return id_Factura;
        }
    }
}
