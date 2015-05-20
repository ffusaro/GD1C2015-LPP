using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico
{
    public partial class BuscarRol : Form
    {

        public MenuPrincipal mp;
        public string evento;

        public BuscarRol(string ev)
        {
            InitializeComponent();
            evento = ev;
            btnContinuar.Enabled = false;

            if (evento == "M")
            {
                btnContinuar.Text = "Modificar";
            }
            else
            {
                btnContinuar.Text = "Eliminar";
            }
            /*CARGA LOS ROLES EN EL COMBOBOX*/
            Conexion con = new Conexion();
            string query = "SELECT nombre FROM LPP.ROLES WHERE 1=1 ";

            if (evento == "B")
            {
                query += "AND habilitado = 1";
            }
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();

            while (lector.Read())
            {
                cmbRoles.Items.Add(lector.GetString(0));
            }
            con.cnn.Close();
            btnContinuar.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (evento == "B")
            {
                Conexion con = new Conexion();
                string query = "UPDATE LPP.ROLES SET habilitado = 0 WHERE nombre = '" + cmbRoles.Text + "'";
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.ExecuteNonQuery();
                con.cnn.Close();

                MessageBox.Show("Rol Eliminado Correctamente");
                this.Close();
            }
            else
            {
                ABMRol abmRol = new ABMRol(cmbRoles.Text);
                abmRol.Show();
                abmRol.bc = this;
                this.Close();
            }
        }

        private void cmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoles.Text != "")
            {
                btnContinuar.Enabled = true;
            }
        }

        private void BuscarRol_Load(object sender, EventArgs e)
        {

        }

     
    }
}
