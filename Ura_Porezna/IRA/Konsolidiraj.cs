using MySql.Data.MySqlClient;

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
            MySqlCommand call = new MySqlCommand(query, con);
            call.ExecuteNonQuery();
            con.Close();
        }
    }
}
