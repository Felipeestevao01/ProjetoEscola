using Dao;
using MySql.Data.MySqlClient;
using System.Data;

namespace Entities
{
    internal class Trabalho
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataTrabalho { get; set; }
        public Professor Professor { get; set; }

        public Trabalho(int id, string descricao, DateTime dataTrabalho)
        {
            Id = id;
            Descricao = descricao;
            DataTrabalho = dataTrabalho;
        }

        public Trabalho(int id, string descricao, DateTime dataTrabalho, Professor professor)
        {
            Id = id;
            Descricao = descricao;
            DataTrabalho = dataTrabalho;
            Professor = professor;
        }

        public static Trabalho GetById(int id)
        {

            string sqlQuery = 
                "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, pessoa.cpf, pessoa.endereco, pessoa.email, pessoa.data_aniversario, " +
                "professor.id, professor.salario, trabalho.id as \"trabalho_id\", descricao, data_trabalho " +
                "FROM pessoa " +
                "INNER JOIN professor ON pessoa.id = professor.id_pessoa " +
                $"INNER JOIN trabalho ON professor.id = trabalho.id_professor AND trabalho.id = {id};";


            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Trabalho novoTrabalho = new(
                (int)linha["id"],
                (string)linha["descricao"],
                (DateTime)linha["data_trabalho"],
                (Professor) new Professor(
                    (DateTime)linha["data_aniversario"],
                    (string)linha["nome"],
                    (string)linha["sobrenome"],
                    (string)linha["telefone"],
                    (string)linha["cpf"],
                    (string)linha["endereco"],
                    (string)linha["email"],
                    (int)linha["trabalho_id"],
                    (double)linha["salario"]
            ));

            return novoTrabalho;
        }

        public static List<Trabalho> GetAll()
        {
            List<Trabalho> lista = new();

            string sqlQuery = "SELECT id, descricao, data_trabalho FROM trabalho;";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach (DataRow linha in dataTable.Rows)
            {
                Trabalho novoTrabalho = new(
                    (int)linha["id"],
                    (string)linha["descricao"],
                    (DateTime)linha["data_trabalho"]
                );
                lista.Add(novoTrabalho);
            }
            return lista;
        }
    }
}
