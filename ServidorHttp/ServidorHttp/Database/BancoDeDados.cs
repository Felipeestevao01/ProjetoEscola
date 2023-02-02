using MySql.Data.MySqlClient;

namespace Dao
{
    internal class BancoDeDados
    {
        private static MySqlConnection Connection;
        private static string myConnectionString = "server=127.0.0.1;uid=root;pwd=123456;database=projetoescola";

        private BancoDeDados()
        {
        }

        public static MySqlConnection GetConnection()
        {
            if (Connection == null)
            {
                try
                {
                    Connection = new MySqlConnection();
                    Connection.ConnectionString = myConnectionString;
                    Connection.Open();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Connection;
        }

        public static MySqlDataReader PreparaQuery(string sqlQuery)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, BancoDeDados.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            return reader;
        }
    }
}
