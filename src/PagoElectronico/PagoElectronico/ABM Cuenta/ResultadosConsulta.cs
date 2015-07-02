﻿using System;
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

namespace PagoElectronico.ABM_Cuenta
{
    public partial class ResultadosConsulta : Form
    {
        private decimal numcuenta;
        public DataTable dt;
        private string consulta;
        public string user;
 
        public ResultadosConsulta(string evento, decimal num_cuenta)
        {
            InitializeComponent();

            numcuenta = num_cuenta;
            consulta = evento;

            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.ReadOnly = true;

            if (consulta == "S")
            {

                string query = "SELECT saldo FROM LPP.CUENTAS WHERE num_cuenta = " + numcuenta + " ";
                this.ejecutarQuery(query);
            }

            if (consulta == "D")
            {
                string query = "SELECT TOP 5 * FROM LPP.DEPOSITOS WHERE num_cuenta = "+numcuenta+" ORDER BY fecha_deposito DESC ";
                this.ejecutarQuery(query);
            }

            if (consulta == "R")
            {
                string query = "SELECT TOP 5 * FROM LPP.RETIROS WHERE num_cuenta = "+numcuenta+" ORDER BY fecha DESC ";
                this.ejecutarQuery(query);
            }

            if (consulta == "T")
            {
                string query = "SELECT TOP 10 * FROM LPP.TRANSFERENCIAS WHERE num_cuenta_origen = "+numcuenta+" OR num_cuenta_destino = "+numcuenta+" ORDER BY fecha DESC ";
                this.ejecutarQuery(query);
            }

            

        }

        private void ejecutarQuery(string query) {
            Conexion con = new Conexion();
            con.cnn.Open();
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dt = dtDatos;
            dgvResults.DataSource = dtDatos;
         

            if(consulta == "D")
            {
                //CAMBIO COLUMNA DE NUM_TARJETA
                foreach (DataGridViewRow row in dgvResults.Rows)
                {
                    //string ultimosCuatro = lector.GetString(4);
                    string ultimosCuatro = (row.Cells["num_tarjeta"].Value).ToString();
                    row.Cells["num_tarjeta"].Value = "XXXX-XXXX-XXXX-" + ultimosCuatro.Remove(0, 12);
                }
              
            }
           
        }

     
        private void btSalir_Click_1(object sender, EventArgs e)
        {
            ABM_Cuenta.ConsultaSaldos cs = new ConsultaSaldos(user);
            cs.Show();
            this.Close();
        }
    }
}
