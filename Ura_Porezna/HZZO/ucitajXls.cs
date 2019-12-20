﻿using ExcelDataReader;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class UcitajXls
    {
        string put;
        public void Otvori(int godina)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.xls)|*.xls";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                put = choofdlog.FileName.ToString();
            }
            if (put == null)
            {
                MessageBox.Show("Nije odabran file");
                return;
            }

            ConvertXlsToCsv.Convert(ref put);

            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            MySqlConnection con = new MySqlConnection(constring);
            string query = String.Format("INSERT INTO poreznaura.hzzo (datum, dokument, brojRn, " +
                "datumRn, izvor, opis, iznos, placeniIznos) " +
                "VALUES (@datum, @dokument, @brojRn, @datumRn, @izvor, " +
                "@opis, @iznos, @placeniIznos);");
            con.Open();
            int rowsAffected = 0;
            try
            {
                string[] lines = File.ReadAllLines(put);
                for ( int i=9; i<lines.Length-1; ++i)
                {
                    string[] text = lines[i].Split(';', '\n');
                    string[] temp = text[2].Split('-');
                    if (Int32.Parse(temp[2].Split('/')[1]) != godina)
                        continue;
                    int brRn= Int32.Parse(temp[0]);

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@datum", text[0].ToString().Substring(0,10));
                    cmd.Parameters.AddWithValue("@dokument", text[1].ToString());
                    cmd.Parameters.AddWithValue("@brojRn", brRn);
                    cmd.Parameters.AddWithValue("@datumRn", text[3].ToString().Substring(0, 10));
                    cmd.Parameters.AddWithValue("@izvor", text[4].ToString().Trim());
                    cmd.Parameters.AddWithValue("@opis", text[5].ToString().Trim());
                    cmd.Parameters.AddWithValue("@iznos", Convert.ToDouble(text[6].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@placeniIznos", Convert.ToDouble(text[7].ToString().Trim()));
                    cmd.Parameters.AddWithValue("@godina", godina);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                query = string.Format("CALL poreznaura.placeno();");
                MySqlCommand call = new MySqlCommand(query, con);
                call.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
    }
}