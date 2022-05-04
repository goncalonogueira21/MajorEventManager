CREATE TABLE Notificacao
(
	Id INT NOT NULL PRIMARY KEY,
	fk_Utilizador_id nvarchar(450),
    fk_Evento_Id INT,
	Hora date,
	FOREIGN KEY(fk_Utilizador_id) REFERENCES AspNetUsers(Id),
	FOREIGN KEY(fk_Evento_Id) REFERENCES Evento(Id)
)