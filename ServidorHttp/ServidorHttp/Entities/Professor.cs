using Dao;
using MySql.Data.MySqlClient;
using System.Data;

namespace Entities
{
    internal class Professor : Pessoa
    {
        public long Id { get; set; }
        public double Salario { get; set; }

        public Professor()
        {
        }

        public Professor(DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email, double salario)
            : base(dataAniversario, nome, sobrenome, telefone, cpf, endereco, email)
        {
            this.Salario = salario;
        }

        public Professor(long idPessoa, DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email,long id, double salario)
    : base(idPessoa, dataAniversario, nome, sobrenome, telefone, cpf, endereco, email)
        {
            this.Id = id;
            this.Salario = salario;
        }

        public Professor(DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email, long id, double salario)
            : base(dataAniversario, nome, sobrenome, telefone, cpf, endereco, email)
        {
            this.Id = id;
            this.Salario = salario;
        }

        public static Professor GetById(long id)
        {
            string sqlQuery =
              "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, pessoa.cpf, pessoa.endereco, pessoa.email, pessoa.data_aniversario, " +
              "professor.id AS professorId, pessoa.id AS pessoaId, professor.salario " +
              "FROM pessoa " +
              "INNER JOIN professor " +
              $"WHERE pessoa.id = professor.id_pessoa AND professor.id = {id}; ";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Professor novoProfessor = new(
                (int)linha["pessoaId"],
                (DateTime)linha["data_aniversario"],
                (string)linha["nome"],
                (string)linha["sobrenome"],
                (string)linha["telefone"],
                (string)linha["cpf"],
                (string)linha["endereco"],
                (string)linha["email"],
                (int)linha["professorId"],
                (double)linha["salario"]
            );
            return novoProfessor;
        }

        public static List<Professor> GetAll()
        {
            List<Professor> lista = new();

            string sqlQuery =
               "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, pessoa.cpf, pessoa.endereco, pessoa.email, pessoa.data_aniversario, " +
               "professor.id AS professorId, pessoa.id AS pessoaId, professor.salario " +
               "FROM pessoa " +
               "INNER JOIN professor " +
               $"WHERE pessoa.id = professor.id_pessoa; ";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach (DataRow linha in dataTable.Rows)
            {
                Professor novoProfessor = new(
                 (int)linha["pessoaId"],
                 (DateTime)linha["data_aniversario"],
                 (string)linha["nome"],
                 (string)linha["sobrenome"],
                 (string)linha["telefone"],
                 (string)linha["cpf"],
                 (string)linha["endereco"],
                 (string)linha["email"],
                 (int)linha["professorId"],
                 (double)linha["salario"]
             );
                lista.Add(novoProfessor);
            }
            return lista;
        }

        public void Salvar()
        {
            Pessoa pessoa = new Pessoa(this.DataAniversario, this.Nome, this.Sobrenome, this.Telefone, this.Cpf, this.Endereco, this.Email);
            pessoa.Salvar();

            string sqlInserirProfessor = $"INSERT INTO aluno (numero_falta, id_pessoa VALUES (\"{this.Salario}\", \"{pessoa.IdPessoa}\");) ";

            long idProfessor = BancoDeDados.Insert(sqlInserirProfessor);
            this.Id = idProfessor;
        }
        public void Deletar()
        {
            string sqlDeletarProfessor = $"DELETE FROM professor WHERE id = {this.Id};";
            BancoDeDados.Delete(sqlDeletarProfessor);
        }

        public void Atualizar()
        {
            Professor professorBanco = Professor.GetById(Id);
            Pessoa pessoa = new Pessoa(professorBanco.IdPessoa, this.DataAniversario, this.Nome, this.Sobrenome, this.Telefone, this.Cpf, this.Endereco, this.Email);
            pessoa.Atualizar();

            string sqlAtualizarProfessor = $"UPDATE professor SET salario = \"{this.Salario}\" WHERE id = \"{this.Id}\";";
            BancoDeDados.Update(sqlAtualizarProfessor);
        }

    }
}
