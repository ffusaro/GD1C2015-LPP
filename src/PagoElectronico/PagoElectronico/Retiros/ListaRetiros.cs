using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico;
using Helper;
using readConfiguracion;
using System.Data.SqlClient;

namespace PagoElectronico.Retiros
{
    public partial class ListaRetiros : Form
    {
        public int cuenta;
        public DataTable dt;
        public ListaRetiros(int num_cuenta)
        {
            InitializeComponent();
            cuenta = num_cuenta;
            //CARGO EL DATAGRIDVIEW CON LOS DATOS DEL RETIRO
            Conexion con = new Conexion();
            string query = "SELECT C.cheque_num, R.id_retiro,R.num_cuenta,R.importe,R.fecha,C.cliente_receptor,B.nombre " +
                           "FROM LPP.RETIROS R JOIN LPP.CHEQUES C ON R.id_retiro= C.id_retiro JOIN LPP.BANCOS B ON B.id_banco=C.id_banco " +
                           "WHERE R.num_cuenta = "+cuenta+"";
                           
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dt = dtDatos;
            dgvRetiros.DataSource = dtDatos;
            con.cnn.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
