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
        }

        private void btn_ucitaj_Click(object sender, EventArgs e)
        {
            UcitajXls citaj = new UcitajXls();
            citaj.Otvori();
        }
    }
}
