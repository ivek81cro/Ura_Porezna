﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ura_Porezna
{
    class Ispis
    {
        public static void Ispisi(string datumOdBox, string datumDoBox, CustomDataGridView dataGridView1)
        {
            string connStr = "datasource=localhost;port=3306;username=root;password=pass123";
            string query = string.Format("SELECT * FROM poreznaura.ira WHERE datum_rn BETWEEN "
                + "'{0}' AND '{1}' ;", datumOdBox, datumDoBox);
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
        }
    }
}
