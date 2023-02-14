using Dao;
using MySql.Data.MySqlClient;
using ProjetoEscola.Entities;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Entities
{
    internal class Trabalho
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataTrabalho { get; set; }
        public Professor Professor { get; set; }
        public List<Questao> Questoes { get; set; }

        public Trabalho()
        {
        }

        public Trabalho(long id, string descricao, DateTime dataTrabalho)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.DataTrabalho = dataTrabalho;
        }

        public Trabalho(long id, string descricao, DateTime dataTrabalho, List<Questao> questoes)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.DataTrabalho = dataTrabalho;
            this.Questoes = questoes;
        }

        public Trabalho(long id, string descricao, DateTime dataTrabalho, Professor professor)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.DataTrabalho = dataTrabalho;
            this.Professor = professor;
        }

        public Trabalho(long id, string descricao, DateTime dataTrabalho, List<Questao> questoes, Professor professor)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.DataTrabalho = dataTrabalho;
            this.Questoes = questoes;
            this.Professor = professor;
        }

        public static Trabalho GetById(long id)
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

            string sqlQueryQuestoes =
                "SELECT questoes.id, questoes.descricao, questoes.escolha, trabalho.id AS 'trabalho_id' " +
                "FROM questoes " +
                "INNER JOIN questoes_trabalhos ON questoes.id = questoes_trabalhos.id_questao " +
                $"INNER JOIN trabalho ON trabalho.id = questoes_trabalhos.id_trabalho AND questoes_trabalhos.id_trabalho = {id};";


            MySqlDataReader readerQuestoes = BancoDeDados.PreparaQuery(sqlQueryQuestoes);
            DataTable dataTableQuestoes = new();
            dataTableQuestoes.Load(readerQuestoes);
            List<Questao> listaQuestao = new();
            foreach (DataRow questaoLinha in dataTableQuestoes.Rows)
            {
                Questao questaoAtual = new(
                    (int)questaoLinha["id"],
                    (string)questaoLinha["descricao"],
                    (string)questaoLinha["escolha"]
                );

                listaQuestao.Add(questaoAtual);
            }
            novoTrabalho.Questoes = listaQuestao;

            return novoTrabalho;
        }

        public static List<Trabalho> GetAll()
        {
            List<Trabalho> listaTrabalhos = new();

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
                listaTrabalhos.Add(novoTrabalho);
            }
            return listaTrabalhos;
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

        public static void DeletarCursoMateria(Questao questao, Trabalho trabalho)
        {
            string sql = $"DELETE FROM questoes_trabalhos WHERE id_questao = {questao.Id} AND id_trabalho = {trabalho.Id}";
            BancoDeDados.Delete(sql);
        }

        public static void AdicionarCursoMateria(Questao questao, Trabalho trabalho)
        {
            string sql = $"INSERT INTO questoes_trabalhos (id_questao, id_trabalho) VALUES ({questao.Id}, {trabalho.Id});";
            BancoDeDados.Insert(sql);
        }

        public void SincronizarQuestoes()
        {
            Trabalho trabalhoDoBanco = Trabalho.GetById(Id);

            foreach (Questao questaoAtual in this.Questoes)
            {
                if (!trabalhoDoBanco.Questoes.Contains(questaoAtual))
                {
                    Trabalho.AdicionarCursoMateria(questaoAtual, this);
                }
            }

            foreach (Questao questaoBanco in trabalhoDoBanco.Questoes)
            {
                if (!Questoes.Contains(questaoBanco))
                {
                    Trabalho.DeletarCursoMateria(questaoBanco, this);
                }
            }
        }
    }
}
