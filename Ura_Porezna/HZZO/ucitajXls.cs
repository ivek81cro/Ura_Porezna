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
        public void Otvori(int godina, CustomDataGridView dataGridView1)
        {
            ConvertXlsToCsv.Convert(ref put, 0);

            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            MySqlConnection con = new MySqlConnection(constring);
            string query = string.Format("INSERT INTO poreznaura.hzzo (datum, dokument, brojRn, " +
                "datumRn, izvor, opis, iznos, placeniIznos, id) " +
                "VALUES (@datum, @dokument, @brojRn, @datumRn, @izvor, " +
                "@opis, @iznos, @placeniIznos, @identifikator);");
            con.Open();
            int rowsAffected = 0;
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

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@datum", text[0].ToString().Substring(0, 10));
                    cmd.Parameters.AddWithValue("@dokument", text[1].ToString());
                    cmd.Parameters.AddWithValue("@brojRn", brRn);
                    cmd.Parameters.AddWithValue("@datumRn", text[3].ToString().Substring(0, 10));
                    cmd.Parameters.AddWithValue("@izvor", text[4].ToString().Trim());
                    cmd.Parameters.AddWithValue("@opis", text[5].ToString().Trim());
                    cmd.Parameters.AddWithValue("@iznos", Convert.ToDouble(text[6].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@placeniIznos", Convert.ToDouble(text[7].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@identifikator", identifikator);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                query = string.Format("SELECT * FROM poreznaura.hzzo");
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];
                    }
                }
                query = string.Format("CALL poreznaura.placeno();");
                MySqlCommand call = new MySqlCommand(query, con);
                call.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
