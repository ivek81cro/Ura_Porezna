﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Ura_Porezna
{
    public partial class FormIRA : Form
    {
        string datumOdBox;
        string datumDoBox;
        public FormIRA()
        {            
            InitializeComponent();
            var datumPocetni = DateTime.Now;
            datumOd.Value = datumPocetni.AddMonths(-1).AddDays(-DateTime.Now.Day+1);
            datumDo.Value = datumPocetni.AddMonths(0).AddDays(-DateTime.Now.Day);
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            datumOd.TextChanged += DatumOd_TextChanged;
            datumDo.TextChanged += DatumDo_TextChanged;
        }

        private void DatumDo_TextChanged(object sender, EventArgs e)
        {
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
        }

        private void DatumOd_TextChanged(object sender, EventArgs e)
        {
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
        }

        string put;

        void BrisiDatagrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
        }

        void BrisiBazu()
        {
            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            string upitBrisi = "DELETE FROM poreznaura.ira WHERE Rbr<>100000;";
            MySqlConnection bazaspoj;
            MySqlCommand bazazapovjed;
            MySqlDataReader citaj;
            bazaspoj = new MySqlConnection(constring);
            bazazapovjed = new MySqlCommand(upitBrisi, bazaspoj);
            bazaspoj.Open();
            citaj = bazazapovjed.ExecuteReader();
            bazaspoj.Close();
        }

        void obojiRedove()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells[4].Value) == Convert.ToInt32(row.Cells[14].Value))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(204, 255, 153);
                }
                if (Convert.ToInt32(row.Cells[13].Value) != 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
            
        }    

        void zbroji()
        {
            label12.ResetText(); label13.ResetText(); label14.ResetText(); label15.ResetText(); label20.ResetText();
            label1.ResetText(); label2.ResetText(); label3.ResetText(); label4.ResetText(); label21.ResetText();
            double ukIznos = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ukIznos += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
            }
            double neoporezivo = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                neoporezivo += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }
            //ukIznos = ukIznos - neoporezivo;
            label19.Text = "Ukupno: " + ukIznos.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            double osn5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value.ToString());
            }
            label18.Text = "Osn.5%: " + osn5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            double osn13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }
            label17.Text = "Osn.13%: " + osn13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            double osn25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString());
            }
            label16.Text = "Osn.25%: " + osn25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double osnUk = osn5 + osn13 + osn25;
            label20.Text = "Osn.Uk: " + osnUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString());
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
                por25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[11].Value.ToString());
            }
            label10.Text = "Por.25%: " + por25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            //obilazak gubitka u lipama radi strojnog racunanja
            //ukIznos = osn5 + osn13 + osn25 + por5 + por13 + por25;
            double pretPorUk = por5 + por13 + por25;
            label11.Text = "Pdv.Uk.: " + pretPorUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
        }

        void filterPodataka()
        {
            Filter filt = new Filter();
            filt.Filtriraj(datumOdBox, datumDoBox, textFilter.Text, dataGridView1);
        }

        void izracunPDV()
        {
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            string constring = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";            
            string upit = string.Format("CALL porez('{0}','{1}');", datumOdBox, datumDoBox);
            MySqlConnection bazaspoj = new MySqlConnection(constring);
            MySqlCommand bazazapovjed = new MySqlCommand(upit, bazaspoj);
            MySqlDataReader citaj;
            try
            {
                bazaspoj.Open();
                citaj = bazazapovjed.ExecuteReader();
                if (citaj.Read())
                {
                    label1.Text = "Por.Osn.5: " + citaj.GetDouble("osn5").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label15.Text = "Por.5: " + citaj.GetDouble("ppor5").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label2.Text = "Por.Osn.13: " + citaj.GetDouble("osn13").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label14.Text = "Por.13: " + citaj.GetDouble("ppor13").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label3.Text = "Por.Osn.25: " + citaj.GetDouble("osn25").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label13.Text = "Por.25: " + citaj.GetDouble("ppor25").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label4.Text = "Por.Osn.Uk.: " +  citaj.GetDouble("osnPretPorUk").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label20.Text = "Pretpor.Uk.: " + citaj.GetDouble("ukpretpor").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label5.Text = "Pretpor.Osn.0: " + citaj.GetDouble("porosn0").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label19.Text = "Pretpor.0: " + (0.00).ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label8.Text = "Pretpor.Osn.5: " + citaj.GetDouble("porosn5").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label18.Text = "Pretpor.5: " + citaj.GetDouble("por5").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label9.Text = "Pretpor.Osn.13: " + citaj.GetDouble("porosn13").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label17.Text = "Pretpor.13: " + citaj.GetDouble("por13").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label10.Text = "Pretpor.Osn.25: " + citaj.GetDouble("porosn25").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label16.Text = "Pretpor.25: " + citaj.GetDouble("por25").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label11.Text = "Pretpor.Osn.Uk.: " + citaj.GetDouble("ukporosn").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label12.Text = "Uk.por.: " + citaj.GetDouble("ukpor").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                    label21.Text = "Razlika za pl.: " + citaj.GetDouble("za_platit").ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
                }
                bazaspoj.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(upit, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            OtvoriXls.Otvori(ref put);
            Ispis.Ispisi(datumOdBox, datumDoBox, dataGridView1);
            obojiRedove();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            BrisiBazu();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            Ispis.Ispisi(datumOdBox, datumDoBox, dataGridView1);
            zbroji();
            obojiRedove();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            izracunPDV();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            filterPodataka();
            zbroji();
            obojiRedove();
        }
    }
}
