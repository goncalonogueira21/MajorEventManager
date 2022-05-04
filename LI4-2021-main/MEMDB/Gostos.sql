CREATE TABLE Gostos
(
	fk_Utilizador_id nvarchar(450),
	pk_Gostos INT NOT NULL PRIMARY KEY, 
    Gostos VARCHAR(30),
	FOREIGN KEY(fk_Utilizador_id) REFERENCES AspNetUsers(Id)
)