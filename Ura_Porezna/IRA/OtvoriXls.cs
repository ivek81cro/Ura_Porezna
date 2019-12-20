using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class OtvoriXls
    {
        public static void Otvori(ref string put)
        {
            ConvertXlsToCsv.Convert(ref put);

            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            MySqlConnection con = new MySqlConnection(constring);
            string query = string.Format("SELECT Rbr FROM poreznaura.ira ORDER BY Rbr DESC LIMIT 1;");
            con.Open();

            MySqlCommand cmd = new MySqlCommand(query, con);
            Object result = cmd.ExecuteScalar();
            int zadnjiRed = Convert.ToInt32(result) == 0 ? -1 : Convert.ToInt32(result);

            query = "INSERT INTO poreznaura.ira (Rbr, datum_rn, br_rn, kupac, " +
                "iznos_uk, osn0, osn5, pdv5, osn13, " +
                "pdv13, osn25, pdv25, pdv_uk, storno_iz)" +
                "VALUES (@Rbr, @Datum_racuna, @Broj_racuna, @Kupac, @Iznos, " +
                "@Osn0, @Osn5, @Pdv5, @Osn13, @Pdv13, " +
                "@Osn25, @Pdv25, @Pdv_uk, @storno_iz);";

            int rowsAffected = 0;

            try
            {
                string[] lines = File.ReadAllLines(put);
                foreach (string line in lines)
                {
                    string[] text = line.Split(';', '\n');
                    if (text[0] == "Rbr" || text[0] == "") continue;
                    DateTime dt = DateTime.Parse(text[5]);
                    text[5] = dt.ToString("yyyy-MM-dd");
                    int trenutniRed = Int32.Parse(text[0]);
                    if (zadnjiRed > 0 && zadnjiRed >= trenutniRed) continue;

                    cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Rbr", text[0]);
                    cmd.Parameters.AddWithValue("@Datum_racuna", text[5].ToString());
                    cmd.Parameters.AddWithValue("@Broj_racuna", text[2].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Kupac", text[8].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Iznos", Convert.ToDouble(text[10].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Osn0", Convert.ToDouble(text[13].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Osn5", Convert.ToDouble(text[14].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Pdv5", Convert.ToDouble(text[15].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Osn13", Convert.ToDouble(text[18].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Pdv13", Convert.ToDouble(text[19].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Osn25", Convert.ToDouble(text[22].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Pdv25", Convert.ToDouble(text[23].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Pdv_uk", Convert.ToDouble(text[24].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@storno_iz", Convert.ToInt32(text[4].ToString().Trim()));

                    rowsAffected = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
            MessageBox.Show("Unešeno");
        }
    }
}
