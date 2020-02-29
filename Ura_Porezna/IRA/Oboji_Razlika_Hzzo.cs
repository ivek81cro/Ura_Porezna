using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class Oboji_Razlika_Hzzo
    {
        public static void ObojiRedove(ref CustomDataGridView dataGridView1, ref Label label20)
        {
            double razlika = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.Cells[3].FormattedValue.ToString().Contains("HZZO"))
                {
                    if (Convert.ToInt32(row.Cells[13].Value) != 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                    }
                    if (Convert.ToInt32(row.Cells[15].Value) - Convert.ToInt32(row.Cells[14].Value) < 5 && Convert.ToInt32(row.Cells[14].Value)!=0)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(204, 255, 153);
                    }
                    else
                    {
                        if (Convert.ToInt32(row.Cells[13].Value) == 0)
                        {
                            razlika += Convert.ToDouble(row.Cells[14].Value) - Convert.ToDouble(row.Cells[15].Value);
                        }
                    }
                }
            }
            label20.Text = "Razlika-HZZO: " + Math.Round(razlika, 2).ToString();
        }
    }
}
