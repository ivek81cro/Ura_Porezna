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
    public partial class Form0 : Form
    {
        public Form0()
        {
            InitializeComponent();
        }
        FormURA ura;
        Form2 ira;

        private void URAPoreznaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ura == null)
            {
                ura = new FormURA();
                ura.MdiParent = this;
                ura.FormClosed += Ura_FormClosed;
                ura.Show();
                ura.WindowState = FormWindowState.Maximized;
            }
            else
            {
                ura.Close();
                ura = new FormURA();
                ura.MdiParent = this;
                ura.FormClosed += Ura_FormClosed;
                ura.Show();
                ura.WindowState = FormWindowState.Maximized;
            }
        }
        void Ura_FormClosed(object sender, FormClosedEventArgs e)
        {
            ura = null;
            //throw new NotImplementedException();
        }

        private void iRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ira == null)
            {
                ira = new Form2();
                ira.MdiParent = this;
                ira.FormClosed += Ira_FormClosed;
                ira.Show();
                ira.WindowState = FormWindowState.Maximized;
            }
            else
            {
                ira.Close();
                ira = new Form2();
                ira.MdiParent = this;
                ira.FormClosed += Ira_FormClosed;
                ira.Show();
                ira.WindowState = FormWindowState.Maximized;
            }
        }
        void Ira_FormClosed(object sender, FormClosedEventArgs e)
        {
            ira = null;
            //throw new NotImplementedException();
        }
    }
}
