CREATE TABLE Donation
(
   IDUtilisateur  INT,
   IDProjet INT,
   DateDonation DATE,
   Montant DECIMAL(10,2) NOT NULL,
   FOREIGN KEY(IDUtilisateur) REFERENCES Utilisateur(IDUtilisateur),
   FOREIGN KEY(IDProjet) REFERENCES Projet(IDProjet)
)
