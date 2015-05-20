using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaHotel;
using System.Security.Cryptography;
using System.Windows.Forms;

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

    }
}
