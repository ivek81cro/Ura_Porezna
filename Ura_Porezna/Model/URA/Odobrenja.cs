using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class Odobrenja
    {
        public UraStavka OdobrenjaZbirno(DataGridView dataGridView1, string datumOdBox, string datumDoBox)
        {
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = string.Format("CALL odobrenjaZbirno('{0}','{1}');", datumOdBox, datumDoBox);
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            return ZbrojiOdobrenjaZbirno(dataGridView1);
        }
        public UraStavka OdobrenjaPojedinacno(DataGridView dataGridView1, string datumOdBox, string datumDoBox)
        {
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = string.Format("CALL odobrenjaPojedinacno('{0}','{1}');", datumOdBox, datumDoBox);
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            foreach (DataGridViewRow Myrow in dataGridView1.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                if (Convert.ToInt32(Myrow.Cells[17].Value) != 0)// Or your condition 
                {
                    Myrow.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
            return ZbrojiOdobrenja(dataGridView1);
        }

        public UraStavka ZbrojiOdobrenja(DataGridView dataGridView1)
        {
            UraStavka stavka = new UraStavka();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.UkIznos += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Neoporezivo += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Osn5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Osn13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Osn25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[11].Value.ToString());
            }
            stavka.OsnovicaUkupno = stavka.Osn5 + stavka.Osn13 + stavka.Osn25;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Por5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[13].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Por13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[14].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Por25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[15].Value.ToString());
            }

            //obilazak gubitka u lipama radi strojnog racunanja
            //ukIznos = osn5 + osn13 + osn25 + por5 + por13 + por25;
            stavka.PretPorUk = stavka.Por5 + stavka.Por13 + stavka.Por25;

            return stavka;
        }
        public UraStavka ZbrojiOdobrenjaZbirno(DataGridView dataGridView1)
        {
            UraStavka stavka = new UraStavka();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.UkIznos += Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.OsnovicaUkupno += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Osn5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Osn13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Osn25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value.ToString());
            }
            stavka.OsnovicaUkupno = stavka.Osn5 + stavka.Osn13 + stavka.Osn25;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Por5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Por13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stavka.Por25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString());
            }
            stavka.PretPorUk = stavka.Por5 + stavka.Por13 + stavka.Por25;

            return stavka;
        }
    }
}
