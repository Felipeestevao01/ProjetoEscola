using Dao;
using MySql.Data.MySqlClient;
using ProjetoEscola.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    internal class Matricula
    {
        public long Id { get; set; }
        public bool Ativa { get; set; }
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }

        public Matricula()
        {
        }

        public Matricula(int id, bool ativa, Aluno aluno)
        {
            Id = id;
            Ativa = ativa;
            Aluno = aluno;
        }

        public Matricula(int id, bool ativa, Aluno aluno, Curso curso)
        {
            Id = id;
            Ativa = ativa;
            Aluno = aluno;
            Curso = curso;
        }

        public static Matricula GetById(int id)
        {
            string sqlQuery =
                "SELECT matricula.id AS \"matricula_id\", matricula.ativa, pessoa.nome AS \"nome_aluno\", pessoa.sobrenome, pessoa.telefone, " +
                "pessoa.cpf, pessoa.endereco, pessoa.email, pessoa.data_aniversario, aluno.id AS \"aluno_id\", aluno.numero_falta, " +
                "curso.id AS \"curso_id\", curso.nome AS \"curso_nome\", curso.carga_horaria, curso.ativo " +
                "FROM matricula " +
                "INNER JOIN pessoa " +
                $"INNER JOIN aluno ON pessoa.id = aluno.id_pessoa AND aluno.id = matricula.id_aluno AND matricula.id = {id} " +
                "INNER JOIN curso ON curso.id = matricula.id_curso;";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Matricula novaMatricula = new(
                (int)linha["matricula_id"],
                (bool)linha["ativa"],
                (Aluno)new Aluno(
                   (DateTime)linha["data_aniversario"],
                   (string)linha["nome_aluno"],
                   (string)linha["sobrenome"],
                   (string)linha["telefone"],
                   (string)linha["cpf"],
                   (string)linha["endereco"],
                   (string)linha["email"],
                   (int)linha["aluno_id"],
                   (int)linha["numero_falta"]
                ),
                (Curso)new Curso(
                    (int)linha["curso_id"],
                    (string)linha["curso_nome"],
                    (double)linha["carga_horaria"],
                    (bool)linha["ativo"]
                )
            );
            return novaMatricula;
        }

        public void Salvar()
        {
            string sqlInserirMatricula =
                "INSERT INTO matricula (ativa, id_aluno, id_curso) " +
                $"VALUES({Ativa}, {Aluno.Id}, {Curso.Id}); ";

            long idMatricula = BancoDeDados.Insert(sqlInserirMatricula);
            this.Id = idMatricula;
        }

        public void Deletar()
        {
            string sqlDeletarMatricula = $"DELETE FROM matricula WHERE id = {Id};";
            BancoDeDados.Delete(sqlDeletarMatricula);
        }
    }
}
