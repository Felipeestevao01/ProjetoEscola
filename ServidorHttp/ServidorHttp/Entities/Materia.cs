using Dao;
using MySql.Data.MySqlClient;
using System.Data;

namespace Entities
{
    internal class Materia
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public List<Professor> Professores { get; set; }
        public List<Curso> Cursos { get; set; }

        public Materia()
        {
        }


        public Materia(string nome, double cargaHoraria)
        {
            Nome = nome;
            CargaHoraria = cargaHoraria;
        }

        public Materia(long id, string nome, double cargaHoraria)
        {
            Id = id;
            Nome = nome;
            CargaHoraria = cargaHoraria;
        }

        public Materia(long id, string nome, double cargaHoraria, List<Professor> professores)
        {
            Id = id;
            Nome = nome;
            CargaHoraria = cargaHoraria;
            Professores = professores;
        }

        public Materia(long id, string nome, double cargaHoraria, List<Professor> professores, List<Curso> cursos)
        {
            Id = id;
            Nome = nome;
            CargaHoraria = cargaHoraria;
            Professores = professores;
            Cursos = cursos;
        }

        public static Materia GetById(int id)
        {
            // Select para buscar a materia especifica.
            string sqlQueryMateria = $"SELECT id, descricao, carga_horaria FROM materia WHERE id = {id};";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQueryMateria);

            DataTable dataTable = new();
            dataTable.Load(reader);
            DataRow linha = dataTable.Rows[0];

            Materia novaMateria = new(
                (int)linha["id"],
                (string)linha["descricao"],
                (double)linha["carga_horaria"]
            );

            // Select para buscar todos os professores de uma materia especifica.
            string sqlQueryProfessores =
                "SELECT pessoa.nome, pessoa.sobrenome, pessoa.telefone, pessoa.cpf, " +
                "pessoa.endereco, pessoa.email, pessoa.data_aniversario, professor.id, professor.salario " +
                "FROM pessoa " +
                "INNER JOIN " +
                "professor ON pessoa.id = professor.id_pessoa " +
                "INNER JOIN " +
                $"professor_materias ON professor_materias.id_professor = professor.id AND professor_materias.id_materia = {id};";

            MySqlDataReader readerProfessores = BancoDeDados.PreparaQuery(sqlQueryProfessores);
            DataTable dataTableProfessor = new();
            dataTableProfessor.Load(readerProfessores);
            List<Professor> listaProfessores = new List<Professor>();
            foreach(DataRow professorLinha in dataTableProfessor.Rows)
            {
                Professor professorAtual = new Professor(
                    (DateTime)professorLinha["data_aniversario"],
                    (string)professorLinha["nome"],
                    (string)professorLinha["sobrenome"],
                    (string)professorLinha["telefone"],
                    (string)professorLinha["cpf"],
                    (string)professorLinha["endereco"],
                    (string)professorLinha["email"],
                    (int)professorLinha["id"],
                    (double)professorLinha["salario"]
                );
                listaProfessores.Add(professorAtual);
            }
            novaMateria.Professores = listaProfessores;

            // Select para buscar todos os cursos de uma materia especifica.
            string sqlQueryCursos =
                    "SELECT curso.id, curso.nome, curso.carga_horaria, curso.ativo " +
                    "FROM curso " +
                    "INNER JOIN " +
                    $"materias_do_curso ON materias_do_curso.id_curso = curso.id AND materias_do_curso.id_materia = {id};";

            MySqlDataReader readerCursos = BancoDeDados.PreparaQuery(sqlQueryCursos);
            DataTable dataTableCursos = new();
            dataTableCursos.Load(readerCursos);
            List<Curso> listaCursos = new();
            foreach(DataRow cursoLinha in dataTableCursos.Rows)
            {
                Curso cursoAtual = new(
                    (int)cursoLinha["id"],
                    (string)cursoLinha["nome"],
                    (double)cursoLinha["carga_horaria"],
                    (bool)cursoLinha["ativo"]
                );

                listaCursos.Add(cursoAtual);
            }
            novaMateria.Cursos = listaCursos;

            return novaMateria;
        }

        public static List<Materia> GetAll()
        {
            List<Materia> lista = new();

            string sqlQuery = "SELECT id, descricao, carga_horaria FROM materia;";
            MySqlDataReader reader = BancoDeDados.PreparaQuery(sqlQuery);

            DataTable dataTable = new();
            dataTable.Load(reader);

            foreach (DataRow linha in dataTable.Rows)
            {
                Materia novaMateria = new(
                   (int)linha["id"],
                   (string)linha["descricao"],
                   (double)linha["carga_horaria"]

               );
               lista.Add(novaMateria);
            }
            return lista;
        }

        public void Salvar()
        {
            
            string sqlInserirMateria =
                    "INSERT INTO materia " +
                    "(descricao, carga_horaria, id_professor) " +
                    $"VALUES (\"{Nome}\", \"{CargaHoraria}\", \"{Professores}\");";

            long idMateria = BancoDeDados.Insert(sqlInserirMateria);
            this.Id = idMateria;
        }
    }
}
