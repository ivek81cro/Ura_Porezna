using System;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class UraStavka
    {
        public double UkIznos { get; set; }
        public double Osn5 { get; set; }
        public double Osn13 { get; set; }
        public double Osn25 { get; set; }
        public double OsnovicaUkupno { get; set; }
        public double Neoporezivo { get; set; }
        public double Por5 { get; set; }
        public double Por13 { get; set; }
        public double Por25 { get; set; }
        public double PretPorUk { get; set; }

        public UraStavka Zbroji(DataGridView dataGridView1)
        {
            double ukIznos = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ukIznos += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString()), 2);
            }

            double neoporezivo = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                neoporezivo += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString()), 2);
            }
            //ukIznos = ukIznos - neoporezivo;


            double osn5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn5 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString()), 2);
            }


            double osn13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn13 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString()), 2);
            }


            double osn25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                osn25 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[11].Value.ToString()), 2);
            }

            double por5 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por5 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[13].Value.ToString()), 2);
            }

            double por13 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por13 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[14].Value.ToString()), 2);
            }

            double por25 = 0.00;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                por25 += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[15].Value.ToString()), 2);
            }

            return new UraStavka()
            {
                OsnovicaUkupno = osn5 + osn13 + osn25,
                Por5 = por5,
                Por13 = por13,
                Por25 = por25,
                Osn5 = osn5,
                Osn13 = osn13,
                Osn25 = osn25,
                Neoporezivo = neoporezivo,
                UkIznos = ukIznos,
                PretPorUk = por5 + por13 + por25
            };
        }
    }
}
