using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using readConfiguracion;

namespace PagoElectronico
{
    class Conexion
    {
        public string cadenaconexion;
        public SqlConnection cnn;

        public Conexion()
        {
            try
            {
                
                //this.cadenaconexion = (@"Data Source=localhost\SQLSERVER2008;Initial Catalog=GD1C2015;User ID=gd;Password=gd2015");
                this.cadenaconexion =readConfiguracion.Configuracion.cadenaConexion();
                this.cnn = new SqlConnection(this.cadenaconexion);

            }
            catch(Exception ex)
            {
                MessageBox.Show("No se pudo realizar la conexion",ex.ToString());
            }
            
            
        }

    }
}