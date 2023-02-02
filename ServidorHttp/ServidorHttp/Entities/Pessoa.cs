using System;

namespace Entities
{
    internal class Pessoa
    {
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

        public int GetIdade()
        {
            return DataAniversario.Year - DateTime.Now.Year;
        }
    }
}