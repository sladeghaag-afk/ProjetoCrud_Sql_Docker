CREATE TABLE PESSOAS(
     ID                  INT            IDENTITY(1,1)         PRIMARY KEY,
     NOME                VARCHAR(150)   NOT NULL,
     EMAIL               VARCHAR(100)   NOT NULL,
     CPF                 CHAR(11)       NOT NULL UNIQUE,
     DATAHORACADASTRO    DATETIME       DEFAULT GETDATE());
     