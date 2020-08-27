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
        
        //kreiraj xml
        private void Button1_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();

            URAIspPodIzBaze ispis = new URAIspPodIzBaze();
            ispis.Ispis(datumOdBox, datumDoBox, dataGridView1);
            UraStavka stavka = new UraStavka();
            stavka = stavka.Zbroji(dataGridView1);

            PopuniLabeleZbroja(stavka);

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

            UraStavka stavka = new UraStavka();
            ispis.Ispis(datumOdBox, datumDoBox, dataGridView1);
            stavka = stavka.Zbroji(dataGridView1);
            PopuniLabeleZbroja(stavka);
        }

        private void PopuniLabeleZbroja(UraStavka stavka)
        {
            label1.Text = "Ukupno: " + stavka.UkIznos.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label2.Text = "Osn.5%: " + stavka.Osn5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label3.Text = "Osn.13%: " + stavka.Osn13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label4.Text = "Osn.25%: " + stavka.Osn25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label5.Text = "Osn.Uk: " + stavka.OsnovicaUkupno.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label8.Text = "Pretpor.Uk.: " + stavka.PretPorUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label9.Text = "Por.5%: " + stavka.Por5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label10.Text = "Por.13%: " + stavka.Por13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label11.Text = "Por.25%: " + stavka.Por25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            label13.Text = "Neoporezivo: " + stavka.Neoporezivo.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
        }
        //odobrenja zbirno
        private void Button5_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            Odobrenja od = new Odobrenja();
            UraStavka stavka = od.OdobrenjaZbirno(dataGridView1, datumOdBox, datumDoBox);
            PopuniLabeleZbroja(stavka);
        }


        //odobrenja pojedinacno
        private void Button6_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            Odobrenja od = new Odobrenja();
            UraStavka stavka = od.OdobrenjaPojedinacno(dataGridView1, datumOdBox, datumDoBox);
            PopuniLabeleZbroja(stavka);
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
        //Filter za odobrenja po nazivu dobavljača
        private void TxtDob_KeyUp(object sender, KeyEventArgs e)
        {
            int row = 0;
            Odobrenja od = new Odobrenja();

            if (dataGridView1.Rows.Count != 0)
            {
                if (dataGridView1.Columns[0].Name.ToString() != "Naziv_dobavljaca")
                    row = 4;

                Filter filter = new Filter();
                filter.Filtriraj(txtDob.Text, dataGridView1, row);

                if (row == 4)
                {
                    foreach (DataGridViewRow Myrow in dataGridView1.Rows)
                    {            //Here 2 cell is target value and 1 cell is Volume
                        if (Convert.ToInt32(Myrow.Cells[17].Value) != 0)// Or your condition 
                        {
                            Myrow.DefaultCellStyle.BackColor = Color.MistyRose;
                        }
                    }
                    PopuniLabeleZbroja(od.ZbrojiOdobrenja(dataGridView1));
                }

                if (row == 0)
                    PopuniLabeleZbroja(od.ZbrojiOdobrenjaZbirno(dataGridView1));
            }
        }
    }
}
