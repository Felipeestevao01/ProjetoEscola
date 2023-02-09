using System.Net;
using System.Text;
using Dao;
using Entities;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

                if (req.HttpMethod == "GET")
                {
                    //API alunos
                    if (path == "/aluno")
                    {
                        Aluno alunoAtual = Aluno.GetById(1);
                        data = JsonSerializer.Serialize(alunoAtual);
                        //List<Aluno> alunos = Aluno.GetAll();
                        //data = JsonSerializer.Serialize(alunos);
                    }

                    //API cursos
                    else if (path == "/curso")
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
                        //Nota notaAtual = Nota.GetById(1);
                        //data = JsonSerializer.Serialize(notaAtual);
                        List<Nota> notas = Nota.GetAll();
                        data = JsonSerializer.Serialize(notas);
                    }

                    //API trabalhos
                    else if (path == "/trabalho")
                    {
                        //Trabalho trabalhoAtual = Trabalho.GetById(1);
                        //data = JsonSerializer.Serialize(trabalhoAtual);
                        List<Trabalho> trabalhos = Trabalho.GetAll();
                        data = JsonSerializer.Serialize(trabalhos);
                    }

                    //API matricula
                    else if (path == "/matricula")
                    {
                        Matricula matriculaAtual = Matricula.GetById(2);
                        data = JsonSerializer.Serialize(matriculaAtual);
                    }
                }
                else if (req.HttpMethod == "POST")
                {
                    string jsonText;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        jsonText = reader.ReadToEnd();
                    }

                    if (path == "/aluno/new")
                    {
                        Aluno aluno = JsonConvert.DeserializeObject<Aluno>(jsonText);
                        aluno.Salvar();

                        data = JsonSerializer.Serialize(aluno);
                    }
                    else if (path == "/aluno/update")
                    {
                        Aluno aluno = JsonConvert.DeserializeObject<Aluno>(jsonText);
                        if (aluno.Id != 0)
                        {
                            aluno.Atualizar();
                        }
                        else
                        {
                            resp.StatusCode = 400;
                            data = "{\"Error\":\"Update precisa de um ID.\"}";
                        }
                    }
                    else if (path == "/professor/new")
                    {
                        Professor professor = JsonConvert.DeserializeObject<Professor>(jsonText);
                        professor.Salvar();

                        data = JsonSerializer.Serialize(professor);
                    }
                    else if (path == "/professor/update")
                    {
                        Professor professor = JsonConvert.DeserializeObject<Professor>(jsonText);
                        if (professor.Id != 0)
                        {
                            professor.Atualizar();
                        }
                        else
                        {
                            resp.StatusCode = 400;
                            data = "{\"Error\":\"Update precisa de um ID.\"}";
                        }
                    }
                    else if (path == "/curso/new")
                    {
                        Curso curso = JsonConvert.DeserializeObject<Curso>(jsonText);
                        curso.Salvar();

                        data = JsonSerializer.Serialize(curso);
                    }
                    else if (path == "/materia/new")
                    {
                        Materia materia = JsonConvert.DeserializeObject<Materia>(jsonText);
                        materia.Salvar();

                        data = JsonSerializer.Serialize(materia);
                    }
                    else if (path == "/nota/new")
                    {
                        Nota nota = JsonConvert.DeserializeObject<Nota>(jsonText);
                        nota.Salvar();

                        data = JsonSerializer.Serialize(nota);
                    }
                    else if (path == "/trabalho/new")
                    {
                        Trabalho trabalho = JsonConvert.DeserializeObject<Trabalho>(jsonText);
                        trabalho.Salvar();

                        data = JsonSerializer.Serialize(trabalho);
                    }
                    else if (path == "/matricula/new")
                    {
                        Matricula matricula = JsonConvert.DeserializeObject<Matricula>(jsonText);
                        matricula.Salvar();

                        data = JsonSerializer.Serialize(matricula);
                    }
                }
                else if (req.HttpMethod == "DELETE")
                {
                    string jsonText;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        jsonText = reader.ReadToEnd();
                    }
                    if(path == "/aluno/delete")
                    {
                        Aluno aluno = JsonConvert.DeserializeObject<Aluno>(jsonText);
                        aluno.Deletar();
                        

                    }
                    else if (path == "/curso/delete")
                    {
                        Curso curso = JsonConvert.DeserializeObject<Curso>(jsonText);
                        curso.Deletar();
                    }
                    else if (path == "/materia/delete")
                    {
                        Materia materia = JsonConvert.DeserializeObject<Materia>(jsonText);
                        materia.Deletar();
                    }
                    else if (path == "/matricula/delete")
                    {
                        Matricula matricula = JsonConvert.DeserializeObject<Matricula>(jsonText);
                        matricula.Deletar();
                    }
                    else if (path == "/pessoa/delete")
                    {
                        Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(jsonText);
                        pessoa.Deletar();
                    }
                    else if (path == "/professor/delete")
                    {
                        Professor professor = JsonConvert.DeserializeObject<Professor>(jsonText);
                        professor.Deletar();
                    }
                    else if (path == "/questao/delete")
                    {
                        Questao questao = JsonConvert.DeserializeObject<Questao>(jsonText);
                        questao.Deletar();
                    }
                    else if (path == "/trabalho/delete")
                    {
                        Trabalho trabalho = JsonConvert.DeserializeObject<Trabalho>(jsonText);
                        trabalho.Deletar();
                    }
                }

                byte[] buffer = Encoding.UTF8.GetBytes(data);
                resp.ContentLength64 = buffer.Length;

                using Stream ros = resp.OutputStream;
                ros.Write(buffer, 0, buffer.Length);
            }
        }
    }
}