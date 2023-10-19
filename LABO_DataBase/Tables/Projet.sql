CREATE TABLE Projet
(
   IDProjet  INT IDENTITY,
   Nom VARCHAR(100) UNIQUE NOT NULL,
   Montant DECIMAL(10,2) NOT NULL,
   DateCreation DATE NOT NULL,
   DateMiseEnLigne DATE,
   DateDeFin DATE,
   EstValid BIT DEFAULT 0 NOT NULL,
   IDUtilisateur INT NOT NULL,
   PRIMARY KEY(IDProjet),
   FOREIGN KEY(IDUtilisateur) REFERENCES Utilisateur(IDUtilisateur)
)
