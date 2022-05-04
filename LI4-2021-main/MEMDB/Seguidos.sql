CREATE TABLE Seguidos
(
	fk_Utilizador_id nvarchar(450),
    fk_Evento_Id INT,
	FOREIGN KEY(fk_Utilizador_id) REFERENCES AspNetUsers(Id),
	FOREIGN KEY(fk_Evento_Id) REFERENCES Evento(Id)
)