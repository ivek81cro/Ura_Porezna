using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Ura_Porezna
{
    public partial class FormURA : Form
    {
        private string datumOdBox;
        private string datumDoBox;
        protected string put;
        public FormURA()
        {
            InitializeComponent();
            var datumPocetni = DateTime.Now;
            datumOd.Value = datumPocetni.AddMonths(-1).AddDays(-DateTime.Now.Day + 1);
            datumDo.Value = datumPocetni.AddMonths(0).AddDays(-DateTime.Now.Day);
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            datumOd.TextChanged += DatumOd_TextChanged;
            datumDo.TextChanged += DatumDo_TextChanged;
            //MessageBox.Show("U windowsu: kut lijevo dolje kliknuti povećalo, utipkati services i kliknuti na to. " +
            //    "Popis je po abecednom redu, naći MYSQL, desni klik, odabrati start. Zatvori services.");
        }

        private void DatumDo_TextChanged(object sender, EventArgs e)
        {
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
        }

        private void DatumOd_TextChanged(object sender, EventArgs e)
        {
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
        }

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
            string upitBrisi = "DELETE FROM poreznaura.ura WHERE Rbr<>100000;";
            MySqlConnection bazaspoj;
            MySqlCommand bazazapovjed;
            MySqlDataReader citaj;
            bazaspoj = new MySqlConnection(constring);
            bazazapovjed = new MySqlCommand(upitBrisi, bazaspoj);
            bazaspoj.Open();
            citaj = bazazapovjed.ExecuteReader();
            bazaspoj.Close();
        }
        void zbroji()
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
        //kreiraj xml
        private void button1_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();

            PopuniXml xmlPopuna = new PopuniXml();
            xmlPopuna.PopuniObrazac(datumOdBox, datumDoBox);
            xmlPopuna.PopuniUkupno(datumOdBox, datumDoBox);
        }
        //otvori csv
        private void button2_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            BrisiBazu();
            UpisCsvURABaza upis = new UpisCsvURABaza();
            upis.Upis(put);

            URAIspPodIzBaze ispis = new URAIspPodIzBaze();

            ispis.ispis(datumOdBox, datumDoBox, dataGridView1);

            MessageBox.Show("Unešeno");
        }
        //brisi bazu
        private void button3_Click(object sender, EventArgs e)
        {
            BrisiBazu();
        }
        //ispis iz baze
        private void button4_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();

            URAIspPodIzBaze ispis = new URAIspPodIzBaze();

            ispis.ispis(datumOdBox,datumDoBox,dataGridView1);
            zbroji();
        }
        //odobrenja zbirno
        private void button5_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();           
            OdobrenjaZbirno();
        }
        //odobrenja pojedinacno
        private void button6_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            OdobrenjaPojFilter();
            
            //odobrenja.zbrojiOdobrenja(dataGridView1);
        }
        //troskovi
        private void button7_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();            
            Troškovi();

            foreach (DataGridViewRow Myrow in dataGridView1.Rows)
            {            
                if (Convert.ToInt32(Myrow.Cells[14].Value) != 0)
                {
                    Myrow.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }
    }
}
