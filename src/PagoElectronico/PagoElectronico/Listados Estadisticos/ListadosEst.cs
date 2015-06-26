
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using readConfiguracion;
using System.Data.SqlClient;

namespace PagoElectronico.Listados_Estadisticos
{
    public partial class Listados : Form
    {
        public MenuPrincipal mp = new MenuPrincipal();
        public Listados()
        {
            InitializeComponent();

            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.AllowUserToDeleteRows = false;
            dgvDatos.ReadOnly = true;

            //CARGANDO DATOS INICIALES
            cmbListado.Items.Add("");
            cmbListado.Items.Add("Clientes con cuentas inhabilitadas por no pagar costos de transaccion");
            cmbListado.Items.Add("Cliente con mayor cantidad de comisiones facturadas en sus cuentas");
            cmbListado.Items.Add("Clientes con mayor cantidad de transacciones realizadas entre cuentas propias");
            cmbListado.Items.Add("Paises con mayor cantidad de movimientos");
            cmbListado.Items.Add("Total facturado para los distintos tipos de cuentas");

            cmbAño.Items.Add("");
            int i = 0;
            while (i < 50)
            {
                cmbAño.Items.Add((Convert.ToInt32(Configuracion.fechaSystem().Substring(0, 4)) - i + 5).ToString());
                i++;
            }
            cmbPeriodo.Items.Add("");
            cmbPeriodo.Items.Add("Enero-Febrero-Marzo");
            cmbPeriodo.Items.Add("Abril-Mayo-Junio");
            cmbPeriodo.Items.Add("Julio-Agosto-Septiembre");
            cmbPeriodo.Items.Add("Octubre-Noviembre-Diciembre");

            //FIN CARGANDO DATOS INICIALES

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            cmbAño.Text = " ";
            cmbListado.Text = " ";
            cmbPeriodo.Text = "";

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            mp.Show();
            this.Close();
        }

        private void cmbAño_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbAño.Text != "")
            {
                cmbPeriodo.Enabled = true;
            }
            else
            {
                cmbPeriodo.Text = "";
                cmbPeriodo.Enabled = false;
            }
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPeriodo.Text != "")
            {
                cmbListado.Enabled = true;
            }
            else
            {
                cmbListado.Text = "";
                cmbListado.Enabled = false;
            }
        }

        private void cmbListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbListado.Text != "")
            {
                btnBuscar.Enabled = true;
            }

        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            cmbAño.SelectedItem = null;
            cmbListado.SelectedItem = null;
            cmbPeriodo.SelectedItem = null;
            dgvDatos.DataSource = null;
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            
            if (cmbAño.Text == "")
            {
                MessageBox.Show("Elija un año por favor");
                return;
            }

            if (cmbPeriodo.Text == "")
            {
                MessageBox.Show("Elija un Periodo por favor");
                return;
            }

            if (cmbListado.Text == "")
            {
                MessageBox.Show("Elija un Listado por favor");
                return;
            }

            Conexion con = new Conexion();
            int inicio;
            int fin;
            int anio = Convert.ToInt32(cmbAño.SelectedItem);

            if (cmbPeriodo.Text == "Enero-Febrero-Marzo")
            {
                inicio = 1;
                fin = 3;
            }
            else
            {
                if (cmbPeriodo.Text == "Abril-Mayo-Junio")
                {
                    inicio = 4;
                    fin = 6;
                }
                else
                {
                    if (cmbPeriodo.Text == "Julio-Agosto-Septiembre")
                    {
                        inicio = 7;
                        fin = 9;
                    }
                    else
                    {
                        inicio = 10;
                        fin = 12;
                    }
                }
            }
            if (cmbListado.Text == "Clientes con cuentas inhabilitadas por no pagar costos de transaccion")
            {
                string query = "LPP.PRC_estadistico_cuentas_inhabilitadas";

                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@desde", inicio));
                command.Parameters.Add(new SqlParameter("@hasta", fin));
                command.Parameters.Add(new SqlParameter("@anio", anio));
                                
                SqlDataReader lector = command.ExecuteReader();
                
                if (!lector.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }
                DataTable dtDatos = new DataTable();
                dtDatos.Load(lector);
                con.cnn.Close();
                dgvDatos.DataSource = dtDatos;
                
            }

            if (cmbListado.Text == "Cliente con mayor cantidad de comisiones facturadas en sus cuentas")
            {
                string query = "LPP.PRC_estadistico_comisiones_facturadas";

                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@desde", inicio));
                command.Parameters.Add(new SqlParameter("@hasta", fin));
                command.Parameters.Add(new SqlParameter("@anio", anio));
                SqlDataReader lector = command.ExecuteReader();

                if (!lector.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }
                
                DataTable dtDatos = new DataTable();
                dtDatos.Load(lector);
                con.cnn.Close();
                dgvDatos.DataSource = dtDatos; ;

            }
            if (cmbListado.Text == "Clientes con mayor cantidad de transacciones realizadas entre cuentas propias")
            {
                string query = "LPP.PRC_estadistico_transacciones_cuentas_propias";

                con.cnn.Open();
                SqlCommand command = new SqlCommand(query, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@desde", inicio));
                command.Parameters.Add(new SqlParameter("@hasta", fin));
                command.Parameters.Add(new SqlParameter("@anio", anio));
                SqlDataReader lector = command.ExecuteReader();

                if (!lector.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }
                DataTable dtDatos = new DataTable();
                dtDatos.Load(lector);
                con.cnn.Close();
                dgvDatos.DataSource = dtDatos;
            }

            if (cmbListado.Text == "Paises con mayor cantidad de movimientos")
            {

                string query2 = "LPP.PRC_estadistico_pais_mas_movimientos";

                con.cnn.Open();
                SqlCommand command = new SqlCommand(query2, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@desde", inicio));
                command.Parameters.Add(new SqlParameter("@hasta", fin));
                command.Parameters.Add(new SqlParameter("@anio", anio));
                SqlDataReader lector2 = command.ExecuteReader();

                if (!lector2.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }

                DataTable dtDatos = new DataTable();
                dtDatos.Load(lector2);
                con.cnn.Close();
                dgvDatos.DataSource = dtDatos;
            }

            if (cmbListado.Text == "Total facturado para los distintos tipos de cuentas")
            {
                //int anio = Convert.ToInt32(cmbAño.Text);
                string query2 = "LPP.PRC_estadistico_facturado_tipo_cuentas";

                con.cnn.Open();
                SqlCommand command = new SqlCommand(query2, con.cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@desde", inicio));
                command.Parameters.Add(new SqlParameter("@hasta", fin));
                command.Parameters.Add(new SqlParameter("@anio", anio));
                SqlDataReader lector2 = command.ExecuteReader();

                if (!lector2.Read())
                {
                    MessageBox.Show("No hay ningun resultado que coincida con las opciones elegidas");
                    con.cnn.Close();
                    return;
                }
                
                DataTable dtDatos = new DataTable();
                dtDatos.Load(lector2);
                con.cnn.Close();
                dgvDatos.DataSource = dtDatos; ;
                con.cnn.Close();
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

   
    }
}
