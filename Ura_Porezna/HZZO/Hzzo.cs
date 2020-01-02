using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Ura_Porezna
{
    public partial class Hzzo : Form
    {
        public Hzzo()
        {
            InitializeComponent();
            for (int i =0; i<20; i++)
                comboGodine.Items.Add(DateTime.Now.Year-i);
            comboGodine.Text = "2019";
            datumPick.Value = DateTime.Now;
        }

        private void btn_ucitaj_Click(object sender, EventArgs e)
        {
            int godina = Int32.Parse(comboGodine.Text);
            UcitajXls citaj = new UcitajXls();
            citaj.Otvori(godina, dataGridView1);
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            string query = string.Format("SELECT * FROM poreznaura.hzzo");
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            string query = string.Format("SELECT * FROM poreznaura.hzzo WHERE datum='{0}';", datumPick.Value.ToString("yyyy-MM-dd"));
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }
    }
}
