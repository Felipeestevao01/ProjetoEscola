using Dao;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Entities
{
    internal class Pessoa
    {
        public long Id { get; set; }
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

        public Pessoa(long id, DateTime dataAniversario, string nome, string sobrenome, string telefone, string cpf, string endereco, string email)
        {
            Id = id;
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
            this.Id = id;
        }
    }
}