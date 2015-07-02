
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
        private DataTable dtResults;

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
               string query = "SELECT TOP 5 c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc, fecha_nac, mail, COUNT(id_deshabilitada) AS cant_veces_inhabilitado FROM LPP.CLIENTES c"
		                    + " JOIN LPP.TIPO_DOCS t ON t.tipo_cod = c.id_tipo_doc"
		                    + " JOIN LPP.CUENTAS cu ON cu.id_cliente = c.id_cliente"
		                    + " JOIN LPP.CUENTAS_DESHABILITADAS cd ON cd.num_cuenta = cu.num_cuenta"
	                        + " WHERE cd.motivo = 'Por deber mas de 5 transacciones' "
	                        + " AND MONTH(cd.fecha_deshabilitacion) BETWEEN "+ inicio +" AND "+ fin +" AND YEAR(cd.fecha_deshabilitacion) = "+ anio+""
	                        + " GROUP BY c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail"
	                        + " ORDER BY COUNT(id_deshabilitada) DESC";
                con.cnn.Open();
                DataTable dtDatos = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con.cnn);
                da.Fill(dtDatos);
                dtResults = dtDatos;
                dgvDatos.DataSource = dtDatos;
                con.cnn.Close();
                
            }

            if (cmbListado.Text == "Cliente con mayor cantidad de comisiones facturadas en sus cuentas")
            {
                string query2 = "SELECT TOP 5 c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail, COUNT(i.id_item_factura) as 'comisiones_facturas' FROM LPP.CLIENTES c"
		                        + " JOIN LPP.CUENTAS cu ON cu.id_cliente = c.id_cliente"
		                        +" JOIN LPP.ITEMS_FACTURA i ON i.num_cuenta= cu.num_cuenta"
		                        +" JOIN LPP.TIPO_DOCS t ON t.tipo_cod = c.id_tipo_doc"
	                            +" WHERE i.facturado = 1"
			                    +" AND MONTH(i.fecha) BETWEEN "+inicio+" AND "+fin+" AND YEAR(i.fecha) = "+anio+ ""
		                        +" GROUP BY c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail"
		                        +" ORDER BY COUNT(i.id_item_factura) DESC";
                 con.cnn.Open();
                 DataTable dtDatos = new DataTable();
                 SqlDataAdapter da = new SqlDataAdapter(query2, con.cnn);
                 da.Fill(dtDatos);
                 dtResults = dtDatos;
                 dgvDatos.DataSource = dtDatos;
                 con.cnn.Close();

            }
            if (cmbListado.Text == "Clientes con mayor cantidad de transacciones realizadas entre cuentas propias")
            {
                string query3 = "SELECT TOP 5 c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail, COUNT(tr.id_transferencia) FROM LPP.CLIENTES c"
		                        +" JOIN LPP.CUENTAS c1 ON c1.id_cliente = c.id_cliente"
		                        +" JOIN LPP.CUENTAS c2 ON c2.id_cliente = c.id_cliente"
		                        +" JOIN LPP.TIPO_DOCS t ON t.tipo_cod = c.id_tipo_doc"
		                        +" JOIN LPP.TRANSFERENCIAS tr ON tr.num_cuenta_origen = c1.num_cuenta AND tr.num_cuenta_destino= c2.num_cuenta"
		                        +" WHERE c1.num_cuenta <> c2.num_cuenta"
			                    +" AND MONTH(tr.fecha) BETWEEN "+inicio+" AND "+fin+" AND YEAR(tr.costo_trans) = "+anio+""
		                        +" GROUP BY c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail"
		                        +" ORDER BY  COUNT(tr.id_transferencia) DESC";
             con.cnn.Open();
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query3, con.cnn);
            da.Fill(dtDatos);
            dtResults = dtDatos;
            dgvDatos.DataSource = dtDatos;
            con.cnn.Close();
                
            }

            if (cmbListado.Text == "Paises con mayor cantidad de movimientos")
            {
                string query4 = "SELECT TOP 5 pais, count_big(d.num_deposito)+count_big(r.id_retiro)+sum(convert(bigint,t1.cant_or))+SUM(convert(bigint,t2.cant_dest)) as 'cant_movimientos' FROM LPP.PAISES p"
		                        +" JOIN LPP.CUENTAS c ON c.id_pais = p.id_pais"
		                        +" JOIN LPP.DEPOSITOS d ON d.num_cuenta = c.num_cuenta"
		                        +" JOIN LPP.RETIROS r ON r.num_cuenta = c.num_cuenta"
		                        +" JOIN (select num_cuenta_origen, COUNT(*) as cant_or, fecha from LPP.TRANSFERENCIAS group by num_cuenta_origen, fecha) t1 ON t1.num_cuenta_origen = c.num_cuenta"
		                        +" JOIN (select num_cuenta_destino, COUNT(*) as cant_dest, fecha from LPP.TRANSFERENCIAS group by num_cuenta_destino, fecha) t2 ON t2.num_cuenta_destino = c.num_cuenta"
	                            +" WHERE (MONTH(d.fecha_deposito) BETWEEN "+inicio+" AND "+fin+" AND YEAR(d.fecha_deposito)= "+anio+")"
                                +" AND(MONTH(r.fecha) BETWEEN "+inicio+" AND "+fin+" AND YEAR(r.fecha)= "+anio+")"
	                            +" AND (MONTH(t1.fecha) BETWEEN "+inicio+" AND "+fin+" AND YEAR(t1.fecha)= "+anio+")"
	                            +" AND (MONTH(t2.fecha) BETWEEN "+inicio+" AND "+fin+" AND YEAR(t2.fecha)= "+anio+")"
	                            +" GROUP BY pais"
	                            +" ORDER BY 2 DESC";

                    con.cnn.Open();
                    DataTable dtDatos = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(query4, con.cnn);
                    da.Fill(dtDatos);
                    dtResults = dtDatos;
                    dgvDatos.DataSource = dtDatos;
                    con.cnn.Close();

            }

            if (cmbListado.Text == "Total facturado para los distintos tipos de cuentas")
            {
                string query5 = "SELECT TOP 5 t.id_tipocuenta, t.descripcion, SUM(i.monto) AS 'totalFacturado' FROM LPP.TIPOS_CUENTA t"
		                        +" JOIN LPP.CUENTAS c ON c.id_tipo = t.id_tipocuenta"
		                        +" JOIN LPP.ITEMS_FACTURA i ON i.num_cuenta = c.num_cuenta"
	                            +" WHERE i.facturado = 1 "
	                            +" AND MONTH(i.fecha) BETWEEN "+inicio+" AND "+fin+" AND YEAR(i.fecha) = "+anio+""	
	                            +" GROUP BY id_tipocuenta, descripcion"
	                            +" ORDER BY SUM(monto) DESC";
            con.cnn.Open();
            DataTable dtDatos = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query5, con.cnn);
            da.Fill(dtDatos);
            dtResults = dtDatos;
            dgvDatos.DataSource = dtDatos;
            con.cnn.Close();
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

   
    }
}
