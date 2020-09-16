using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class UcitajXls
    {
        string put;
        public void Otvori(int godina)
        {
            ConvertXlsToCsv.Convert(ref put);

            if (put == null)
                return;

            int rowsAffected = 0;
            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            MySqlConnection con = new MySqlConnection(constring);
            string query = string.Format("INSERT INTO poreznaura.hzzo (datum, dokument, brojRn, " +
                "datumRn, izvor, opis, iznos, placeniIznos, id) " +
                "VALUES (@datum, @dokument, @brojRn, @datumRn, @izvor, " +
                "@opis, @iznos, @placeniIznos, @identifikator);");

            con.Open();
            try
            {
                string[] lines = File.ReadAllLines(put);
                for (int i = 9; i < lines.Length - 1; ++i)
                {
                    string[] text = lines[i].Split(';', '\n');
                    string[] temp = text[2].Split('-');
                    if (text[5].Contains("CEZ"))
                        continue;
                    if (Int32.Parse(temp[2].Split('/')[1]) != godina)
                        continue;
                    string identifikator = temp[0] + text[1].Split('-')[2] + text[4].Split('/')[0];
                    int brRn = Int32.Parse(temp[0]);

                    long exists = 0;
                    MySqlConnection con2 = new MySqlConnection(constring);
                    con2.Open();
                    using (MySqlCommand com = new MySqlCommand($"SELECT COUNT(*) FROM poreznaura.hzzo WHERE id='{identifikator.Trim()}'", con2)) {
                        exists = (long)com.ExecuteScalar();
                    }
                    con2.Close();

                    if (exists != 0)
                        continue;

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@datum", DateTime.ParseExact(text[0].ToString().Substring(0, 10), "dd.MM.yyyy", null));
                    cmd.Parameters.AddWithValue("@dokument", text[1].ToString());
                    cmd.Parameters.AddWithValue("@brojRn", brRn);
                    cmd.Parameters.AddWithValue("@datumRn", text[3].ToString().Substring(0, 10));
                    cmd.Parameters.AddWithValue("@izvor", text[4].ToString().Trim());
                    cmd.Parameters.AddWithValue("@opis", text[5].ToString().Trim());
                    cmd.Parameters.AddWithValue("@iznos", Convert.ToDouble(text[6].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@placeniIznos", Convert.ToDouble(text[7].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@identifikator", identifikator);

                    rowsAffected += cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {                
                query = string.Format("CALL poreznaura.placeno();");
                MySqlCommand call = new MySqlCommand(query, con);
                call.ExecuteNonQuery();
                con.Close();
                MessageBox.Show($"Unešeno je {rowsAffected} redova u bazu");
            }
        }
    }
}
