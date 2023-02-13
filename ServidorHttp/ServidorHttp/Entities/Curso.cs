using Dao;
using MySql.Data.MySqlClient;
using System.Data;

namespace Entities
{
    internal class Curso
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public bool Ativo { get; set; }

        public Curso()
        {
        }

        public Curso(string nome, double cargaHoraria, bool ativo)
        {
            this.Nome = nome;
            this.CargaHoraria = cargaHoraria;
            this.Ativo = ativo;
        }

        public Curso(long id, string nome, double cargaHoraria, bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.CargaHoraria = cargaHoraria;
            this.Ativo = ativo;
        }


        public static Curso GetById(int id)
        {
            string sqlQuery = $"SELECT id, nome, carga_horaria, ativo FROM curso WHERE id = {id};";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Curso novoCurso = new(
                (int)linha["id"],
                (string)linha["nome"],
                (double)linha["carga_horaria"],
                (bool)linha["ativo"]
            );
            return novoCurso;
        }

        public static List<Curso> GetAll()
        {
            List<Curso> lista = new();

            string sqlQuery = "SELECT curso.id, curso.nome, curso.carga_horaria, curso.ativo FROM curso;";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach (DataRow linha in dataTable.Rows)
            {
                Curso novoCurso = new(
                    (int)linha["id"],
                    (string)linha["nome"],
                    (double)linha["carga_horaria"],
                    (bool)linha["ativo"]
                );
                lista.Add(novoCurso);
            }
            return lista;
        }

        public void Salvar()
        {
            string sqlInserirCurso =
                    "INSERT INTO curso (nome, carga_horaria, ativo) " +
                    $"VALUES " +
                    $"(\"{this.Nome}\", {this.CargaHoraria}, {this.Ativo});";

            long idCurso = BancoDeDados.Insert(sqlInserirCurso);
            this.Id = idCurso;
        }

        public void Deletar()
        {
            string sqlDeletarCurso = $"DELETE FROM curso WHERE id = {this.Id};";
            BancoDeDados.Delete(sqlDeletarCurso);
        }

        public void Atualizar()
        {
            string sqlAtualizarCurso = $"UPDATE curso SET nome = \"{this.Nome}\", carga_horaria = {this.CargaHoraria}, ativo = {this.Ativo} WHERE id = \"{this.Id}\";";
            BancoDeDados.Update(sqlAtualizarCurso);
        }
    }
}
