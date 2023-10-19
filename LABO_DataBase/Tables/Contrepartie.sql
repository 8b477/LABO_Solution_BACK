CREATE TABLE Contrepartie
(
   IDContrepartie  INT IDENTITY,
   Montant DECIMAL(15,2) NOT NULL,
   Description VARCHAR(250) NOT NULL,
   IDProjet INT NOT NULL,
   PRIMARY KEY(IDContrepartie),
   FOREIGN KEY(IDProjet) REFERENCES Projet(IDProjet)
)
