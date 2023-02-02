using System.Net;
using System.Text;
using System.Text.Json;
using Entities;

namespace ProjetoEscola.Entities
{
    internal class ServidorHttp
    {
        public int Porta { get; set; }
        public string Dominio { get; set; }

        public ServidorHttp(string dominio, int porta)
        {
            Porta = porta;
            Dominio = dominio;
        }

        public void Servir()
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add($"{Dominio}:{Porta}/");

            listener.Start();

            Console.WriteLine($"Escutando na porta {Porta}...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest req = context.Request;

                Console.WriteLine($"Received request for {req.Url}");

                using HttpListenerResponse resp = context.Response;
                resp.Headers.Set("Content-Type", "application/json");

                var path = req.RawUrl;
                string data = "";

                //API alunos
                if (path == "/aluno")
                {
                    Aluno alunoAtual = Aluno.GetById(1);
                    data = JsonSerializer.Serialize(alunoAtual);
                    //List<Aluno> alunos = Aluno.GetAll();
                    //data = JsonSerializer.Serialize(alunos);
                }
                
                //API cursos
                else if(path == "/curso")
                {
                    List<Curso> cursos = Curso.GetAll();
                    data = JsonSerializer.Serialize(cursos);
                }

                //API professores
                else if (path == "/professor")
                {
                    List<Professor> professores = Professor.GetAll();
                    data = JsonSerializer.Serialize(professores);
                }

                //API materias
                else if (path == "/materia")
                {
                    Materia materiaAtual = Materia.GetById(1);
                    data = JsonSerializer.Serialize(materiaAtual);
                }

                //API questões
                else if (path == "/questao")
                {
                    List<Questao> questoes = Questao.GetAll();
                    data = JsonSerializer.Serialize(questoes);
                }

                //API notas
                else if (path == "/nota")
                {
                    List<Nota> notas = Nota.GetAll();
                    data = JsonSerializer.Serialize(notas);
                }


                //API trabalhos
                else if (path == "/trabalho")
                {
                    List<Trabalho> trabalhos = Trabalho.GetAll();
                    data = JsonSerializer.Serialize(trabalhos);
                }


                byte[] buffer = Encoding.UTF8.GetBytes(data);
                resp.ContentLength64 = buffer.Length;

                using Stream ros = resp.OutputStream;
                ros.Write(buffer, 0, buffer.Length);
            }
        }
    }
}