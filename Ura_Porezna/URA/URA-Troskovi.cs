using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;

namespace Ura_Porezna
{
    partial class FormURA
    {
        void Troškovi()
        {            
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = "CALL troskovi('" + datumOdBox + "','" + datumDoBox + "');";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    conn.Close();
                }
            }
            zbrojiTroskove();
        }

        void zbrojiTroskove()
        {
            double ukIznos = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ukIznos += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
            }
            label1.Text = "Iznos: " + ukIznos.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double osn5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString());
            }
            label2.Text = "Osn.5%: " + osn5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double osn13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }
            label3.Text = "Osn.13%: " + osn13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double osn25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString());
            }
            double pretporUk = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                pretporUk += Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString());
            }
            label8.Text = "Pretpor.Uk: " + pretporUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label4.Text = "Osn.25%: " + osn25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double osnUk = osn5 + osn13 + osn25;
            label5.Text = "Osn.Uk: " + osnUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[11].Value.ToString());
            }
            label9.Text = "Pretpor.5%: " + por5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[12].Value.ToString());
            }
            label10.Text = "Pretpor.13%: " + por13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[13].Value.ToString());
            }
            label11.Text = "Pretpor.25%: " + por25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
        }
    }
}
