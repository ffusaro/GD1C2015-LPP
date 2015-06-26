﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PagoElectronico.Tarjetas
{
    public partial class BuscarTarjetas : Form
    {
        public DataTable dt;
        public Tarjetas.abmTarjetas abmt;
        private string usuario;
        private string numTarjeta;

        public BuscarTarjetas(string user)
        {
            InitializeComponent();
            usuario = user;

            dgvTarjetas.AllowUserToAddRows = false;
            dgvTarjetas.AllowUserToDeleteRows = false;
            dgvTarjetas.ReadOnly = true;

            //CARGO DGV CON LAS TARJETAS ASOCIADAS AL CLIENTE ASOCIADO AL USUARIO
            Conexion con = new Conexion();
            string query = " SELECT C.apellido +', '+ C.nombre as 'Apellido, Nombre', T.num_tarjeta,E.emisor_descr,T.fecha_emision,T.fecha_vencimiento  "+
                            " FROM LPP.CLIENTES C JOIN LPP.TARJETAS T ON T.id_cliente=C.id_cliente  "+
					                             "JOIN LPP.EMISORES E ON E.id_emisor = T.id_emisor  "+
                             "WHERE C.username = '"+usuario+"'";
            con.cnn.Open();
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
            da.Fill(dtDatos);
            dt = dtDatos;
            dgvTarjetas.DataSource = dtDatos;
            con.cnn.Close();


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTarjetas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            string num_tarjeta = dgvTarjetas.Rows[indice].Cells["num_tarjeta"].Value.ToString();

            abmt = new Tarjetas.abmTarjetas(usuario,num_tarjeta);
            abmt.txtNumTarjeta.Enabled = false;
            abmt.Show();
            this.Close();
        }
        
    }
}
