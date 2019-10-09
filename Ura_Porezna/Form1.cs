using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;

namespace Ura_Porezna
{
    public partial class Form1 : Form
    {
        string datumOdBox;
        string datumDoBox;
        public Form1()
        {
            InitializeComponent();
            var datumPocetni = DateTime.Now;
            datumOd.Value = datumPocetni.AddMonths(-1).AddDays(-DateTime.Now.Day + 1);
            datumDo.Value = datumPocetni.AddMonths(0).AddDays(-DateTime.Now.Day);
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            MessageBox.Show("U windowsu: kut lijevo dolje kliknuti povećalo, utipkati services i kliknuti na to. " +
                "Popis je po abecednom redu, naći MYSQL, desni klik, odabrati start. Zatvori services.");
        }

        string path;
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

        void IspisIzBaze()
        {
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            string connStr = "datasource=localhost;port=3306;username=root;password=pass123";
            string query = "SELECT * FROM poreznaura.ura WHERE Datum_racuna BETWEEN '"
                + datumOdBox + "' AND '" + datumDoBox + "' ;";
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
            {
                if (Convert.ToInt32(Myrow.Cells[17].Value) != 0)
                {
                    Myrow.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }

        void OdobrenjaZbirno()
        {
            label8.Text = "0.00"; label9.Text = "0.00"; label10.Text = "0.00"; label11.Text = "0.00";
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = "CALL odobrenjaZbirno('"+datumOdBox+"','"+datumDoBox+"'); ";
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
            double ukIznos = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ukIznos += Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            label1.Text = "Iznos: " + ukIznos.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
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
            double pretporUk = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                pretporUk += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString());
            }
            label11.Text = "Pretpor.Uk: " + pretporUk;
            label4.Text = "Osn.25%: " + osn25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double osnUk = osn5 + osn13 + osn25;
            label5.Text = "Osn.Uk: " + osnUk.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }
            label8.Text = "Pretpor.5%: " + por5.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por13 += Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString());
            }
            label9.Text = "Pretpor.13%: " + por13.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
            double por25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por25 += Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString());
            }
            label10.Text = "Pretpor.25%: " + por25.ToString("C", CultureInfo.CreateSpecificCulture("hr-HR"));
        }

        void Troškovi()
        {
            label8.Text = "0.00"; label9.Text = "0.00"; label10.Text = "0.00"; label11.Text = "0.00";
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = "CALL troskovi('"+datumOdBox+"','"+datumDoBox+"');";
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

        void OdobrenjaPojedinacno()
        {
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            string connStr = "datasource=localhost;port=3306;username=root;password=pass123";
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
        }

        void OdobrenjaPojFilter()
        {
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
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
        }

        void PopuniObrazac()
        {
            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            string upit;
            MySqlConnection bazaspoj;
            MySqlCommand bazazapovjed;
            MySqlDataReader citaj;

            string UUID = Guid.NewGuid().ToString();
            SaveFileDialog savFile = new SaveFileDialog();
            savFile.Filter = "XML|*.xml";
            if (savFile.ShowDialog() == DialogResult.OK)
            {
                path = savFile.FileName;
                constring = "datasource=localhost;port=3306;username=root;password=pass123";
                upit = "select * from poreznaura.obveznik;";
                bazaspoj = new MySqlConnection(constring);
                bazazapovjed = new MySqlCommand(upit, bazaspoj);

                bazaspoj.Open();
                citaj = bazazapovjed.ExecuteReader();
                citaj.Read();
                //-----------------------------------------------STRANICA A-----------------------------------------------------------                
                XNamespace ns = "http://e-porezna.porezna-uprava.hr/sheme/zahtjevi/ObrazacURA/v1-0";
                XNamespace ns2 = "http://e-porezna.porezna-uprava.hr/sheme/Metapodaci/v2-0";
                XNamespace ns3 = "";

                XDocument xDoc = new XDocument(

                            new XElement(ns + "ObrazacURA",
                                new XAttribute("verzijaSheme", "1.0"),
                                new XElement(ns2 + "Metapodaci",
                                    new XElement(ns2 + "Naslov", "Obrazac U-RA",
                                        new XAttribute("dc", "http://purl.org/dc/elements/1.1/title")),
                                    new XElement(ns2 + "Autor", citaj["ime"].ToString().ToUpper() + " " + citaj["prezime"].ToString().ToUpper(),
                                        new XAttribute("dc", "http://purl.org/dc/elements/1.1/creator")),
                                    new XElement(ns2 + "Datum", DateTime.Now.ToString("s"),
                                        new XAttribute("dc", "http://purl.org/dc/elements/1.1/date")),
                                    new XElement(ns2 + "Format", "text/xml",
                                        new XAttribute("dc", "http://purl.org/dc/elements/1.1/format")),
                                    new XElement(ns2 + "Jezik", "hr-HR",
                                        new XAttribute("dc", "http://purl.org/dc/elements/1.1/language")),
                                    new XElement(ns2 + "Identifikator", UUID,
                                        new XAttribute("dc", "http://purl.org/dc/elements/1.1/identifier")),
                                    new XElement(ns2 + "Uskladjenost", "ObrazacURA-v1-0",
                                        new XAttribute("dc", "http://purl.org/dc/terms/conformsTo")),
                                    new XElement(ns2 + "Tip", "Elektronički obrazac",
                                        new XAttribute("dc", "http://purl.org/dc/elements/1.1/type")),
                                    new XElement(ns2 + "Adresant", "Ministarstvo Financija, Porezna uprava, Zagreb")),
                                new XElement(ns + "Zaglavlje",
                                    new XElement(ns + "Razdoblje",
                                        new XElement(ns + "DatumOd", datumOd.Value.ToString("yyyy-MM-dd")),
                                        new XElement(ns + "DatumDo", datumDo.Value.ToString("yyyy-MM-dd"))),
                                    new XElement(ns + "Obveznik",
                                        new XElement(ns + "OIB", citaj["oib"].ToString()),
                                        new XElement(ns + "Ime", citaj["ime"].ToString().ToUpper()),
                                        new XElement(ns + "Prezime", citaj["prezime"].ToString().ToUpper()),
                                        new XElement(ns + "Adresa",
                                            new XElement(ns + "Mjesto", citaj["mjesto"].ToString().ToUpper()),
                                            new XElement(ns + "Ulica", citaj["ulica"].ToString().ToUpper()),
                                            new XElement(ns + "Broj", citaj["broj"].ToString()),
                                            new XElement(ns + "DodatakKucnomBroju", citaj["dodatakBroju"].ToString().ToUpper())),
                                        new XElement(ns + "PodrucjeDjelatnosti", "G"),
                                        new XElement(ns + "SifraDjelatnosti", "4773")),
                                    new XElement(ns + "ObracunSastavio",
                                        new XElement(ns + "Ime", citaj["ime"].ToString().ToUpper()),
                                        new XElement(ns + "Prezime", citaj["prezime"].ToString().ToUpper()))),
                                new XElement(ns + "Tijelo",
                                    new XElement(ns + "Racuni", null),
                                    new XElement(ns + "Ukupno", null))));

                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                //xDoc.Save(xWrite);
                //xWrite.Close();
                bazaspoj.Close();
                // Save to Disk
                xDoc.Save(path);

                //---------------------------------------------------------------RACUNI--------------------------------------------------------
                constring = "datasource=localhost;port=3306;username=root;password=pass123";
                upit = "select * from poreznaura.ura where Datum_racuna between '"
                    + datumOdBox + "' AND '" + datumDoBox + "';";
                bazaspoj = new MySqlConnection(constring);
                bazazapovjed = new MySqlCommand(upit, bazaspoj);
                bazaspoj.Open();
                citaj = bazazapovjed.ExecuteReader();
                XDocument doc = XDocument.Load(path);
                while (citaj.Read())
                {
                    doc.Element(ns + "ObrazacURA").Element(ns + "Tijelo").Element(ns + "Racuni").Add(new XElement(ns + "R",
                                                        new XElement(ns + "R1", citaj["Rbr"].ToString()),
                                                        new XElement(ns + "R2", citaj["Broj_racuna"].ToString()),
                                                        new XElement(ns + "R3", citaj["Datum_racuna"]),
                                                        new XElement(ns + "R4", citaj["Naziv_dobavljaca"].ToString()),
                                                        new XElement(ns + "R5", citaj["Sjediste_dobavljaca"].ToString()),
                                                        new XElement(ns + "R6", "1"),
                                                        new XElement(ns + "R7", citaj["OIB"].ToString()),
                                                        new XElement(ns + "R8", citaj["Porezna_osn5"]),
                                                        new XElement(ns + "R9", citaj["Porezna_osn13"]),
                                                        new XElement(ns + "R10", citaj["Porezna_osn25"]),
                                                        new XElement(ns + "R11", citaj["Iznos_s_porezom"]),
                                                        new XElement(ns + "R12", citaj["Ukupni_pretporez"]),
                                                        new XElement(ns + "R13", citaj["por5"]),
                                                        new XElement(ns + "R14", "0.00"),
                                                        new XElement(ns + "R15", citaj["por13"]),
                                                        new XElement(ns + "R16", "0.00"),
                                                        new XElement(ns + "R17", citaj["por25"]),
                                                        new XElement(ns + "R18", "0.00")));
                }
                doc.Save(path);
                bazaspoj.Close();
            }
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

        void PopuniUkupno()
        {
            string connStr = "datasource=localhost;port=3306;username=root;password=pass123";
            string query = "SELECT * FROM poreznaura.ura WHERE Datum_racuna BETWEEN '"
                + datumOdBox + "' AND '" + datumDoBox + "';";
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

            XNamespace ns = "http://e-porezna.porezna-uprava.hr/sheme/zahtjevi/ObrazacURA/v1-0";
            XNamespace ns2 = "http://e-porezna.porezna-uprava.hr/sheme/Metapodaci/v2-0";

            XDocument doc = XDocument.Load(path);


            doc.Element(ns + "ObrazacURA").Element(ns + "Tijelo").Element(ns + "Ukupno").Add(
                                                new XElement(ns + "U8", Math.Round(osn5, 2)),
                                                new XElement(ns + "U9", Math.Round(osn13, 2)),
                                                new XElement(ns + "U10", Math.Round(osn25, 2)),
                                                new XElement(ns + "U11", Math.Round(ukIznos, 2)),
                                                new XElement(ns + "U12", Math.Round(pretPorUk, 2)),
                                                new XElement(ns + "U13", Math.Round(por5, 2)),
                                                new XElement(ns + "U14", "0.00"),
                                                new XElement(ns + "U15", Math.Round(por13, 2)),
                                                new XElement(ns + "U16", "0.00"),
                                                new XElement(ns + "U17", Math.Round(por25, 2)),
                                                new XElement(ns + "U18", "0.00"));


            doc.Save(path);
        }

        void OtvoriCsv()
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.csv)|*.csv";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                put = choofdlog.FileName.ToString();
            }
            //popunjavanje datagridview1 prema parametrima prvog stupca u invoice.txt
            if (put == null)
            {
                MessageBox.Show("Nije odabran file");
                return;
            }
            /*Poruka poruka = new Poruka();
            poruka.Show();*/
            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            MySqlConnection con = new MySqlConnection(constring);
            string query = "INSERT INTO poreznaura.ura (Rbr, Datum_racuna, Broj_racuna, Za_uplatu, " +
                "Naziv_dobavljaca, Sjediste_dobavljaca, OIB, Iznos_s_porezom, Porezna_osn0, " +
                "Porezna_osn5, Porezna_osn13, Porezna_osn25, Ukupni_pretporez, por5, por13, por25, " +
                "br_primke, storno, odobr) " +
                "VALUES (@Rbr, @Datum_racuna, @Broj_racuna, @Za_uplatu, @Naziv_dobavljaca, " +
                "@Sjediste_dobavljaca, @OIB, @Iznos_s_porezom, @Porezna_osn0, @Porezna_osn5, " +
                "@Porezna_osn13, @Porezna_osn25, @Ukupni_pretporez, @por5, @por13, @por25, " +
                "@br_primke, @storno, @odobr);";
            con.Open();
            int rowsAffected = 0;
            try
            {
                string[] lines = File.ReadAllLines(put);
                foreach (string line in lines)
                {
                    string[] text = line.Split(';', '\n');
                    if (text[0] == "Rbr" || text[0] == "") continue;

                    DateTime dt1 = DateTime.Parse(text[2]);
                    text[6] = dt1.ToString("yyyy-MM-dd");

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Rbr", text[1]);
                    cmd.Parameters.AddWithValue("@Datum_racuna", text[6].ToString());
                    cmd.Parameters.AddWithValue("@Broj_racuna", text[3].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Za_uplatu", Convert.ToDouble(text[11].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Naziv_dobavljaca", text[12].ToString().Trim());
                    cmd.Parameters.AddWithValue("@br_primke", Convert.ToDouble(text[13].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Sjediste_dobavljaca", text[16].ToString().Trim());
                    cmd.Parameters.AddWithValue("@OIB", text[17].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Iznos_s_porezom", Convert.ToDouble(text[18].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Porezna_osn0", Convert.ToDouble(text[19].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Porezna_osn5", Convert.ToDouble(text[20].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Porezna_osn13", Convert.ToDouble(text[24].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Porezna_osn25", Convert.ToDouble(text[28].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@Ukupni_pretporez", Convert.ToDouble(text[30].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@por5", Convert.ToDouble(text[21].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@por13", Convert.ToDouble(text[25].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@por25", Convert.ToDouble(text[29].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@storno", text[5].ToString().Trim());
                    cmd.Parameters.AddWithValue("@odobr", text[37].ToString().Trim());

                    rowsAffected=cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();

            IspisIzBaze();
            
            MessageBox.Show("Unešeno");
        }

        //kreiraj xml
        private void button1_Click(object sender, EventArgs e)
        {
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            BrisiDatagrid();
            PopuniObrazac();
            PopuniUkupno();
        }
        //otvori csv
        private void button2_Click(object sender, EventArgs e)
        {
            BrisiDatagrid();
            BrisiBazu();
            datumOdBox = datumOd.Value.ToString("yyyy-MM-dd");
            datumDoBox = datumDo.Value.ToString("yyyy-MM-dd");
            OtvoriCsv();
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
            IspisIzBaze();
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
            //OdobrenjaPojedinacno();            
            zbroji();
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
