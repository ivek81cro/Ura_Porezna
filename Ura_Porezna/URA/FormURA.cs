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
            var datumPocetni = DateTime.Now.AddMonths(-1);
            datumOd.Value = new DateTime(datumPocetni.Year, datumPocetni.Month, 1);
            datumDo.Value = datumOd.Value.AddMonths(1).AddDays(-1);
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
            DialogResult result = MessageBox.Show("Stvarno želite obrisati cijelu tablicu URA?", "Potvrda", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string constring = "datasource=localhost;port=3306;username=root;password=pass123";
                string upitBrisi = "DELETE FROM poreznaura.ura WHERE Rbr<>100000;";
                MySqlConnection bazaspoj;
                MySqlCommand bazazapovjed;
                bazaspoj = new MySqlConnection(constring);
                bazazapovjed = new MySqlCommand(upitBrisi, bazaspoj);
                bazaspoj.Open();
                bazazapovjed.ExecuteReader();
                bazaspoj.Close();
            }
            else
            {
                return;
            }
        }
        UraStavka Zbroji()
        {
            double ukIznos = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ukIznos += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString()), 2);
            }

            double neoporezivo = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                neoporezivo += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString()), 2);
            }
            //ukIznos = ukIznos - neoporezivo;


            double osn5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn5 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString()), 2);
            }


            double osn13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn13 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString()), 2);
            }


            double osn25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn25 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[11].Value.ToString()), 2);
            }

            double por5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por5 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[13].Value.ToString()), 2);
            }

            double por13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por13 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[14].Value.ToString()), 2);
            }

            double por25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por25 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[15].Value.ToString()), 2);
            }

            label1.Text = "Ukupno: " + ukIznos.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label13.Text = "Neoporezivo: " + neoporezivo.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label2.Text = "Osn.5%: " + osn5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label3.Text = "Osn.13%: " + osn13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label4.Text = "Osn.25%: " + osn25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double osnUk = osn5 + osn13 + osn25;
            label5.Text = "Osn.Uk: " + osnUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label8.Text = "Por.5%: " + por5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label9.Text = "Por.13%: " + por13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label10.Text = "Por.25%: " + por25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            //obilazak gubitka u lipama radi strojnog racunanja
            //ukIznos = osn5 + osn13 + osn25 + por5 + por13 + por25;
            double pretPorUk = por5 + por13 + por25;
            label11.Text = "Pretpor.Uk.: " + pretPorUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));

            return new UraStavka()
            {
                Por5 = por5,
                Por13 = por13,
                Por25 = por25,
                Osn5 = osn5,
                Osn13 = osn13,
                Osn25 = osn25,
                Neoporezivo = neoporezivo,
                UkIznos = ukIznos,
                PretPorUk = pretPorUk
            };
        }
        //kreiraj xml
        private void Button1_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();

            URAIspPodIzBaze ispis = new URAIspPodIzBaze();
            ispis.Ispis(datumOdBox, datumDoBox, dataGridView1);
            UraStavka stavka = Zbroji();

            PopuniXml popuniXml = new PopuniXml();
            if (popuniXml.PopuniObrazac(datumOdBox, datumDoBox)) 
            {
                popuniXml.PopuniUkupno(stavka);
            }
        }
        //otvori csv
        private void Button2_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            UpisCsvURABaza upis = new UpisCsvURABaza();
            upis.Upis(put);

            URAIspPodIzBaze ispis = new URAIspPodIzBaze();

            ispis.Ispis(datumOdBox, datumDoBox, dataGridView1);
        }
        //brisi bazu
        private void Button3_Click(object sender, EventArgs e)
        {
            BrisiBazu();
        }
        //ispis iz baze
        private void Button4_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();

            URAIspPodIzBaze ispis = new URAIspPodIzBaze();

            ispis.Ispis(datumOdBox, datumDoBox, dataGridView1);
            _ = Zbroji();
        }
        //odobrenja zbirno
        private void Button5_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();           
            OdobrenjaZbirno();
        }
        //odobrenja pojedinacno
        private void Button6_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            OdobrenjaPojFilter();
            
            //odobrenja.zbrojiOdobrenja(dataGridView1);
        }
        //troskovi
        private void Button7_Click(object sender, EventArgs e)
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
