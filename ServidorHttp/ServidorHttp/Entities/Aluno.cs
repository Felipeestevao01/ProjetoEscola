using Dao;
using System.Data;
using MySql.Data.MySqlClient;

namespace Entities
{
    internal class Aluno : Pessoa
    {
        public int Id { get; set; }
        public int NumeroFaltas { get; set; }
        public List<Matricula> Matriculas { get; set; }
        public Aluno()
        {
        }

        public Aluno(DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email, int id, int numeroFaltas)
            : base(dataAniversario, nome, sobrenome, telefone, cpf, endereco, email)
        {
            Id = id;
            NumeroFaltas = numeroFaltas;
        }

        public Aluno(DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email, int id, int numeroFaltas, List<Matricula> matriculas)
         : base(dataAniversario, nome, sobrenome, telefone, cpf, endereco, email)
        {
            Id = id;
            NumeroFaltas = numeroFaltas;
            Matriculas = matriculas;
        }


        public static Aluno GetById(int id)
        {
            string sqlQuery =
                "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, " +
                "pessoa.cpf, pessoa.endereco, pessoa.email, pessoa.data_aniversario, " +
                "aluno.id, aluno.numero_falta FROM pessoa INNER JOIN aluno " +
                $"WHERE pessoa.id = aluno.id AND aluno.id = {id};";

            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);
            DataTable dataTableAlunos = new();
            dataTableAlunos.Load(reader);
            DataRow linha = dataTableAlunos.Rows[0];

            Aluno novoAluno = new(
                (DateTime)linha["data_aniversario"],
                (string)linha["nome"],
                (string)linha["sobrenome"],
                (string)linha["telefone"],
                (string)linha["cpf"],
                (string)linha["endereco"],
                (string)linha["email"],
                (int)linha["id"],
                (int)linha["numero_falta"]
            );

            return novoAluno;
        }

        public List<Matricula> GetMatriculas()
        {
            string sqlQueryMatriculas =
                "SELECT matricula.id, ativa " +
                "FROM matricula " +
                $"INNER JOIN aluno ON matricula.id_aluno = aluno.id AND aluno.id = {Id};";

            MySqlDataReader readerMatriculas = BancoDeDados.PreparaQuery(sqlQueryMatriculas);
            DataTable dataTableMatriculas = new();
            dataTableMatriculas.Load(readerMatriculas);
            List<Matricula> listaMastriculas = new();

            foreach (DataRow matriculaLinha in dataTableMatriculas.Rows)
            {
                Matricula matriculaAtual = new(
                    (int)matriculaLinha["id"],
                    (bool)matriculaLinha["ativa"],
                    this
                );

                string sqlQueryCursos =
                    "SELECT curso.id, curso.nome, curso.carga_horaria, curso.ativo " +
                    "FROM curso " +
                    $"INNER JOIN matricula ON matricula.id_curso = curso.id AND matricula.id = {Id};";

                MySqlDataReader readerCursos = BancoDeDados.PreparaQuery(sqlQueryCursos);
                DataTable dataTableCursos = new();
                dataTableCursos.Load(readerCursos);
                DataRow linhaCurso = dataTableCursos.Rows[0];
                matriculaAtual.Curso = new Curso(
                    (int)linhaCurso["id"],
                    (string)linhaCurso["nome"],
                    (double)linhaCurso["carga_horaria"],
                    (bool)linhaCurso["ativo"]
                );

                listaMastriculas.Add(matriculaAtual);
            };

            return listaMastriculas;

        }

        public static List<Aluno> GetAll()
        {
            List<Aluno> lista = new();

            string sqlQuery =
               "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, " +
               "pessoa.cpf, pessoa.endereco, pessoa.email, pessoa.data_aniversario, " +
               "aluno.id, aluno.numero_falta FROM pessoa INNER JOIN aluno " +
               $"WHERE pessoa.id = aluno.id;";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach(DataRow linha in dataTable.Rows)
            {
                Aluno novoAluno = new(
                   (DateTime)linha["data_aniversario"],
                   (string)linha["nome"],
                   (string)linha["sobrenome"],
                   (string)linha["telefone"],
                   (string)linha["cpf"],
                   (string)linha["endereco"],
                   (string)linha["email"],
                   (int)linha["id"],
                   (int)linha["numero_falta"]
               );
                lista.Add(novoAluno);
            }
            return lista;
        }
    }
}
