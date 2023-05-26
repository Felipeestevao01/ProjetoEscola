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

select * from questoes;
select * from trabalho;
SELECT * FROM questoes_trabalhos;
select * from materias_do_curso;

SELECT * from questoes;
SELECT* FROM questoes_trabalhos;
SELECT * from trabalho;


SELECT questoes.id, questoes.descricao, questoes.escolha, trabalho.id AS 'trabalho_id'
FROM questoes
INNER JOIN 
questoes_trabalhos ON questoes.id = questoes_trabalhos.id_questao
INNER JOIN
trabalho ON trabalho.id = questoes_trabalhos.id_trabalho AND questoes_trabalhos.id_trabalho = 2;


select * from pessoa;
select * from aluno;

SELECT 
    pessoa.nome,
    pessoa.sobrenome,
    pessoa.telefone,
    pessoa.cpf,
    pessoa.endereco,
    pessoa.email,
    pessoa.data_aniversario,
    aluno.id,
    aluno.numero_falta
FROM
    pessoa
        INNER JOIN
    aluno
WHERE
    pessoa.id = aluno.id_pessoa;

