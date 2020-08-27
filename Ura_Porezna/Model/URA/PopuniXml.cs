using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Ura_Porezna
{    
    class PopuniXml
    {
        public bool PopuniObrazac(string datumOdBox, string datumDoBox)
        {
            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            string upit;
            MySqlConnection bazaspoj;
            MySqlCommand bazazapovjed;
            MySqlDataReader citaj;

            string UUID = Guid.NewGuid().ToString();
            SaveFileDialog savFile = new SaveFileDialog
            {
                Filter = "XML|*.xml"
            };
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
                XmlWriter.Create(sw);
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
                    double iznosSporezom = Math.Round(Convert.ToDouble(citaj["Porezna_osn5"]) + Convert.ToDouble(citaj["Porezna_osn13"]) +
                        Convert.ToDouble(citaj["Porezna_osn25"]) + Convert.ToDouble(citaj["Ukupni_pretporez"]), 2);

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
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PopuniUkupno(UraStavka s)
        {
            XNamespace ns = "http://e-porezna.porezna-uprava.hr/sheme/zahtjevi/ObrazacURA/v1-0";

            XDocument doc = XDocument.Load(put);

            doc.Element(ns + "ObrazacURA").Element(ns + "Tijelo").Element(ns + "Ukupno").Add(
                                                new XElement(ns + "U8", Math.Round(s.Osn5, 2)),
                                                new XElement(ns + "U9", Math.Round(s.Osn13, 2)),
                                                new XElement(ns + "U10", Math.Round(s.Osn25, 2)),
                                                new XElement(ns + "U11", Math.Round(s.Osn5 + s.Osn13 + s.Osn25 + s.PretPorUk, 2)),
                                                new XElement(ns + "U12", Math.Round(s.PretPorUk, 2)),
                                                new XElement(ns + "U13", Math.Round(s.Por5, 2)),
                                                new XElement(ns + "U14", "0.00"),
                                                new XElement(ns + "U15", Math.Round(s.Por13, 2)),
                                                new XElement(ns + "U16", "0.00"),
                                                new XElement(ns + "U17", Math.Round(s.Por25, 2)),
                                                new XElement(ns + "U18", "0.00"));

            doc.Save(put);
        }

        private string put = null;
    }
}