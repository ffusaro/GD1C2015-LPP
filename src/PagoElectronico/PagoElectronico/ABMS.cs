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
        public string insertarCliente(string Nombre, string Apellido, string Tipo_ID, int Numero_ID, string Mail, DateTime Nacimiento, string Nacionalidad,int id_domicilio, string usuario, int habilitado)
        {
            con1.cnn.Open();
           
            string salida = "Se registro el Cliente correctamente";

            try
            {
                string query = "INSERT INTO LPP.CLIENTES" 
                    +" (num_doc, apellido, nombre, fecha_nac, mail, id_tipo_Doc, id_pais, id_domicilio, username, habilitado) " 
                    +"VALUES (" + Numero_ID + ", '" + Apellido + "', '" + Nombre + "', CONVERT(DATETIME, '" + Nacimiento.ToString("yyyy-MM-dd HH:MM:ss") + "', 103),"
                    + " '" + Mail + "', (select tipo_cod FROM LPP.TIPO_DOCS WHERE tipo_descr = '" + Tipo_ID + "')" +
                    ", (select id_pais from LPP.PAISES WHERE pais = '" + Nacionalidad + "' )," + id_domicilio + ", '"+usuario+"', "+ habilitado+") ";
                
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

                string query = "SELECT id_tipo_doc, num_doc FROM LPP.CLIENTES WHERE id_tipo_doc = (SELECT tipo_cod FROM LPP.TIPO_DOCS WHERE tipo_descr = '" + Tipo_ID + "') AND num_doc = " + Numero_ID + "";
                SqlCommand command = new SqlCommand(query, con1.cnn);
                dr = command.ExecuteReader();
                while (dr.Read())
                {

                    contador++;
                }
                dr.Close();

            }
            catch (Exception )
            {
                con1.cnn.Close();
                return contador;
                
            }
            con1.cnn.Close();
            //MessageBox.Show("El Cliente ya existe");
            return 0;
        }

        public string modificarCliente(string Nombre, string Apellido, string Tipo_ID, int Numero_ID, string Mail, DateTime Nacimiento,string Nacionalidad,string Username, int habilitado)
        {
            con1.cnn.Open();
            string salida = "Se modificó el Cliente correctamente";

            try
            {
                string query = "UPDATE LPP.CLIENTES SET fecha_nac = CONVERT(DATETIME, '" + Nacimiento.ToString("yyyy-MM-dd HH:MM:ss")+ "', 103)"
                    +", mail = '" + Mail 
                    + "', apellido = '" + Apellido 
                    + "', nombre = '" + Nombre 
                    + "',  id_pais=(select id_pais from LPP.PAISES where pais like '%" + Nacionalidad +"')"
                    +", id_tipo_doc = ( select tipo_cod from LPP.TIPO_DOCS where tipo_descr='"+Tipo_ID +"'"
                    + "), num_doc = "+ Numero_ID
                    +" , habilitado = " + habilitado+" WHERE username = '"+Username +"'";
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
                string query = "UPDATE LPP.CLIENTES SET habilitado = 0 WHERE id_tipo_doc = (select tipo_cod from LPP.TIPO_DOCS where tipo_descr = '" +Tipo_ID + "') AND num_doc = '" + Numero_ID + "'";
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
                string query = "INSERT INTO LPP.DOMICILIOS (calle, num, piso, depto, localidad)" +
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
            string query3 = "SELECT id_domicilio FROM LPP.DOMICILIOS WHERE calle = '"+calle+"' AND num = "+numero+"AND piso= "+Piso+" ";
            con3.cnn.Open();
            SqlCommand command3 = new SqlCommand(query3, con3.cnn);
            SqlDataReader lector3 = command3.ExecuteReader();
            lector3.Read();
            int id_domicilio = lector3.GetInt32(lector3.GetOrdinal("id_domicilio"));
            con3.cnn.Close();

            return id_domicilio;

        }
      

        public void modificarDomicilio(string calle, int numero, int Piso, string depto, string localidad, int id_domicilio)
        {
            string salida = "Se modificó el DOMICILIO correctamente";
            con1.cnn.Open();
            try
            {
                string query = "UPDATE LPP.DOMICILIOS SET calle = '" + calle +"',  num = " + numero + ","+
                               " piso = " +Piso + ", depto = '"+ depto+"', localidad = '" +localidad+"'" +
                               " WHERE id_domicilio = "+id_domicilio+"";
                
                SqlCommand command = new SqlCommand(query, con1.cnn);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                salida = "No se pudo modificar" + ex.ToString();
            }

            con1.cnn.Close();
            
        }

        public void ingresarAlSistema(string user, Login.LogIn log, MenuPrincipal mp, Form fm, string rol){
            
            Conexion con = new Conexion();

            MessageBox.Show("Bienvenido/a  " + user );

            if (getRolUser(user) == "Administrador")
            {
                mp.Show();
                mp.cargarUsuario(user, rol, log);
                fm.Hide();
            }
            else
            {
                if (rol != "Administrador")
                {
                    if (verificoSiDebe(user))
                    {
                        DialogResult dialogResult = MessageBox.Show("Alguna de sus cuentas se encuentra inhabilitada. Puede habilitarla cambiandole el tipo de cuenta o extendiendo la suscripcion actual ¿Desea habilitarla ahora? ", "Cuentas", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            MessageBox.Show("Le recordamos que solo podra habilitar cuentas que hayan sido inhabilitadas por vencimiento de la duracion de la cuenta.");
                            ABM_Cuenta.Buscar bc = new ABM_Cuenta.Buscar(0, user);
                            mp.Show();
                            mp.cargarUsuario(user, rol, log);
                            bc.Show();
                            con.cnn.Close();
                            fm.Hide();
                        }
                        if (dialogResult == DialogResult.No)
                        {
                            mp.cargarUsuario(user, rol, log);
                            mp.Show();
                            con.cnn.Close();
                            fm.Hide();
                        }
                    }
                }
                else
                {
                    mp.cargarUsuario(user, rol, log);
                    mp.Show();
                    con.cnn.Close();
                }
            }
        }
    

        private bool verificoSiDebe(string user)
        {

            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "SELECT num_cuenta FROM LPP.CUENTAS WHERE id_cliente= " + getIdCliente(user) + " AND id_estado = 4";
            bool debe = false;
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();

            if (lector.Read())
            {
                debe = true;
                con.cnn.Close();

            }

            con.cnn.Close();
            return debe;
        }

        private string getRolUser(string usuario)
        {
            Conexion con = new Conexion();
            //OBTENGO USUARIO DEL ROL
            con.cnn.Open();
            string query = "SELECT R.nombre FROM LPP.ROLESXUSUARIO U JOIN LPP.ROLES R ON R.id_rol=U.rol WHERE U.username = '" + usuario + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            string rol = lector.GetString(0);
            con.cnn.Close();
            return rol;
        }

        private int getIdCliente(string user)
        {
            Conexion con = new Conexion();
            //OBTENGO ID DE CLIENTE
            con.cnn.Open();
            string query = "SELECT id_cliente FROM LPP.CLIENTES WHERE username = '" + user + "'";
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            int id_cliente = lector.GetInt32(0);
            con.cnn.Close();
            return id_cliente;

        }

        




    }
}
