using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Ura_Porezna
{
    class PopuniXml : FormURA
    {
        public void PopuniObrazac(string datumOdBox, string datumDoBox)
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
                put = savFile.FileName;                
                upit = "SELECT * FROM poreznaura.obveznik;";
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
                                        new XElement(ns + "DatumOd", datumOdBox),
                                        new XElement(ns + "DatumDo", datumDoBox)),
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
                xDoc.Save(put);

                //---------------------------------------------------------------RACUNI--------------------------------------------------------
                constring = "datasource=localhost;port=3306;username=root;password=pass123";
                upit = string.Format("SELECT * FROM poreznaura.ura WHERE Datum_racuna BETWEEN " +
                    "'{0}' AND '{1}';", datumOdBox, datumDoBox);
                bazaspoj = new MySqlConnection(constring);
                bazazapovjed = new MySqlCommand(upit, bazaspoj);
                bazaspoj.Open();
                citaj = bazazapovjed.ExecuteReader();
                XDocument doc = XDocument.Load(put);
                while (citaj.Read())
                {
                    double iznosSporezom = Convert.ToDouble(citaj["Porezna_osn5"]) + Convert.ToDouble(citaj["Porezna_osn13"]) + 
                        Convert.ToDouble(citaj["Porezna_osn25"]);
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
                                                        new XElement(ns + "R11", iznosSporezom),
                                                        new XElement(ns + "R12", citaj["Ukupni_pretporez"]),
                                                        new XElement(ns + "R13", citaj["por5"]),
                                                        new XElement(ns + "R14", "0.00"),
                                                        new XElement(ns + "R15", citaj["por13"]),
                                                        new XElement(ns + "R16", "0.00"),
                                                        new XElement(ns + "R17", citaj["por25"]),
                                                        new XElement(ns + "R18", "0.00")));
                }
                doc.Save(put);
                bazaspoj.Close();
            }
        }
        public void PopuniUkupno(string datumOdBox, string datumDoBox)
        {
            URAIspPodIzBaze ispis = new URAIspPodIzBaze();

            ispis.ispis(datumOdBox, datumDoBox, dataGridView1);

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

            XDocument doc = XDocument.Load(put);


            doc.Element(ns + "ObrazacURA").Element(ns + "Tijelo").Element(ns + "Ukupno").Add(
                                                new XElement(ns + "U8", Math.Round(osn5, 2)),
                                                new XElement(ns + "U9", Math.Round(osn13, 2)),
                                                new XElement(ns + "U10", Math.Round(osn25, 2)),
                                                new XElement(ns + "U11", Math.Round(osnUk, 2)),
                                                new XElement(ns + "U12", Math.Round(pretPorUk, 2)),
                                                new XElement(ns + "U13", Math.Round(por5, 2)),
                                                new XElement(ns + "U14", "0.00"),
                                                new XElement(ns + "U15", Math.Round(por13, 2)),
                                                new XElement(ns + "U16", "0.00"),
                                                new XElement(ns + "U17", Math.Round(por25, 2)),
                                                new XElement(ns + "U18", "0.00"));


            doc.Save(put);
        }
    }
}
