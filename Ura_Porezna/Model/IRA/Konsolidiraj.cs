using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Ura_Porezna
{
    class Konsolidiraj
    {
        public void Pokreni()
        {
            string constring = "datasource=localhost;port=3306;username=root;password=pass123";
            MySqlConnection con = new MySqlConnection(constring);
            string query = string.Format("CALL poreznaura.konsolidiraj_prvi();");
            con.Open();
            try
            {
                MySqlCommand call = new MySqlCommand(query, con);
                call.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                MessageBox.Show("Izvršeno","Konsolidacija", MessageBoxButtons.OK ,MessageBoxIcon.Information);
                con.Close();
            }
        }
    }
}
