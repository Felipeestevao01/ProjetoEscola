using Dao;
using Entities;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProjetoEscola.Entities
{
    internal class Questao
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public string Escolha { get; set; }

        public Questao()
        {
        }

        public Questao(long id, string descricao, string escolha)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.Escolha = escolha;
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
        public void Salvar()
        {
            string sqlInserirQuestao = $"INSERT INTO questoes (descricao, escolha) VALUES(\"{this.Descricao}\", \"{this.Escolha}\");";

            long idQuestao = BancoDeDados.Insert(sqlInserirQuestao);
            this.Id = idQuestao;
        }
        public void Deletar()
        {
            string sqlDeletarQuestao = $"DELETE FROM questoes WHERE id = {this.Id};";
            BancoDeDados.Delete(sqlDeletarQuestao);
        }

        public void Atualizar()
        {
            string sqlAtualizarQuestao = $"UPDATE questoes SET descricao = \"{this.Descricao}\", escolha = \"{this.Escolha}\" WHERE id = \"{this.Id}\";";
            BancoDeDados.Update(sqlAtualizarQuestao);
        }
    }
}
