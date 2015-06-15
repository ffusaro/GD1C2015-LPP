using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using Helper;
using readConfiguracion;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class BuscarCuenta : Form
    {
        
        AltaCuenta FormCuenta;
        public MenuPrincipal mp;
        public int ev;

        public BuscarCuenta(int evento)
        {
            InitializeComponent();
            ev = evento;
            btnBuscar.Enabled = false;
        }
        private void btnSalir_Click(object sender, EventArgs e)
 {
     this.Close();
 }

 private void btnLimpiar_Click(object sender, EventArgs e)
 {
     txtUsuario.Text = "";
     btnBuscar.Enabled = false;
 }

 private void btnBuscar_Click(object sender, EventArgs e)
 {

     Conexion con = new Conexion();

     con.cnn.Open();
     string query = "SELECT id_cliente FROM LPP.CLIENTE WHERE username = '" + txtUsuario.Text + "'";
     SqlCommand command = new SqlCommand(query, con.cnn);
     SqlDataReader lector = command.ExecuteReader();
     lector.Read();
     int id_Cliente = lector.GetInt32(0);
     con.cnn.Close();

     string query1 = "SELECT num_cuenta FROM LPP.CUENTA WHERE id_cliente = id_Cliente"; 
    
     DataTable dtDatos = new DataTable();
     SqlDataAdapter da = new SqlDataAdapter(query1, con.cnn);
     da.Fill(dtDatos);
     dgvCuentas.DataSource = dtDatos;
     con.cnn.Close();
 }

 private void dgvUsuario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
 {
     int indice = e.RowIndex;
     string Cuenta = dgvCuentas.Rows[indice].Cells["Cuenta"].Value.ToString();

      FormCuenta = new AltaCuenta(Cuenta, "M_E");
      FormCuenta.Show();
      FormCuenta.padre_buscarCuenta = this;
       this.Close();
     
    
 }
 private void txtUsuarios_TextChanged(object sender, EventArgs e)
 {
     btnBuscar.Enabled = true;
 }

 private void txtName_TextChanged(object sender, EventArgs e)
 {
     btnBuscar.Enabled = true;
 }

 private void txtLname_TextChanged(object sender, EventArgs e)
 {
     btnBuscar.Enabled = true;
 }

 


 
    }

       
    }

