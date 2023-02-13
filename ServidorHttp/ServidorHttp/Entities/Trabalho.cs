using Dao;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace Entities
{
    internal class Trabalho
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataTrabalho { get; set; }
        public Professor Professor { get; set; }

        public Trabalho()
        {
        }

        public Trabalho(int id, string descricao, DateTime dataTrabalho)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.DataTrabalho = dataTrabalho;
        }

        public Trabalho(int id, string descricao, DateTime dataTrabalho, Professor professor)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.DataTrabalho = dataTrabalho;
            this.Professor = professor;
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
                )
           );
            return novoTrabalho;
        }

        public static List<Trabalho> GetAll()
        {
            List<Trabalho> lista = new();

            string sqlQuery =
                "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, pessoa.cpf, pessoa.endereco, pessoa.email, pessoa.data_aniversario, " +
                "professor.id, professor.salario, trabalho.id as \"trabalho_id\", descricao, data_trabalho " +
                "FROM pessoa " +
                "INNER JOIN professor ON pessoa.id = professor.id_pessoa " +
                "INNER JOIN trabalho ON professor.id = trabalho.id_professor;";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach (DataRow linha in dataTable.Rows)
            {
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
                    )
                );
                lista.Add(novoTrabalho);
            }
            return lista;
        }

        public void Salvar()
        {
            string sqlInserirTrabalho =
                "INSERT INTO trabalho(descricao, data_trabalho, id_professor) " +
                $"VALUES(\"{this.Descricao}\", \"{this.DataTrabalho.ToString("yyyy-MM-ddTHH:mm:ss")}\", {this.Professor.Id});";

            long idTrabalho = BancoDeDados.Insert(sqlInserirTrabalho);
            this.Id = idTrabalho;
        }

        public void Deletar()
        {
            string sqlDeletarTrabalho = $"DELETE FROM trabalho WHERE id = {this.Id};";
            BancoDeDados.Delete(sqlDeletarTrabalho);
        }

        public void Atualizar()
        {
            string sqlAtualizarTrabalho = $"UPDATE trabalho SET descricao = \"{this.Descricao}\"," +
                $" data_trabalho = \"{this.DataTrabalho.ToString("yyyy-MM-ddTHH:mm:ss")}\" WHERE id = \"{this.Id}\";";
            BancoDeDados.Update(sqlAtualizarTrabalho);
        }
    }
}
