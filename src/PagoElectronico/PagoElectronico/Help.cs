using System.Security.Cryptography;
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
using Helper;
using readConfiguracion;
using PagoElectronico;

namespace Helper
{
    public static class Help
    {

        public static string Sha256(this string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }
        public static bool fechaMayorA(this string fecha, string AAAAMMDD, string msg)
        {
            int fechaLimite = Convert.ToInt32(AAAAMMDD.Replace('-', '0'));

            if (!(Convert.ToInt32(fecha.Replace('-', '0')) > fechaLimite))
            {
                MessageBox.Show("           " + msg + "\n" +
                                "Debe ser mayor a " + AAAAMMDD);
            }

            return Convert.ToInt32(fecha.Replace('-', '0')) > fechaLimite;
        }
        public static bool fechaMenorA(this string fecha, string AAAAMMDD, string msg)
        {
            int fechaLimite = Convert.ToInt32(AAAAMMDD.Replace('-', '0'));

            if (!(Convert.ToInt32(fecha.Replace('-', '0')) < fechaLimite))
            {
                MessageBox.Show("           " + msg + "\n" +
                                "Debe ser menor a " + AAAAMMDD);
            }

            return Convert.ToInt32(fecha.Replace('-', '0')) < fechaLimite;
        }
        public static bool VerificadorDeDeudas(int id_cliente)
        {
            Conexion con = new Conexion();
            string query = "SELECT i.num_cuenta FROM LPP.ITEMS_FACTURA i JOIN LPP.CUENTAS c ON c.num_cuenta = i.num_cuenta  "+
		                    "WHERE c.id_cliente = "+id_cliente+" AND i.facturado = 0  "+
		                    "GROUP BY i.num_cuenta "+
		                    "HAVING COUNT(i.id_item_factura) > 5";
            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector = command.ExecuteReader();
            if (lector.Read())
            {
                decimal num_cuenta = lector.GetDecimal(0);
                con.cnn.Close();
                Helper.Help.inhabilitarCuenta(num_cuenta);
                return true;
            }
            else
            {
                con.cnn.Close();
                return false;
            }
        }
        public static void inhabilitarCuenta(decimal cuenta)
        {
            Conexion con = new Conexion();
            con.cnn.Open();
            string query = "LPP.PRC_inhabilitar_cuenta_por_deudor";
            SqlCommand command = new SqlCommand(query, con.cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@num_cuenta",cuenta));
            command.ExecuteNonQuery();
            con.cnn.Close();
        }

    }
}
