-- Select para buscar a materia especifica.
SELECT 
    materia.id AS 'Materia ID',
    descricao,
    carga_horaria,
    pessoa.nome AS 'Nome Professor'
FROM
    materia
        INNER JOIN
    professor_materias ON materia.id = professor_materias.id_materia
        AND materia.id = 1
        INNER JOIN
    professor ON professor.id = professor_materias.id_professor
        INNER JOIN
    pessoa ON pessoa.id = professor.id_pessoa;

-- Select para buscar todos os professores de uma materia especifica.
SELECT 
    pessoa.nome,
    pessoa.sobrenome,
    pessoa.telefone,
    pessoa.cpf,
    pessoa.endereco,
    pessoa.email,
    pessoa.data_aniversario,
    professor.id,
    professor.salario
FROM 
	pessoa
		INNER JOIN
	professor ON pessoa.id = professor.id_pessoa
		INNER JOIN 
	professor_materias ON professor_materias.id_professor = professor.id AND professor_materias.id_materia = 1;

-- Select para buscar todos os cursos de uma materia especifica.
SELECT 
	curso.id,
	curso.nome,
    curso.carga_horaria,
    curso.ativo
FROM 
	curso
INNER JOIN
materias_do_curso ON materias_do_curso.id_curso = curso.id AND materias_do_curso.id_materia = 1;

-- Select para buscar uma matricula especifica.
SELECT 
    matricula.id, ativa
FROM
    matricula
INNER JOIN
	aluno ON matricula.id_aluno = aluno.id AND aluno.id = 1;
    
-- Select para buscar os cursos de uma matricula especifica.
SELECT 
	curso.id, 
	curso.nome, 
	curso.carga_horaria, 
	curso.ativo
FROM
	curso
INNER JOIN
	matricula ON matricula.id_curso = curso.id AND matricula.id = 2;

-- Select para trazer o professor do trabalho especifico.
SELECT 
    pessoa.nome,
    pessoa.sobrenome,
    pessoa.telefone,
    pessoa.cpf,
    pessoa.endereco,
    pessoa.email,
    pessoa.data_aniversario,
    professor.id,
    professor.salario,
    trabalho.id as "trabalho_id",
    descricao,
    data_trabalho
FROM
    pessoa
        INNER JOIN
    professor ON pessoa.id = professor.id_pessoa
        INNER JOIN
    trabalho ON professor.id = trabalho.id_professor AND trabalho.id = 1;
   
-- -- Select para trazer todos os trabalhos e seus respectivos professores.
SELECT 
    pessoa.nome,
    pessoa.sobrenome,
    pessoa.telefone,
    pessoa.cpf,
    pessoa.endereco,
    pessoa.email,
    pessoa.data_aniversario,
    professor.id,
    professor.salario,
    trabalho.id as "trabalho_id",
    descricao,
    data_trabalho
FROM
    pessoa
        INNER JOIN
    professor ON pessoa.id = professor.id_pessoa
        INNER JOIN
    trabalho ON professor.id = trabalho.id_professor;
		
-- Selecte em uma nota especifica
SELECT 
	nota.id, 
    valor_nota,
    pessoa.nome,
    pessoa.sobrenome,
    pessoa.telefone,
    pessoa.cpf,
    pessoa.endereco,
    pessoa.email,
    pessoa.data_aniversario,
    professor.id as "professor_id",
    professor.salario,
    trabalho.id as "trabalho_id",
    descricao,
    data_trabalho
FROM
	nota
		INNER JOIN
    pessoa
        INNER JOIN
    professor ON pessoa.id = professor.id_pessoa
        INNER JOIN
    trabalho ON professor.id = trabalho.id_professor AND trabalho.id = 1
		WHERE
	nota.id = 1;
    
	SELECT 
	nota.id, 
	valor_nota,
	pessoa.nome,
	pessoa.sobrenome,
	pessoa.telefone,
	pessoa.cpf,
	pessoa.endereco,
	pessoa.email,
	pessoa.data_aniversario,
	professor.id,
	professor.salario,
	trabalho.id as "trabalho_id",
	descricao,
	data_trabalho
		FROM
	nota
		INNER JOIN
    pessoa
        INNER JOIN
    professor ON pessoa.id = professor.id_pessoa
        INNER JOIN
    trabalho ON professor.id = trabalho.id_professor AND trabalho.id = 1;
    
    -- Select de 1 matricula referente a 1 aluno e 1 curso especifico.
    SELECT 
    matricula.id AS "matricula_id",
    matricula.ativa,
    pessoa.nome AS "nome_aluno",
    pessoa.sobrenome,
    pessoa.telefone,
    pessoa.cpf,
    pessoa.endereco,
    pessoa.email,
    pessoa.data_aniversario,
    aluno.id AS "aluno_id",
    aluno.numero_falta,
    curso.id AS "curso_id",
    curso.nome AS "curso_nome",
    curso.carga_horaria,
    curso.ativo
        FROM
	matricula 
		INNER JOIN
	pessoa
		INNER JOIN
	aluno ON pessoa.id = aluno.id_pessoa AND aluno.id = matricula.id_aluno AND matricula.id = 1
		INNER JOIN
	curso ON curso.id = matricula.id_curso;
    
    select * from materia;
    select * from professor;
    
    SELECT * FROM professor_materias;
    

    
    
    
		