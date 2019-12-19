using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ura_Porezna
{
    public partial class Hzzo : Form
    {
        public Hzzo()
        {
            InitializeComponent();
            showData();
            for (int i =0; i<20; i++)
                comboGodine.Items.Add(DateTime.Now.Year-i);
            comboGodine.Text = "2019";
        }

        private void btn_ucitaj_Click(object sender, EventArgs e)
        {
            int godina = Int32.Parse(comboGodine.Text);
            UcitajXls citaj = new UcitajXls();
            citaj.Otvori(godina);
            showData();
        }

        private void showData()
        {
            string connStr = "datasource=localhost;port=3306;database=poreznaura;username=root;" +
                "password=pass123;Allow User Variables=True";
            string query = string.Format("SELECT * FROM hzzo");
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
