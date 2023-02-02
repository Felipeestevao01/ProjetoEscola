
using Dao;
using MySql.Data.MySqlClient;
using System.Data;

namespace Entities
{
    internal class Professor : Pessoa
    {
        public int Id { get; set; }
        public double Salario { get; set; }

        public Professor()
        {
        }

        public Professor(DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email, int id, double salario)
            : base(dataAniversario, nome, sobrenome, telefone, cpf, endereco, email)
        {
            Id = id;
            Salario = salario;
        }

        public static Professor GetById(int id)
        {
            string sqlQuery =
                "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, pessoa.cpf, pessoa.endereco, " +
                "pessoa.email, pessoa.data_aniversario, professor.id, professor.salario " +
                "FROM pessoa INNER JOIN professor " +
                $"WHERE pessoa.id = professor.id_pessoa AND professor.id ={id}; ";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Professor novoProfessor = new(
                (DateTime)linha["data_aniversario"],
                (string)linha["nome"],
                (string)linha["sobrenome"],
                (string)linha["telefone"],
                (string)linha["cpf"],
                (string)linha["endereco"],
                (string)linha["email"],
                (int)linha["id"],
                (double)linha["salario"]
            );
            return novoProfessor;
        }

        public static List<Professor> GetAll()
        {
            List<Professor> lista = new();

            string sqlQuery =
               "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, pessoa.cpf, pessoa.endereco, " +
               "pessoa.email, pessoa.data_aniversario, professor.id, professor.salario " +
               "FROM pessoa INNER JOIN professor WHERE pessoa.id = professor.id_pessoa";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach (DataRow linha in dataTable.Rows)
            {
                Professor novoProfessor = new(
                   (DateTime)linha["data_aniversario"],
                   (string)linha["nome"],
                   (string)linha["sobrenome"],
                   (string)linha["telefone"],
                   (string)linha["cpf"],
                   (string)linha["endereco"],
                   (string)linha["email"],
                   (int)linha["id"],
                   (double)linha["salario"]
               );
                lista.Add(novoProfessor);
            }
            return lista;
        }
    }
}
