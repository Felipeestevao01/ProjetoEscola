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
            this.DataAniversario = dataAniversario;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.Cpf = cpf;
            this.Endereco = endereco;
            this.Email = email;
        }

        public Pessoa(long idPessoa, DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email)
        {
            this.IdPessoa = idPessoa;
            this.DataAniversario = dataAniversario;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.Cpf = cpf;
            this.Endereco = endereco;
            this.Email = email;
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
                $"(\"{this.Nome}\", \"{this.Sobrenome}\", \"{this.Telefone}\", \"{this.Cpf}\", \"{this.Endereco}\", \"{this.Email}\", \"{this.DataAniversario.ToString("yyyy-MM-ddTHH:mm:ss")}\");";

            long id = BancoDeDados.Insert(sqlInserirPessoa);
            this.IdPessoa = id;
        }

        public void Deletar()
        {
            string sqlDeletarPessoa = $"DELETE FROM pessoa WHERE id = {this.IdPessoa};";
            BancoDeDados.Delete(sqlDeletarPessoa);
        }

        public void Atualizar()
        {
            string sqlAtualizarPessoa =
                "UPDATE pessoa " +
                $"SET nome = \"{this.Nome}\", sobrenome = \"{this.Sobrenome}\", telefone = \"{this.Telefone}\", cpf = \"{this.Cpf}\", endereco = \"{this.Endereco}\", " +
                $"email = \"{this.Email}\", data_aniversario = \"{this.DataAniversario.ToString("yyyy-MM-ddTHH:mm:ss")}\" " +
                $"WHERE id = \"{this.IdPessoa}\";";
            BancoDeDados.Update(sqlAtualizarPessoa);
        }
    }
}