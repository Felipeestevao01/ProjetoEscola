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

        public static long Insert(string sqlInsert)
        {
            MySqlCommand command = new MySqlCommand(sqlInsert, BancoDeDados.GetConnection());
            command.ExecuteNonQuery();
            return command.LastInsertedId;
        }

        public static long Delete(string sqlDelete)
        {
            MySqlCommand command = new MySqlCommand(sqlDelete, BancoDeDados.GetConnection());
            return command.ExecuteNonQuery();
        }

        public static long Update(string sqlUpdate)
        {
            MySqlCommand command = new MySqlCommand(sqlUpdate, BancoDeDados.GetConnection());
            return command.ExecuteNonQuery();
        }
    }
}
