using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class IspPodIzBazeUra
    {
        public void ispis(string datumOd, string datumDo, DataGridView dgw1 )
        {
            string connStr = "datasource=localhost;port=3306;username=root;password=pass123";
            string query = "SELECT * FROM poreznaura.ura WHERE Datum_racuna BETWEEN '"
                + datumOd + "' AND '" + datumDo + "' ;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dgw1.DataSource = ds.Tables[0];
                    conn.Close();
                }
            }
            foreach (DataGridViewRow Myrow in dgw1.Rows)
            {
                if (Convert.ToInt32(Myrow.Cells[17].Value) != 0)
                {
                    Myrow.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }
    }
}
