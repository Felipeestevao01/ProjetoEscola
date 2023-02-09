using Dao;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Entities
{
    internal class Pessoa
    {
        public long IdPessoa { get; set; }
        public DateTime DataAniversario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string Cpf  { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }

        public Pessoa()
        {
        }

        public Pessoa(DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email)
        {
            DataAniversario = dataAniversario;
            Nome = nome;
            Sobrenome = sobrenome;
            Telefone = telefone;
            Cpf = cpf;
            Endereco = endereco;
            Email = email;
        }

        public Pessoa(long idPessoa, DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email)
        {
            IdPessoa = idPessoa;
            DataAniversario = dataAniversario;
            Nome = nome;
            Sobrenome = sobrenome;
            Telefone = telefone;
            Cpf = cpf;
            Endereco = endereco;
            Email = email;
        }

        public int GetIdade()
        {
            return DataAniversario.Year - DateTime.Now.Year;
        }

        public void Salvar()
        {
            string sqlInserirPessoa =
                "INSERT INTO pessoa (nome, sobrenome, telefone, cpf, endereco, email, data_aniversario) " +
                "VALUES " +
                $"(\"{Nome}\", \"{Sobrenome}\", \"{Telefone}\", \"{Cpf}\", \"{Endereco}\", \"{Email}\", \"{DataAniversario.ToString("yyyy-MM-ddTHH:mm:ss")}\");";

            long id = BancoDeDados.Insert(sqlInserirPessoa);
            this.IdPessoa = id;
        }

        public void Deletar()
        {
            string sqlDeletarPessoa = $"DELETE FROM pessoa WHERE id = {IdPessoa};";
            BancoDeDados.Delete(sqlDeletarPessoa);
        }

        public void Atualizar()
        {
            string sqlAtualizarPessoa =
                "UPDATE pessoa " +
                $"SET nome = \"{Nome}\", sobrenome = \"{Sobrenome}\", telefone = \"{Telefone}\", cpf = \"{Cpf}\", endereco = \"{Endereco}\", " +
                $"email = \"{Email}\", data_aniversario = \"{DataAniversario.ToString("yyyy-MM-ddTHH:mm:ss")}\" " +
                $"WHERE id = \"{IdPessoa}\";";

            BancoDeDados.Update(sqlAtualizarPessoa);
        }
    }
}