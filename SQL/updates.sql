-- Atualizar pessoa
UPDATE pessoa
SET 
nome = "",
sobrenome = "",
telefone = "",
cpf = "",
endereco = "",
email = "",
data_anivesario = ""
WHERE id = "";

-- Atualizar aluno
UPDATE aluno
SET numero_falta = "",
id_pessoa = ""
WHERE id = "";

-- Atualizar professor
UPDATE professor
SET salario = ""
WHERE id = "";

-- Atualizar curso
UPDATE curso
SET nome = "",
carga_horaria = "",
ativo = ""
WHERE id = "";

-- Atualizar materia
UPDATE materia
SET descricao = "",
carga_horaria = ""
WHERE id = "";

-- Atualizar nota
UPDATE nota
SET valor_nota = ""
WHERE id = "";

-- Atualizar trabalho
UPDATE trabalho
SET descricao = "",
data_trabalho = ""
WHERE id = "";

-- Atualizar matricula
UPDATE matricula
SET ativa = ""
WHERE id = "";

-- Atualizar Questao
UPDATE questoes
SET descricao = "",
escolha = ""
WHERE id = "";