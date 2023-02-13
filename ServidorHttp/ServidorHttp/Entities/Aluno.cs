using Dao;
using System.Data;
using MySql.Data.MySqlClient;

namespace Entities
{
    internal class Aluno : Pessoa
    {
        public long Id { get; set; }
        public int NumeroFaltas { get; set; }
        public List<Matricula> Matriculas { get; set; }

        public Aluno()
        {
        }

        public Aluno(DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email, long id, int numeroFaltas)
            : base(dataAniversario, nome, sobrenome, telefone, cpf, endereco, email)
        {
            this.Id = id;
            this.NumeroFaltas = numeroFaltas;
        }

        public Aluno(long idPessoa, DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email, long id, int numeroFaltas)
    : base(idPessoa, dataAniversario, nome, sobrenome, telefone, cpf, endereco, email)
        {
            this.Id = id;
            this.NumeroFaltas = numeroFaltas;
        }

        public Aluno(DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email, long id, int numeroFaltas, List<Matricula> matriculas)
         : base(dataAniversario, nome, sobrenome, telefone, cpf, endereco, email)
        {
            this.Id = id;
            this.NumeroFaltas = numeroFaltas;
            this.Matriculas = matriculas;
        }

        public static Aluno GetById(long id)
        {
            string sqlQuery =
                "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, " +
                "pessoa.cpf, pessoa.endereco, pessoa.email, pessoa.data_aniversario, " +
                "aluno.id as alunoId, pessoa.id as pessoaId, aluno.numero_falta FROM pessoa INNER JOIN aluno " +
                $"WHERE pessoa.id = aluno.id_pessoa AND aluno.id = {id};";

            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);
            DataTable dataTableAlunos = new();
            dataTableAlunos.Load(reader);
            DataRow linha = dataTableAlunos.Rows[0];

            Aluno novoAluno = new(
                (int)linha["pessoaId"],
                (DateTime)linha["data_aniversario"],
                (string)linha["nome"],
                (string)linha["sobrenome"],
                (string)linha["telefone"],
                (string)linha["cpf"],
                (string)linha["endereco"],
                (string)linha["email"],
                (int)linha["alunoId"],
                (int)linha["numero_falta"]
            );
            return novoAluno;
        }

        public List<Matricula> GetMatriculas()
        {
            string sqlQueryMatriculas =
                "SELECT matricula.id, ativa " +
                "FROM matricula " +
                $"INNER JOIN aluno ON matricula.id_aluno = aluno.id AND aluno.id = {this.Id};";

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
                    $"INNER JOIN matricula ON matricula.id_curso = curso.id AND matricula.id = {this.Id};";

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

        public void Salvar()
        {
            Pessoa pessoa = new Pessoa(this.DataAniversario, this.Nome, this.Sobrenome, this.Telefone, this.Cpf, this.Endereco, this.Email);
            pessoa.Salvar();

            string sqlInserirAluno =
                "INSERT INTO aluno (numero_falta, id_pessoa) " +
                $"VALUES (\"{this.NumeroFaltas}\", \"{pessoa.IdPessoa}\");";

            long idAluno = BancoDeDados.Insert(sqlInserirAluno);
            this.Id = idAluno;
        }

        public void Deletar()
        {
            string sqlDeletarAluno = $"DELETE FROM aluno WHERE id = {this.Id};";
            BancoDeDados.Delete(sqlDeletarAluno);
        }

        public void Atualizar()
        {
            Aluno alunoBanco = Aluno.GetById(Id);
            Pessoa pessoa = new Pessoa(alunoBanco.IdPessoa, this.DataAniversario, this.Nome, this.Sobrenome, this.Telefone, this.Cpf, this.Endereco, this.Email);
            
            pessoa.Atualizar();

            string sqlAtualizarAluno = $"UPDATE aluno SET numero_falta = \"{this.NumeroFaltas}\" WHERE id = \"{this.Id}\";";
            BancoDeDados.Update(sqlAtualizarAluno);
        }
    }
}
