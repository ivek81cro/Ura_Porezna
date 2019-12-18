﻿using ExcelDataReader;
using System.Data;
using System.IO;

namespace Ura_Porezna
{
    class ConvertXlsToCsv
    {
        public static void Convert(ref string put)
        {
            FileStream stream = File.Open(put, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
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
            put = put.Replace("xls", "csv");
            string output = put; // define your own filepath & filename
            StreamWriter csv = new StreamWriter(@output, false);
            csv.Write(csvData);
            csv.Close();
        }
    }
        
}
