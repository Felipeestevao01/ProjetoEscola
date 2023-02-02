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
            string sqlQuery = $"SELECT id, descricao, data_trabalho FROM trabalho WHERE id = {id};";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Trabalho novoTrabalho = new(
                (int)linha["id"],
                (string)linha["descricao"],
                (DateTime)linha["data_trabalho"]
            );
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
