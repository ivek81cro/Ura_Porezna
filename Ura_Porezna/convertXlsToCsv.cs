using ExcelDataReader;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class ConvertXlsToCsv
    {
        public static void Convert(ref string put, int zadnjiRed)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Xls Files *.xls|*.xls|Xlsx Files *.xlsx|*.xlsx|Csv files *.csv|*.csv";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                put = choofdlog.FileName.ToString();
            }
            if (put == null)
            {
                MessageBox.Show("Nije odabran file");
                return;
            }

            if (put.Contains(".csv")) return;

            FileStream stream = File.Open(put, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader;
            try
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            catch
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            DataSet result = excelReader.AsDataSet();
            excelReader.Close();

            result.Tables[0].TableName.ToString();

            string csvData = "";
            int row_no = 0;
            int ind = 0;
            while (row_no < result.Tables[ind].Rows.Count) // ind is the index of table
                                                           // (sheet name) which you want to convert to csv
            {
                for (int i = 0; i < result.Tables[ind].Columns.Count; i++)
                {
                    csvData += result.Tables[ind].Rows[row_no][i].ToString() + ";";
                }
                row_no++;
                csvData += "\n";
            }
            if (put.Contains(".xlsx"))
            {
                put = put.Replace("xlsx", "csv");
            }
            else
            {
                put = put.Replace("xls", "csv");
            }
            string output = put; // define your own filepath & filename
            StreamWriter csv = new StreamWriter(@output, false);
            csv.Write(csvData);
            csv.Close();
        }
    }        
}
