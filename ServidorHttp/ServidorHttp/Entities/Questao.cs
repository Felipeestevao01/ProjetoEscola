using Dao;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProjetoEscola.Entities
{
    internal class Questao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Escolha { get; set; }

        public Questao()
        {
        }

        public Questao(int id, string descricao, string escolha)
        {
            Id = id;
            Descricao = descricao;
            Escolha = escolha;
        }

        public static Questao GetById(int id)
        {
            string sqlQuery = $"SELECT id, descricao, escolha FROM questoes WHERE id = {id};";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Questao novaQuestao = new(
                (int)linha["id"],
                (string)linha["descricao"],
                (string)linha["escolha"]
            );
            return novaQuestao;
        }

        public static List<Questao> GetAll()
        {
            List<Questao> lista = new();

            string sqlQuery = "SELECT id, descricao, escolha FROM questoes;";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach (DataRow linha in dataTable.Rows)
            {
                Questao novaQuestao = new(
                   (int)linha["id"],
                   (string)linha["descricao"],
                   (string)linha["escolha"]
               );
                lista.Add(novaQuestao);
            }
            return lista;
        }
    }
}
