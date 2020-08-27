using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Ura_Porezna
{
    class Troskovi
    {
        public UraStavka Troškovi(CustomDataGridView dataGridView1, string datumOdBox, string datumDoBox)
        {
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = string.Format("CALL troskovi('{0}','{1}');", datumOdBox, datumDoBox);
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    _ = adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            return ZbrojiTroskove(dataGridView1);
        }

        private UraStavka ZbrojiTroskove(CustomDataGridView dataGridView1)
        {
            UraStavka stavka = new UraStavka();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.UkIznos += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Osn5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Osn13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Osn25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString());
            }
            stavka.OsnovicaUkupno = stavka.Osn5 + stavka.Osn13 + stavka.Osn25;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.PretPorUk += Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Por5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[11].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Por13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[12].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Por25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[13].Value.ToString());
            }

            return stavka;
        }
    }
}

