using Dao;
using MySql.Data.MySqlClient;
using System.Data;

namespace Entities
{
    internal class Nota
    {
        public int Id { get; set; }
        public double ValorNota { get; set; }
        public Trabalho Trabalho { get; set; }

        public Nota(int id, double valorNota)
        {
            Id = id;
            ValorNota = valorNota;
        }

        public Nota(int id, double valorNota, Trabalho trabalho)
        {
            Id = id;
            ValorNota = valorNota;
            Trabalho = trabalho;
        }

        public static Nota GetById(int id)
        {
            string sqlQuery = $"SELECT id, valor_nota FROM nota WHERE id = {id};";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Nota novaNota = new Nota(
                (int)linha["id"],
                (double)linha["valor_nota"]
            );
            return novaNota;
        }

        public static List<Nota> GetAll()
        {
            List<Nota> lista = new();

            string sqlQuery = "SELECT id, valor_nota FROM nota;";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach (DataRow linha in dataTable.Rows)
            {
                Nota novaNota = new(
                    (int)linha["id"],
                    (double)linha["valor_nota"]
                );
                lista.Add(novaNota);
            }
            return lista;
        }
    }
}
