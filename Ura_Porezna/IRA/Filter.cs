using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class Filter
    {
        public void Filtriraj(string textFilter, CustomDataGridView dataGridView1, int column)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                BindingSource bs = new BindingSource
                {
                    DataSource = dataGridView1.DataSource,
                    Filter = dataGridView1.Columns[column].HeaderText.ToString() + " LIKE '%" + textFilter + "%'"
                };
                dataGridView1.DataSource = bs;
            }
        }
    }
}
