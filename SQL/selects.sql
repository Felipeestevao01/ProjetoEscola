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
	

