using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class UpisCsvURABaza
    {
        public void Upis(string put)
        {
            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            MySqlConnection con = new MySqlConnection(constring);
            string query = string.Format("SELECT Rbr FROM poreznaura.ura ORDER BY Rbr DESC LIMIT 1;");
            con.Open();

            MySqlCommand cmd = new MySqlCommand(query, con);
            Object result = cmd.ExecuteScalar();
            int zadnjiRed = Convert.ToInt32(result) == 0 ? -1 : Convert.ToInt32(result);

            Poruka p = new Poruka();
            p.Prikazi();

            ConvertXlsToCsv.Convert(ref put, zadnjiRed);

            if (put == null) return;

            query = string.Format("INSERT INTO poreznaura.ura (Rbr, Datum_racuna, Broj_racuna, Za_uplatu, " +
                "Naziv_dobavljaca, Sjediste_dobavljaca, OIB, Iznos_s_porezom, Porezna_osn0, " +
                "Porezna_osn5, Porezna_osn13, Porezna_osn25, Ukupni_pretporez, por5, por13, por25, " +
                "br_primke, storno, odobr) " +
                "VALUES (@Rbr, @Datum_racuna, @Broj_racuna, @Za_uplatu, @Naziv_dobavljaca, " +
                "@Sjediste_dobavljaca, @OIB, @Iznos_s_porezom, @Porezna_osn0, @Porezna_osn5, " +
                "@Porezna_osn13, @Porezna_osn25, @Ukupni_pretporez, @por5, @por13, @por25, " +
                "@br_primke, @storno, @odobr);");

            int rowsAffected = 0;
            try
            {
                string[] lines = File.ReadAllLines(put);
                foreach (string line in lines)
                {
                    string[] text = line.Split(';', '\n');
                    if (text[0] == "Rbr" || text[0] == "") continue;
                    int trenutniRed = Int32.Parse(text[0]);
                    if (zadnjiRed > 0 && zadnjiRed >= trenutniRed) continue;


                    DateTime dt1 = DateTime.Parse(text[2]);
                    text[6] = dt1.ToString("yyyy-MM-dd");

                    cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Rbr", text[1].ToString());
                    cmd.Parameters.AddWithValue("@Datum_racuna", text[6].ToString());
                    cmd.Parameters.AddWithValue("@Broj_racuna", text[3].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Za_uplatu", Convert.ToDouble(text[11].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Naziv_dobavljaca", text[12].ToString().Trim());
                    cmd.Parameters.AddWithValue("@br_primke", Convert.ToDouble(text[13].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Sjediste_dobavljaca", text[16].ToString().Trim());
                    cmd.Parameters.AddWithValue("@OIB", text[17].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Iznos_s_porezom", Convert.ToDouble(text[18].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Porezna_osn0", Convert.ToDouble(text[19].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Porezna_osn5", Convert.ToDouble(text[20].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Porezna_osn13", Convert.ToDouble(text[24].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Porezna_osn25", Convert.ToDouble(text[28].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Ukupni_pretporez", Convert.ToDouble(text[30].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@por5", Convert.ToDouble(text[21].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@por13", Convert.ToDouble(text[25].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@por25", Convert.ToDouble(text[29].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@storno", text[5].ToString().Trim());
                    cmd.Parameters.AddWithValue("@odobr", text[37].ToString().Trim());

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
            p.Zatvori();
        }
    }
}
