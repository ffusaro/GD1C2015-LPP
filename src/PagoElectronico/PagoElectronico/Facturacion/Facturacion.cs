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
        public DataTable dtDatos;
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
            if (id_item == 0)
            {
                string query = "SELECT i.id_item_factura, i.num_cuenta, i.monto, it.descripcion, i.fecha FROM LPP.ITEMS_FACTURA i"
                            + " JOIN LPP.ITEMS it ON it.id_item = i.id_item"
                            + " WHERE i.id_factura is NULL AND i.facturado = 0  AND fecha IS NOT NULL ORDER BY i.num_cuenta";
                con.cnn.Open();
                dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
                da.Fill(dtDatos);
                dt = dtDatos;
            }
            else
            {

                string query = "SELECT i.id_item_factura, i.num_cuenta, i.monto, it.descripcion, i.fecha FROM LPP.ITEMS_FACTURA i"
                            + " JOIN LPP.ITEMS it ON it.id_item = i.id_item"
                            + " WHERE i.id_item = " + id_item + " AND  i.id_factura is NULL AND i.facturado = 0  AND fecha IS NOT NULL ORDER BY i.num_cuenta";

                con.cnn.Open();
                dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
                da.Fill(dtDatos);
                dt = dtDatos;
            }

            dgvFactura.DataSource = dtDatos;
            con.cnn.Close();

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            dgvFactura.Columns.Add(chk);
            chk.HeaderText = "Facturar";
            chk.Name = "chk";
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
                int ind = dgvFactura.Columns["chk"].Index;
                if (Convert.ToBoolean(row.Cells[ind].Value))
                {
                    int i = dgvFactura.Columns["id_item_factura"].Index;
                    id_item_factura = Convert.ToDecimal(row.Cells[i].Value);
                    salida = facturar(id_item_factura);
                }
            }

            MessageBox.Show(""+salida);
            Buscar busc = new Buscar(usuario);
            this.Close();           

        }
        private string facturar(decimal id_item)
        {
            
            Conexion con = new Conexion();
            string salida;

            try
            {
                string query = "PRC_facturar_item_factura";
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                id_item_factura = id_item;
                command.Parameters.Add(new SqlParameter("@id_item_factura",id_item_factura));
                decimal id_factura = getIdFactura();
                command.Parameters.Add(new SqlParameter("@id_factura", id_factura));
                command.ExecuteNonQuery();

                command.ExecuteNonQuery();
                
                salida = "Se facturo correctamente";
                
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
            DateTime fechaConfiguracion = DateTime.ParseExact(readConfiguracion.Configuracion.fechaSystem(), "yyyy-dd-MM", System.Globalization.CultureInfo.InvariantCulture);
            command.Parameters.Add(new SqlParameter("@fecha", fechaConfiguracion));
            command.Parameters.Add(new SqlParameter("@id_cliente", getIdCliente()));
            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@id_factura";
            outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
            outPutParameter.Direction = System.Data.ParameterDirection.Output;
            command.Parameters.Add(outPutParameter);

            command.ExecuteNonQuery();

            return Convert.ToDecimal(outPutParameter.Value); 
        }
    }
}
