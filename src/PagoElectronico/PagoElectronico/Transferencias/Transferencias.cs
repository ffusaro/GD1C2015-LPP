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

namespace PagoElectronico.Transferencias
{
    public partial class Transferencias : Form
    {
        public string usuario;
        public decimal num_cuenta_origen;
        public decimal num_cuenta_destino;
        public decimal importe;
        private decimal costoTransferencia;

        public Transferencias(string user,decimal num_cuenta)
        {
            InitializeComponent();
            usuario = user;
            grpDatos.Enabled = false;
            btnNuevo.Enabled = true;
            btnSalir.Enabled = true;
            btnGrabar.Enabled = false;
            btnLimpiar.Enabled = false;

            //CARGA DE NUMEROS DE CUENTA DE ORIGEN
            Conexion con = new Conexion();
            string query = "PRC_cuentas_de_un_cliente";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id_cliente", getIdCliente()));
            SqlDataReader lector = command.ExecuteReader();
            while (lector.Read())
            {
                cmbNroCuenta.Items.Add(lector.GetDecimal(0));
            }

            con.cnn.Close();
            if(num_cuenta != 0)
            {
                txtCuentaDestino.Text = Convert.ToString(num_cuenta);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled = true;
            btnLimpiar.Enabled = true;
            btnGrabar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbNroCuenta.SelectedItem = null;
            txtCuentaDestino.Text = "";
            txtImporte.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarCuentas bc = new BuscarCuentas(usuario);
            bc.num_cuenta_origen = Convert.ToDecimal(cmbNroCuenta.Text);
            bc.importe = Convert.ToDecimal(txtImporte.Text);
            bc.Show();
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (cmbNroCuenta.SelectedItem == null)
            {
                MessageBox.Show("Elija Número de Cuenta Origen por favor");
                return;
            }
            if(txtImporte.Text == "")
            {
                MessageBox.Show("Ingrese un Importe por favor");
                return;
            }
            int temp;
            try
            {
                if (txtImporte.Text != "")
                    temp = Convert.ToInt32(txtImporte.Text);

            }
            catch (Exception h)
            {
                MessageBox.Show("Importe solo puede contener números", h.ToString());
                return;
            }
            if (Convert.ToInt32(txtImporte.Text) < 0)
            {
                MessageBox.Show("El importe ingresado debe ser mayor a cero");
                txtImporte.Focus();
                return;
            }
            if (txtCuentaDestino.Text == "")
            {
                MessageBox.Show("Elija un Numero de Cuenta Destino por favor");
                return;
            }
            try
            {
                if (txtCuentaDestino.Text != "")
                    temp = Convert.ToInt32(txtCuentaDestino.Text);

            }
            catch (Exception h)
            {
                MessageBox.Show("Cuenta destino solo puede contener números", h.ToString());
                return;
            }


            costoTransferencia = getCosto(usuario,Convert.ToDecimal(cmbNroCuenta.SelectedItem));
            int id_trans = grabarTransferencia(Convert.ToDecimal(cmbNroCuenta.SelectedItem),Convert.ToDecimal(txtCuentaDestino.Text),Convert.ToDecimal(txtImporte.Text));
            agregarAFactura();
            DialogResult dialogResult = MessageBox.Show("Su Tranferencia se realizo correctamente. ¿Desea ver el comprobante?", "Retiro de Efectivo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                cmbNroCuenta.SelectedItem = null;
                txtImporte.Text = "";
                grpDatos.Enabled = false;
                btnNuevo.Enabled = true;
                btnLimpiar.Enabled = false;
                btnSalir.Enabled = true;
                btnGrabar.Enabled = false;
                ListaTransferencias lt = new ListaTransferencias(id_trans);
                lt.Show();
                this.Close();
            }
            else
            {
                this.Close();
            }

        }
        private int grabarTransferencia(decimal origen, decimal destino, decimal importe)
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "PRC_realizar_transferencia";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_cuenta_origen", origen));
            command.Parameters.Add(new SqlParameter("@num_cuenta_destino", destino));
            command.Parameters.Add(new SqlParameter("@importe", importe));
            command.Parameters.Add(new SqlParameter("@fecha",readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000"));
            SqlDataReader lector = command.ExecuteReader();
            con.cnn.Close();

            //OBTENGO ID TRANSFERENCIA
            string query2 = "SELECT id_transferencia FROM LPP.TRANSFERENCIAS WHERE num_cuenta_origen = " + origen + "" +
                            "ORDER BY fecha DESC";
            con.cnn.Open();
            SqlCommand command2 = new SqlCommand(query2, con.cnn);
            SqlDataReader lector2 = command2.ExecuteReader();
            lector2.Read();
            int id_transferencia = lector2.GetInt32(0);
            con.cnn.Close();

            return id_transferencia;
        }
        private decimal getCosto(string usuario, decimal numOrigen)
        {
            Conexion con = new Conexion();
            //OBTENGO EL COSTO DE LA TRANSFERENCIA DEPENDIENDO LA CUENTA DEL USUARIO
            string query = "SELECT p.costo_transaccion FROM LPP.CLIENTES C JOIN LPP.CUENTAS T ON c.username='"+usuario+"' AND t.num_cuenta = "+numOrigen+""+
                           "JOIN LPP.TIPOS_CUENTA P ON P.id_tipocuenta=T.id_tipo ";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            decimal costo = lector.GetDecimal(0);
            return costo;
        }
        private void agregarAFactura()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
           
            //INSERTO EN FACTURA
            con.cnn.Open();
            string query = "INSERT INTO LPP.FACTURAS (id_cliente,fecha) VALUES (" + getIdCliente() + ", CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103))";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.ExecuteNonQuery();
            con.cnn.Close();
            //OBTENGO ID_FACTURA
            string query1 = "SELECT id_factura FROM LPP.FACTURAS WHERE id_cliente = "+getIdCliente()+"' ORDER BY fecha DESC";
            SqlCommand command1 = new SqlCommand(query1, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            int id_factura = lector.GetInt32(0);
            con.cnn.Close();
            /*EN DUDA POR EL TRIGGER
            //INSERTO ITEMS_FACTURA
            con.cnn.Open();
            string query2 = "INSERT INTO LPP.ITEMS_FACTURA (id_item,num_cuenta,monto,facturado,id_factura,fecha) VALUES (" + getIdItem() + ","+Convert.ToDecimal(cmbNroCuenta.SelectedItem)+","+costoTransferencia+",0,"+id_factura+", CONVERT(datetime,'" + readConfiguracion.Configuracion.fechaSystem() + " 00:00:00.000', 103))";
            SqlCommand command2 = new SqlCommand(query2, con.cnn);
            command2.ExecuteNonQuery();
            con.cnn.Close();
             */
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
        private int getIdItem()
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            //OBTENGO ID ITEM
            string query = "SELECT id_item FROM LPP.ITEMS WHERE descripcion = 'Comisión por transferencia.'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            int id_item = lector.GetInt32(0);
            con.cnn.Close();
            return id_item;
        }

    }
}
