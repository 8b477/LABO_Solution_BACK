CREATE TABLE Donation
(
   IDUtilisateur  INT IDENTITY, --enlever le Identity
   IDProjet INT,
   DateDonation DATE,
   Montant DECIMAL(10,2) NOT NULL,
   PRIMARY KEY(IDUtilisateur, IDProjet), --delete ceci
   FOREIGN KEY(IDUtilisateur) REFERENCES Utilisateur(IDUtilisateur),
   FOREIGN KEY(IDProjet) REFERENCES Projet(IDProjet)
)
