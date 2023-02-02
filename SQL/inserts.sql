-- Inserir pessoa
INSERT INTO pessoa (nome, sobrenome, telefone, cpf, endereco, email, data_aniversario)
VALUES("", "", "", "", "", "", "");

-- Inserir aluno
INSERT INTO aluno (numero_falta, id_pessoa)
VALUES("", "");

-- Inserir curso
INSERT INTO curso (nome, carga_horaria, ativo)
VALUES ("", "", "");

-- Inserir professor
INSERT INTO professor(salario, id_pessoa)
VALUES ("", "");

-- Inserir materia
INSERT INTO materia (descricao, carga_horaria, id_professor)
VALUES ("", "", "");

-- Tabela intermediaria Materia X Curso
INSERT INTO materias_do_curso( id_curso, id_materia)
VALUES ("", "");

-- Inserir matricula
INSERT INTO matricula(ativa, id_aluno, id_curso)
VALUES ("", "", ""); 

-- Inserir trabalho
INSERT INTO trabalho(descricao, data_trabalho, id_professor)
VALUES("", "", ""); 

-- Inserir Nota
INSERT INTO nota(valor_nota, id_trabalho)
VALUES ("" , "");

-- Tabela intermediaria Professor X Materia
INSERT INTO professor_materias(id_materia, id_professor)
VALUES("", "");

