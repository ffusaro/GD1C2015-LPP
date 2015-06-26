﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_de_Usuario
{
    public partial class BuscarUsuario : Form
         
    {
        ABMUsuario FormUsuario;
        ABM_Cliente.AsignarUsuario FormAsignar;
        ABM_Cuenta.AsignarUsuarioCuenta ac;
        public MenuPrincipal mp;
        public DataTable dt;
        public int ev;
        
        public BuscarUsuario(int evento)
        {
            InitializeComponent();
            ev = evento;
            dgvUsuario.AllowUserToAddRows = false;
            dgvUsuario.AllowUserToDeleteRows = false;
            dgvUsuario.ReadOnly = true;

            if (ev==1)
            {
                label6.Text = "Doble click en el Usuario que quiera Modificar/Eliminar";
            
            }
            else
            {

                label6.Text = "Doble click en el Usuario que quiera Asociar";
            }
         }

       
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuarios.Text = "";
            btnBusca.Enabled = false;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            Conexion con = new Conexion();
           

            string query = "SELECT username FROM LPP.USUARIOS ";

            if (txtUsuarios.Text != "")
            {
                query += "WHERE username LIKE '%" + txtUsuarios.Text + "%'";
            }

            con.cnn.Open();
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dgvUsuario.DataSource = dtDatos;
            con.cnn.Close();
       }

        private void dgvUsuario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            string Usuario = dgvUsuario.Rows[indice].Cells["username"].Value.ToString();

            if (ev == 1)
            {
                FormUsuario = new ABMUsuario(Usuario, "M_E");
                FormUsuario.usuario = Usuario;
                FormUsuario.Show();
                FormUsuario.padre_buscarUsuario = this;
                this.Close();
            }
            else {
                if (ev == 0)
                {

                    FormAsignar = new ABM_Cliente.AsignarUsuario("B", Usuario);
                    FormAsignar.Show();
                    this.Close();


                }
                else
                {
                    ac = new ABM_Cuenta.AsignarUsuarioCuenta("A", Usuario);
                    ac.Show();
                    this.Close();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsuarios_TextChanged(object sender, EventArgs e)
        {
            btnBusca.Enabled = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnBusca.Enabled = true;
        }

        private void txtLname_TextChanged(object sender, EventArgs e)
        {
            btnBusca.Enabled = true;
        }

     

       

       

        
    }
}
