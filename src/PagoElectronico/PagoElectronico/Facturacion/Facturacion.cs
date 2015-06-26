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
        private decimal num_cuenta;

        public Facturacion(decimal id_item,string user)
        {
            InitializeComponent();
            usuario = user;
            idItem = id_item;

            dgvFactura.AllowUserToAddRows = false;
            dgvFactura.AllowUserToDeleteRows = false;
            dgvFactura.ReadOnly = true;

            //CARGO EL DATAGRIDVIEW CON LOS DATOS A FACTURAR
            Conexion con = new Conexion();
            if (getRolUser() == "Administrador")
            {
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
            }
            else
            {
                if (id_item == 0)
                {
                    string query = "SELECT i.id_item_factura, i.num_cuenta, i.monto, it.descripcion, i.fecha FROM LPP.ITEMS_FACTURA i"
                                + " JOIN LPP.ITEMS it ON it.id_item = i.id_item"
                                + " JOIN LPP.CUENTAS c ON c.num_cuenta = i.num_cuenta "
                                + " JOIN LPP.CLIENTES cl ON cl.id_cliente = c.id_cliente"
                                + " WHERE cl.username = '" + usuario + "' AND  i.id_factura is NULL AND i.facturado = 0  AND fecha IS NOT NULL ORDER BY i.num_cuenta";
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
                                +" JOIN LPP.CUENTAS c ON c.num_cuenta = i.num_cuenta "
                                +" JOIN LPP.CLIENTES cl ON cl.id_cliente = c.id_cliente"
                                + " WHERE i.id_item = " + id_item + " AND cl.username = '"+usuario+"' AND  i.id_factura is NULL AND i.facturado = 0  AND fecha IS NOT NULL ORDER BY i.num_cuenta";

                    con.cnn.Open();
                    dtDatos = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
                    da.Fill(dtDatos);
                    dt = dtDatos;
                }
                
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
            if (getRolUser() == "Administrador")
            {
                con.cnn.Open();
                string query = "SELECT id_cliente FROM LPP.CUENTAS WHERE num_cuenta = '" + num_cuenta + "'";
                SqlCommand command = new SqlCommand(query, con.cnn);
                SqlDataReader lector = command.ExecuteReader();
                lector.Read();
                int id_cliente = lector.GetInt32(0);
                con.cnn.Close();
                return id_cliente;

            }
            else
            {
                con.cnn.Open();
                string query = "SELECT id_cliente FROM LPP.CLIENTES WHERE username = '" + usuario + "'";
                SqlCommand command = new SqlCommand(query, con.cnn);
                SqlDataReader lector = command.ExecuteReader();
                lector.Read();
                int id_cliente = lector.GetInt32(0);
                con.cnn.Close();
                return id_cliente;

            }

        }

        private string getRolUser()
        {
            Conexion con = new Conexion();
            //OBTENGO ID DE CLIENTE
            con.cnn.Open();
            string query = "SELECT R.nombre FROM LPP.ROLESXUSUARIO U JOIN LPP.ROLES R ON R.id_rol=U.rol WHERE U.username = '" + usuario + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            string rol = lector.GetString(0);
            con.cnn.Close();
            return rol;
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
                    if (getRolUser() == "Administrador") {
                        int i = dgvFactura.Columns["id_item_factura"].Index;
                        id_item_factura = Convert.ToDecimal(row.Cells[i].Value);
                        int j = dgvFactura.Columns["num_cuenta"].Index;
                        num_cuenta = Convert.ToDecimal(row.Cells[j].Value);
                        salida = facturar(id_item_factura);
                    
                    }
                    else
                    {
                        int i = dgvFactura.Columns["id_item_factura"].Index;
                        id_item_factura = Convert.ToDecimal(row.Cells[i].Value);
                        salida = facturar(id_item_factura);
                    }
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
                string query = "LPP.PRC_facturar_item_factura";
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                id_item_factura = id_item;
                command.Parameters.Add(new SqlParameter("@id_item_factura",id_item_factura));
                decimal id_factura = getIdFactura();
                command.Parameters.Add(new SqlParameter("@id_factura", id_factura));
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
            string query = "LPP.PRC_obtener_factura";
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
