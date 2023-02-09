-- CREATE DATABASE projetoescola;

-- Criação tabela de pessoa
CREATE TABLE pessoa (
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
nome VARCHAR(80) NOT NULL,
sobrenome VARCHAR(80) NOT NULL,
telefone VARCHAR(25) NOT NULL,
cpf VARCHAR(20) NOT NULL,
endereco VARCHAR(150) NOT NULL,
email VARCHAR(70) NOT NULL,
data_aniversario DATE NOT NULL);

-- Criação tabela curso
CREATE TABLE curso(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
nome VARCHAR(100) NOT NULL,
ativo BOOL NOT NULL);

-- Criação tabela professor
CREATE TABLE professor(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
salario DOUBLE NOT NULL,
id_pessoa INT NOT NULL,
FOREIGN KEY(id_pessoa) REFERENCES pessoa(id));

-- Criação tabela trabalho
CREATE TABLE trabalho(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
descricao VARCHAR(200) NOT NULL,
data_trabalho DATE NOT NULL,
id_professor INT NOT NULL, 
FOREIGN KEY(id_professor) REFERENCES professor(id));

-- Criação tabela Nota
CREATE TABLE nota(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
valor_nota DOUBLE NOT NULL,
id_trabalho INT NOT NULL,
FOREIGN KEY(id_trabalho) REFERENCES trabalho(id));

-- Criação tabela questoes
CREATE TABLE questoes(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
descricao VARCHAR(220) NOT NULL,
escolha VARCHAR(200));

-- Criação tabela intermediaria questoes_trabalhos
CREATE TABLE questoes_trabalhos(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
id_questao INT NOT NULL,
FOREIGN KEY(id_questao) REFERENCES questoes(id),
id_trabalho INT NOT NULL,
FOREIGN KEY(id_trabalho) REFERENCES trabalho(id));

-- Criação tabela materia
CREATE TABLE materia(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
descricao VARCHAR(100) NOT NULL,
carga_horaria DOUBLE NOT NULL,
id_professor INT,
FOREIGN KEY(id_professor) REFERENCES professor(id));

-- Criação tabela intermedaria materias_do_curso
CREATE TABLE materias_do_curso(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
id_curso INT NOT NULL,
FOREIGN KEY(id_curso) REFERENCES curso(id),
id_materia INT NOT NULL,
FOREIGN KEY(id_materia) REFERENCES materia(id));

-- Criação tabela intermediaria professor_materias
CREATE TABLE professor_materias(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
id_materia INT NOT NULL,
FOREIGN KEY(id_materia) REFERENCES materia(id),
id_professor int NOT NULL,
FOREIGN KEY(id_professor) REFERENCES professor(id));

-- Criação tabela aluno
CREATE TABLE aluno(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
numero_falta INT,
id_pessoa INT NOT NULL,
FOREIGN KEY(id_pessoa) REFERENCES pessoa(id));

-- Criação tabela matricula
CREATE TABLE matricula(
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL, 
ativa Bool,
id_aluno INT,
FOREIGN KEY(id_aluno) REFERENCES aluno(id),
id_curso INT,
FOREIGN KEY(id_curso) REFERENCES curso(id));

ALTER TABLE nota
ADD id_aluno INT NOT NULL, 
ADD FOREIGN KEY(id_aluno) REFERENCES aluno(id);

