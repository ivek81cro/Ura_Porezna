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
            for (int i =0; i<20; i++)
                comboGodine.Items.Add(DateTime.Now.Year-i);
            comboGodine.Text = "2019";
        }

        private void btn_ucitaj_Click(object sender, EventArgs e)
        {
            int godina = Int32.Parse(comboGodine.Text);
            UcitajXls citaj = new UcitajXls();
            citaj.Otvori(godina, dataGridView1);
        }        
    }
}
