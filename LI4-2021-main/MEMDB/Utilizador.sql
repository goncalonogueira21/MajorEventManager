CREATE TABLE Utilizador
(
	IdUser INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    Email VARCHAR(30) UNIQUE NOT NULL, 
    Password VARCHAR(30) NOT NULL,
    fk_Gostos INT
	FOREIGN KEY(fk_Gostos) REFERENCES Gostos(pk_Gostos)
)