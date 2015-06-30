using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Helper;

namespace PagoElectronico.Login
{
    public partial class RolIngreso : Form
    {
        private string user;
        public MenuPrincipal mp = new MenuPrincipal();
        public Login.LogIn login = new LogIn();
        ABMS abm = new ABMS();

        public RolIngreso(string usuario, Login.LogIn log)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            user = usuario;
            login = log;
            /*CARGAR ROLES*/
            Conexion con1 = new Conexion();
            string query5 = "SELECT DISTINCT R.nombre FROM LPP.ROLES R JOIN LPP.ROLESXUSUARIO U " +
                            "ON U.rol = R.id_rol AND U.username = '" + user + " " +
                            "' AND R.habilitado = 1";

            con1.cnn.Open();
            SqlCommand command5 = new SqlCommand(query5, con1.cnn);
            SqlDataReader lector5 = command5.ExecuteReader();

            while (lector5.Read())
            {
                cmbRol.Items.Add(lector5.GetString(0));
            }

            con1.cnn.Close();
        }

        private void btnRol_Click(object sender, EventArgs e)
        {
            if (cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un Rol, por favor");
                return;
            }

            abm.ingresarAlSistema(user, login, this.mp, this, cmbRol.Text);


        }
    }
}
