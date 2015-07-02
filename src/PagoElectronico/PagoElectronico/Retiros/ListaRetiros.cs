using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Helper;
using readConfiguracion;
using System.Data.SqlClient;

namespace PagoElectronico.Retiros
{
    public partial class ListaRetiros : Form
    {
        private decimal id_retiro;
        private DataTable dtRetiro;
        private DataTable dtCheque;

        public ListaRetiros(decimal retiro)
        {
            InitializeComponent();
            id_retiro = retiro;

            dgvCheque.AllowUserToAddRows = false;
            dgvCheque.AllowUserToDeleteRows = false;
            dgvCheque.ReadOnly = true;

            Conexion con = new Conexion();

            string query2 = "SELECT cheque_num, c.id_retiro, r.num_cuenta, c.importe, c.fecha, b.nombre, cl.nombre as 'nombre_receptor', cl.apellido as 'apellido_receptor', m.descripcion FROM LPP.CHEQUES c "
                            +" JOIN LPP.BANCOS b ON b.id_banco = c.id_banco JOIN LPP.CLIENTES cl ON cl.id_cliente = c.cliente_receptor JOIN LPP.RETIROS r ON r.id_retiro = c.id_retiro JOIN LPP.MONEDAS m ON m.id_moneda = r.id_moneda "
                            +" WHERE c.id_retiro = " + id_retiro + " ";
            DataTable dtCh = new DataTable();
            SqlDataAdapter dch = new SqlDataAdapter(query2, con.cnn);
            dch.Fill(dtCh);
            dtCheque = dtCh;
            dgvCheque.DataSource = dtCh;
            con.cnn.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
