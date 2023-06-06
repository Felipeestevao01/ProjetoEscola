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
            this.Porta = porta;
            this.Dominio = dominio;
        }

        public void Servir()
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add($"{this.Dominio}:{this.Porta}/");

            listener.Start();

            Console.WriteLine($"Escutando na porta {this.Porta}...");

            while (true)
                { 
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest req = context.Request;

                Console.WriteLine($"Received request for {req.Url}");

                using HttpListenerResponse resp = context.Response;
                resp.Headers.Set("Content-Type", "application/json");

                var path = req.RawUrl;
                string data = "";

                if (path.Contains("/professores"))
                {
                    string[] urlComponentes = path.Split('/');
                    string jsonText;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        jsonText = reader.ReadToEnd();
                    }

                    if (req.HttpMethod == "GET")
                    {
                        if(urlComponentes.Length == 3)
                        {
                            int id = int.Parse(urlComponentes[2]);
                            Professor professor = Professor.GetById(id);
                            data = JsonSerializer.Serialize(professor);
                        }   
                        else
                        {
                            List<Professor> professores = Professor.GetAll();
                            data = JsonSerializer.Serialize(professores);
                        }
                    }
                    else if(req.HttpMethod == "POST")
                    {
                        Professor professor = JsonConvert.DeserializeObject<Professor>(jsonText);
                        professor.Salvar();
                        data = JsonSerializer.Serialize(professor);
                    }
                    else if(req.HttpMethod == "PUT")
                    {
                        try
                        {
                            Professor professor = JsonConvert.DeserializeObject<Professor>(jsonText);
                            if (professor.Id > 0)
                            {
                                professor.Atualizar();
                            }
                            else
                            {
                                resp.StatusCode = 400;
                                data = "{\"Error\":\"Update precisa de um ID.\"}";
                            }
                            data = JsonSerializer.Serialize(professor);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    else if(req.HttpMethod == "DELETE")
                    {
                        Professor professor = JsonConvert.DeserializeObject<Professor>(jsonText);
                        professor.Deletar();
                    }
                }
                else if (path.Contains("/alunos"))
                {
                    string[] urlComponentes = path.Split('/');
                    string jsonText;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        jsonText = reader.ReadToEnd();
                    }

                    if (req.HttpMethod == "GET")
                    {
                        if (urlComponentes.Length == 3)
                        {
                            int id = int.Parse(urlComponentes[2]);
                            Aluno aluno = Aluno.GetById(id);
                            data = JsonSerializer.Serialize(aluno);
                        }
                        else
                        {
                            List<Aluno> alunos = Aluno.GetAll();
                            data = JsonSerializer.Serialize(alunos);
                        }
                    }
                    else if (req.HttpMethod == "POST")
                    {
                        Aluno aluno = JsonConvert.DeserializeObject<Aluno>(jsonText);
                        aluno.Salvar();
                        data = JsonSerializer.Serialize(aluno);
                    }
                    else if (req.HttpMethod == "PUT")
                    {
                        try
                        {
                            Aluno aluno = JsonConvert.DeserializeObject<Aluno>(jsonText);
                            if (aluno.Id > 0)
                            {
                                aluno.Atualizar();
                            }
                            else
                            {
                                resp.StatusCode = 400;
                                data = "{\"Error\":\"Update precisa de um ID.\"}";
                            }
                            data = JsonSerializer.Serialize(aluno);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    else if (req.HttpMethod == "DELETE")
                    {
                        Aluno aluno = JsonConvert.DeserializeObject<Aluno>(jsonText);
                        aluno.Deletar();
                    }
                }
                else if (path.Contains("/cursos"))
                {
                    string[] urlComponentes = path.Split('/');
                    string jsonText;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        jsonText = reader.ReadToEnd();
                    }

                    if (req.HttpMethod == "GET")
                    {
                        if (urlComponentes.Length == 3)
                        {
                            int id = int.Parse(urlComponentes[2]);
                            Curso curso = Curso.GetById(id);
                            data = JsonSerializer.Serialize(curso);
                        }
                        else
                        {
                            List<Curso> cursos = Curso.GetAll();
                            data = JsonSerializer.Serialize(cursos);
                        }
                    }
                    else if (req.HttpMethod == "POST")
                    {
                        Curso curso = JsonConvert.DeserializeObject<Curso>(jsonText);
                        curso.Salvar();
                        data = JsonSerializer.Serialize(curso);
                    }
                    else if (req.HttpMethod == "PUT")
                    {
                        try
                        {
                            Curso curso = JsonConvert.DeserializeObject<Curso>(jsonText);
                            if (curso.Id > 0)
                            {
                                curso.Atualizar();
                            }
                            else
                            {
                                resp.StatusCode = 400;
                                data = "{\"Error\":\"Update precisa de um ID.\"}";
                            }
                            data = JsonSerializer.Serialize(curso);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    else if (req.HttpMethod == "DELETE")
                    {
                        Curso curso = JsonConvert.DeserializeObject<Curso>(jsonText);
                        curso.Deletar();
                    }
                }
                else if (path.Contains("/materias"))
                {
                    string[] urlComponentes = path.Split('/');
                    string jsonText;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        jsonText = reader.ReadToEnd();
                    }

                    if (req.HttpMethod == "GET")
                    {
                        if (urlComponentes.Length == 3)
                        {
                            int id = int.Parse(urlComponentes[2]);
                            Materia materia = Materia.GetById(id);
                            data = JsonSerializer.Serialize(materia);
                        }
                        else
                        {
                            List<Materia> materias = Materia.GetAll();
                            data = JsonSerializer.Serialize(materias);
                        }
                    }
                    else if (req.HttpMethod == "POST")
                    {
                        Materia materia = JsonConvert.DeserializeObject<Materia>(jsonText);
                        materia.Salvar();
                        data = JsonSerializer.Serialize(materia);
                    }
                    else if (req.HttpMethod == "PUT")
                    {
                        try
                        {
                            Materia materia = JsonConvert.DeserializeObject<Materia>(jsonText);
                            if (materia.Id > 0)
                            {
                                materia.Atualizar();
                            }
                            else
                            {
                                resp.StatusCode = 400;
                                data = "{\"Error\":\"Update precisa de um ID.\"}";
                            }
                            data = JsonSerializer.Serialize(materia);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    else if (req.HttpMethod == "DELETE")
                    {
                        Materia materia = JsonConvert.DeserializeObject<Materia>(jsonText);
                        materia.Deletar();
                    }
                }
                else if (path.Contains("/notas"))
                {
                    string[] urlComponentes = path.Split('/');
                    string jsonText;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        jsonText = reader.ReadToEnd();
                    }

                    if (req.HttpMethod == "GET")
                    {
                        if (urlComponentes.Length == 3)
                        {
                            int id = int.Parse(urlComponentes[2]);
                            Nota nota = Nota.GetById(id);
                            data = JsonSerializer.Serialize(nota);
                        }
                        else
                        {
                            List<Nota> nota = Nota.GetAll();
                            data = JsonSerializer.Serialize(nota);
                        }
                    }
                    else if (req.HttpMethod == "POST")
                    {
                        Nota nota = JsonConvert.DeserializeObject<Nota>(jsonText);
                        nota.Salvar();
                        data = JsonSerializer.Serialize(nota);
                    }
                    else if (req.HttpMethod == "PUT")
                    {
                        try
                        {
                            Nota nota = JsonConvert.DeserializeObject<Nota>(jsonText);
                            if (nota.Id > 0)
                            {
                                nota.Atualizar();
                            }
                            else
                            {
                                resp.StatusCode = 400;
                                data = "{\"Error\":\"Update precisa de um ID.\"}";
                            }
                            data = JsonSerializer.Serialize(nota);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    else if (req.HttpMethod == "DELETE")
                    {
                        Nota nota = JsonConvert.DeserializeObject<Nota>(jsonText);
                        nota.Deletar();
                    }
                }
                else if (path.Contains("/questoes"))
                {
                    string[] urlComponentes = path.Split('/');
                    string jsonText;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        jsonText = reader.ReadToEnd();
                    }

                    if (req.HttpMethod == "GET")
                    {
                        if (urlComponentes.Length == 3)
                        {
                            int id = int.Parse(urlComponentes[2]);
                            Questao questao = Questao.GetById(id);
                            data = JsonSerializer.Serialize(questao);
                        }
                        else
                        {
                            List<Questao> questao = Questao.GetAll();
                            data = JsonSerializer.Serialize(questao);
                        }
                    }
                    else if (req.HttpMethod == "POST")
                    {
                        Questao questao = JsonConvert.DeserializeObject<Questao>(jsonText);
                        questao.Salvar();
                        data = JsonSerializer.Serialize(questao);
                    }
                    else if (req.HttpMethod == "PUT")
                    {
                        try
                        {
                            Questao questao = JsonConvert.DeserializeObject<Questao>(jsonText);
                            if (questao.Id > 0)
                            {
                                questao.Atualizar();
                            }
                            else
                            {
                                resp.StatusCode = 400;
                                data = "{\"Error\":\"Update precisa de um ID.\"}";
                            }
                            data = JsonSerializer.Serialize(questao);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    else if (req.HttpMethod == "DELETE")
                    {
                        Questao questao = JsonConvert.DeserializeObject<Questao>(jsonText);
                        questao.Deletar();
                    }
                }
                else if (path.Contains("/trabalhos"))
                {
                    string[] urlComponentes = path.Split('/');
                    string jsonText;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        jsonText = reader.ReadToEnd();
                    }

                    if (req.HttpMethod == "GET")
                    {
                        if (urlComponentes.Length == 3)
                        {
                            int id = int.Parse(urlComponentes[2]);
                            Trabalho trabalho = Trabalho.GetById(id);
                            data = JsonSerializer.Serialize(trabalho);
                        }
                        else
                        {
                            List<Trabalho> trabalho = Trabalho.GetAll();
                            data = JsonSerializer.Serialize(trabalho);
                        }
                    }
                    else if (req.HttpMethod == "POST")
                    {
                        Trabalho trabalho = JsonConvert.DeserializeObject<Trabalho>(jsonText);
                        trabalho.Salvar();
                        data = JsonSerializer.Serialize(trabalho);
                    }
                    else if (req.HttpMethod == "PUT")
                    {
                        try
                        {
                            Trabalho trabalho = JsonConvert.DeserializeObject<Trabalho>(jsonText);
                            if (trabalho.Id > 0)
                            {
                                trabalho.Atualizar();
                            }
                            else
                            {
                                resp.StatusCode = 400;
                                data = "{\"Error\":\"Update precisa de um ID.\"}";
                            }
                            data = JsonSerializer.Serialize(trabalho);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    else if (req.HttpMethod == "DELETE")
                    {
                        Trabalho trabalho = JsonConvert.DeserializeObject<Trabalho>(jsonText);
                        trabalho.Deletar();
                    }
                }

                byte[] buffer = Encoding.UTF8.GetBytes(data);
                resp.ContentLength64 = buffer.Length;
                resp.AddHeader("access-control-allow-origin", "*");
                resp.AddHeader("access-control-allow-methods", "*");
                using Stream ros = resp.OutputStream;
                ros.Write(buffer, 0, buffer.Length);
            }
        }
    }
}