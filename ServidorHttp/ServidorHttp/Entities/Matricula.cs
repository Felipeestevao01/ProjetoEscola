using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    internal class Matricula
    {
        public int Id { get; set; }
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


    }
}
