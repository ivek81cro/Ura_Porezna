using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Ura_Porezna
{
    public partial class FormURA
    {
        public void OdobrenjaZbirno()
        {
            string datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            string datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = "CALL odobrenjaZbirno('" + datumOdBox + "','" + datumDoBox + "'); ";
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
            zbrojiOdobrenjaZbirno();
        }
        public void OdobrenjaPojedinacno()
        {
            string datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            string datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = "CALL odobrenjaPojedinacno('" + datumOdBox + "','" + datumDoBox + "');";
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
            foreach (DataGridViewRow Myrow in dataGridView1.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                if (Convert.ToInt32(Myrow.Cells[17].Value) != 0)// Or your condition 
                {
                    Myrow.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
            zbrojiOdobrenja();
        }
        public void OdobrenjaPojFilter()
        {
            string datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            string datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = "CALL odobrenja('" + datumOdBox + "' , '" + datumDoBox +
                "' , '" + txtDob.Text + "');";
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
            foreach (DataGridViewRow Myrow in dataGridView1.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                if (Convert.ToInt32(Myrow.Cells[17].Value) != 0)// Or your condition 
                {
                    Myrow.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
            zbrojiOdobrenja();
        }
        void zbrojiOdobrenja()
        {
            double ukIznos = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ukIznos += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString());
            }
            double neoporezivo = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                neoporezivo += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }
            //ukIznos = ukIznos - neoporezivo;
            label1.Text = "Ukupno: " + ukIznos.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            double osn5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString());
            }
            label2.Text = "Osn.5%: " + osn5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            double osn13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString());
            }
            label3.Text = "Osn.13%: " + osn13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            double osn25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[11].Value.ToString());
            }

            label4.Text = "Osn.25%: " + osn25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double osnUk = osn5 + osn13 + osn25;
            label5.Text = "Osn.Uk: " + osnUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[13].Value.ToString());
            }
            label8.Text = "Por.5%: " + por5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[14].Value.ToString());
            }
            label9.Text = "Por.13%: " + por13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[15].Value.ToString());
            }
            label10.Text = "Por.25%: " + por25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            //obilazak gubitka u lipama radi strojnog racunanja
            //ukIznos = osn5 + osn13 + osn25 + por5 + por13 + por25;
            double pretPorUk = por5 + por13 + por25;
            label11.Text = "Pretpor.Uk.: " + pretPorUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
        }
        void zbrojiOdobrenjaZbirno()
        {
            double ukIznos = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ukIznos += Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            double osnovica = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osnovica += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            //ukIznos = ukIznos - neoporezivo;
            label1.Text = "Ukupno: " + ukIznos.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            double osn5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
            }
            label2.Text = "Osn.5%: " + osn5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            double osn13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
            }
            label3.Text = "Osn.13%: " + osn13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            double osn25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value.ToString());
            }

            label4.Text = "Osn.25%: " + osn25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double osnUk = osn5 + osn13 + osn25;
            label5.Text = "Osn.Uk: " + osnUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }
            label8.Text = "Por.5%: " + por5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString());
            }
            label9.Text = "Por.13%: " + por13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString());
            }
            label10.Text = "Por.25%: " + por25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            //obilazak gubitka u lipama radi strojnog racunanja
            //ukIznos = osn5 + osn13 + osn25 + por5 + por13 + por25;
            double pretPorUk = por5 + por13 + por25;
            label11.Text = "Pretpor.Uk.: " + pretPorUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
        }
    }
}
