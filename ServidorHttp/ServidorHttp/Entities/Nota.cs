using Dao;
using MySql.Data.MySqlClient;
using System.Data;

namespace Entities
{
    internal class Nota
    {
        public int Id { get; set; }
        public double ValorNota { get; set; }
        public Trabalho Trabalho { get; set; }

        public Nota(int id, double valorNota)
        {
            Id = id;
            ValorNota = valorNota;
        }

        public Nota(int id, double valorNota, Trabalho trabalho)
        {
            Id = id;
            ValorNota = valorNota;
            Trabalho = trabalho;
        }

        public static Nota GetById(int id)
        {
            string sqlQuery =
                "SELECT nota.id, valor_nota, pessoa.nome, pessoa.sobrenome, pessoa.telefone, pessoa.cpf, pessoa.endereco, pessoa.email, " +
                "pessoa.data_aniversario, professor.id as \"professor_id\", professor.salario, trabalho.id as \"trabalho_id\", descricao, data_trabalho " +
                "FROM nota " +
                "INNER JOIN pessoa " +
                "INNER JOIN professor ON pessoa.id = professor.id_pessoa " +
                $"INNER JOIN trabalho ON professor.id = trabalho.id_professor AND trabalho.id = {id} " +
                $"WHERE nota.id = {id};";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Nota novaNota = new Nota(
                (int)linha["id"],
                (double)linha["valor_nota"],
                (Trabalho) new Trabalho(
                    (int)linha["trabalho_id"],
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
                        (int)linha["professor_id"],
                        (double)linha["salario"]
                    )
                )
            );
            return novaNota;
        }

        public static List<Nota> GetAll()
        {
            List<Nota> lista = new();

            string sqlQuery =
                "SELECT nota.id, valor_nota, pessoa.nome, pessoa.sobrenome, pessoa.telefone, pessoa.cpf, pessoa.endereco, pessoa.email, " +
                "pessoa.data_aniversario, professor.id as \"professor_id\", professor.salario, trabalho.id as \"trabalho_id\", descricao, data_trabalho " +
                "FROM nota " +
                "INNER JOIN pessoa " +
                "INNER JOIN professor ON pessoa.id = professor.id_pessoa " +
                $"INNER JOIN trabalho ON professor.id = trabalho.id_professor ";

            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach (DataRow linha in dataTable.Rows)
            {
                Nota novaNota = new(
                    (int)linha["id"],
                    (double)linha["valor_nota"],
                    (Trabalho)new Trabalho(
                        (int)linha["trabalho_id"],
                        (string)linha["descricao"],
                        (DateTime)linha["data_trabalho"],
                        (Professor)new Professor(
                            (DateTime)linha["data_aniversario"],
                            (string)linha["nome"],
                            (string)linha["sobrenome"],
                            (string)linha["telefone"],
                            (string)linha["cpf"],
                            (string)linha["endereco"],
                            (string)linha["email"],
                            (int)linha["professor_id"],
                            (double)linha["salario"]
                        )
                    )
                );
                lista.Add(novaNota);
            }
            return lista;
        }
    }
}
