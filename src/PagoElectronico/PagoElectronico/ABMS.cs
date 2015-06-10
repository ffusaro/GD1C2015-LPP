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

namespace PagoElectronico
{
    class ABMS
    {
        Conexion con1 = new Conexion();
        SqlDataReader dr;


        //ABM CLIENTE
        public string insertarCliente(string Nombre, string Apellido, string Tipo_ID, int Numero_ID, string Mail, DateTime Nacimiento, string Nacionalidad, bool Habilitacion,int id_domicilio, string usuario)
        {
            con1.cnn.Open();
           
            string salida = "Se registro el Cliente correctamente";

            try
            {
                string query = "INSERT INTO LPP.CLIENTES (num_doc, apellido, nombre, fecha_nac, mail, id_tipo_Doc, Nacionalidad, Habilitado,id_domicilio,username) VALUES (" + Numero_ID + ", '" + Apellido + "', '" + Nombre + "', '" + Nacimiento + "', '" + Mail + "', '" + Tipo_ID + "','" + Nacionalidad + "', '" + Habilitacion + "', "+id_domicilio +",'"+ usuario +"')";
                SqlCommand command = new SqlCommand(query, con1.cnn);
                command.ExecuteNonQuery();

            }
            
            catch(Exception ex)
            {
              salida = "No se pudo insertar el Cliente"+ ex.ToString() ;
            }


            con1.cnn.Close();  
            return salida;
             
        }
        public int clienteRegistrado( string Tipo_ID, int Numero_ID)
        {
            con1.cnn.Open();
            int contador = 0;
            try
            {
                string query = "SELECT id_tipo_doc, num_doc FROM LPP.CLIENTES WHERE id_tipo_doc = '"+ Tipo_ID +"' AND num_doc = "+ Numero_ID +"";
                SqlCommand command = new SqlCommand(query, con1.cnn);
                dr = command.ExecuteReader();
                while(dr.Read())
                {
                    
                    contador++;
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("El Cliente ya existe",ex.ToString());
            }
            con1.cnn.Close();  
            return contador;
        
        
        }

        public string modificarCliente(string Nombre, string Apellido, string Tipo_ID, int Numero_ID, string Mail, DateTime Nacimiento,string Nacionalidad, bool Habilitacion)
        {
            con1.cnn.Open();
            string salida = "Se modificó el Cliente correctamente";

            try
            {
                string query = "UPDATE LPP.CLIENTES SET fecha_nac = '" + Nacimiento + "', mail = '" + Mail + "', apellido = '" + Apellido + "', nombre = '" + Nombre + "',  Nacionalidad='" + Nacionalidad + "', Habilitado ='" + Habilitacion + "' WHERE id_tipo_doc = '"+Tipo_ID+"' AND num_doc = "+ Numero_ID+"";
                SqlCommand command = new SqlCommand(query, con1.cnn);
                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                salida = "No se pudo modificar el Cliente" + ex.ToString();
            }

            con1.cnn.Close();  
            return salida;

        }

        public string eliminarCliente(string Tipo_ID, int Numero_ID)
        {
            con1.cnn.Open();
            string salida = "Se deshabilito el Cliente correctamente";

            try
            {
                string query = "UPDATE LPP.CLIENTES SET habilitado = 0 WHERE id_tipo_doc = '" +Tipo_ID + "' AND num_doc = '" + Numero_ID + "'";
                SqlCommand command = new SqlCommand(query, con1.cnn);
                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                salida = "No se pudo deshabilitar el cliente" + ex.ToString();
            }

            con1.cnn.Close();  
            return salida;

        }

        //ABM DOMICILIO
        public int insertarDomicilio(string calle, int numero, int Piso, string depto, string localidad)
        {
            string salida = "Se registro DOMICILIO correctamente";
            con1.cnn.Open();
            try
            {
                string query = "INSERT INTO LPP.DOMICILIO (calle, numero, piso, depto, localidad)" +
                               " VALUES ('" + calle + "'," + numero + "," + Piso + ",'"+ depto +"', '"+ localidad +"')";
                              
                
                SqlCommand command = new SqlCommand(query, con1.cnn);
                command.ExecuteNonQuery();
                

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                salida = "No se pudo insertar" + ex.ToString();
            }
            con1.cnn.Close();


            Conexion con3 = new Conexion();
            string query3 = "SELECT id_domicilio FROM LPP.DOMICILIO WHERE calle = '"+calle+"' AND num = "+numero+" AND piso= "+Piso+" ";
            con3.cnn.Open();
            SqlCommand command3 = new SqlCommand(query3, con3.cnn);
            SqlDataReader lector3 = command3.ExecuteReader();
            int id_domicilio = lector3.GetInt32(0);
            con3.cnn.Close();

            return id_domicilio;

        }
      

        public void modificarDomicilio(string calle, int numero, int Piso, string depto, string localidad, int id_domicilio)
        {
            string salida = "Se modificó el DOMICILIO correctamente";
            con1.cnn.Open();
            try
            {
                string query = "UPDATE LPP.DOMICILIO SET calle = '" + calle +"',  numero = " + numero + ""+
                               "piso = " +Piso + ", depto = '"+ depto+"', localidad = '" +localidad+"'" +
                               "WHERE id_domicilio = "+id_domicilio+"";
                
                SqlCommand command = new SqlCommand(query, con1.cnn);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                salida = "No se pudo modificar" + ex.ToString();
            }

            con1.cnn.Close();
            
        }

        

        




    }
}
