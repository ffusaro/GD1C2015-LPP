using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PagoElectronico.ABM_Rol
{
    public partial class ABMRol : Form
    {
        public string evento;
        public MenuPrincipal mp;
        public BuscarRol bc;
        public int ban;
       

        public ABMRol(string ev)
        {
            InitializeComponent();
            evento = ev;
          

            string funcionalidad;
            if (evento == "A")
            {
                chkBoxHabilitado.Visible = false;
                btnGrabar.Enabled = false;
            }
            else
            {
                txtNombre.Text = evento;
            }


            Conexion con = new Conexion();
            Conexion con2 = new Conexion();

            /*CARGO LAS FUNCIONALIDADES EN UN CHECKLIST*/
            string query = "SELECT descripcion FROM LPP.FUNCIONALIDAD";
            
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            int i = 0;

            while (lector.Read())
            {
                funcionalidad = lector.GetString(0);
                chkListFuncionalidades.Items.Add(funcionalidad);

                if (evento != "A")
                {
                    int id_rol_abm = getIdRol(evento);
                    //VERIFICA SI EL ROL CONTIENE LA FUNCIONALIDAD
                    query = "SELECT 1 FROM LPP.FUNCIONALIDADXROL F " +
                                 "JOIN LPP.FUNCIONALIDAD D ON D.id_funcionalidad = F.funcionalidad " +
                                 "AND D.descripcion = '" + funcionalidad + "' " +
                                 "WHERE F.rol = '" + id_rol_abm + "'";
                    
                    con2.cnn.Open();
                    SqlCommand command2 = new SqlCommand(query, con2.cnn);
                    SqlDataReader lector2 = command2.ExecuteReader();
                   if (lector2.Read())
                    {
                        chkListFuncionalidades.SetItemChecked(i, true);
                    }
                    con2.cnn.Close();
                  }
                i++;
            }
            con.cnn.Close();
            if (evento != "A")
            {
                con.cnn.Open();
                try
                {
                    string queryM = "SELECT habilitado FROM LPP.ROLES WHERE nombre = '" +evento + "' ";
                    SqlCommand commando = new SqlCommand(queryM, con.cnn);
                    SqlDataReader lectorcito = commando.ExecuteReader();
                    lectorcito.Read();
                    chkBoxHabilitado.Checked = lectorcito.GetBoolean(0);

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error de lectura del campo Habilitado"+ex);
                }
                con.cnn.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            chkBoxHabilitado.Checked = false;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            if (evento != "A")
            {
                Int32 id_rol = getIdRol(evento);
                string query = "DELETE LPP.FUNCIONALIDADXROL WHERE rol = " + id_rol + "";
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.ExecuteNonQuery();
                con.cnn.Close();
            }
            else
            {
                
                string query1 = "SELECT 1 FROM LPP.ROLES WHERE nombre = '" + txtNombre.Text + "'";
                con.cnn.Open();
                SqlCommand command1 = new SqlCommand(query1, con.cnn);
                SqlDataReader lector1 = command1.ExecuteReader();

                if (lector1.Read())
                {
                    MessageBox.Show("Nombre de Rol ya existente.");
                    con.cnn.Close();
                    return;
                }
                con.cnn.Close();

                string query2 = "INSERT INTO LPP.ROLES (nombre,habilitado) VALUES ('" + txtNombre.Text + "',1)";
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query2, con.cnn);
                command.ExecuteNonQuery();
                con.cnn.Close();

            }

            Int32 id_rol_nuevo = getIdRol(txtNombre.Text);
            foreach (object itemsCheck in chkListFuncionalidades.CheckedItems)
            {
                string query = "INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('" + id_rol_nuevo + "',(SELECT F.id_funcionalidad FROM LPP.FUNCIONALIDAD F WHERE F.descripcion = '" + itemsCheck.ToString() + "'))";
                con.cnn.Open();
                SqlCommand command2 = new SqlCommand(query, con.cnn);
                command2.ExecuteNonQuery();
                con.cnn.Close();
            }
            if (evento == "A")
            {
                MessageBox.Show("Rol Creado Correctamente");
                this.Close();
            }
            else
            {
                string query = "UPDATE LPP.ROLES SET habilitado = '" + chkBoxHabilitado.Checked + "' WHERE nombre = '" + txtNombre.Text + "'";
                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.ExecuteNonQuery();
                con.cnn.Close();

                MessageBox.Show("Rol Modificado Correctamente");
                bc.Close();
                this.Close();
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")
            {
                btnGrabar.Enabled = true;
                chkListFuncionalidades.Enabled = true;
            }
            else
            {
                btnGrabar.Enabled = false;
            }
        }
        private Int32 getIdRol(string rol)
        {
            Conexion con = new Conexion();

            //CONSIGO ID_ROL
            string query3 = "SELECT id_rol FROM LPP.ROLES WHERE nombre = '" + rol + "'";
            con.cnn.Open();
            SqlCommand command3 = new SqlCommand(query3, con.cnn);
            Int32 id_rol = Convert.ToInt32(command3.ExecuteScalar());
            con.cnn.Close();
            return id_rol;
        
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))))
            {
                e.Handled = true;  
            }else{
                e.Handled = false;
            }
        }

      
    }
}
