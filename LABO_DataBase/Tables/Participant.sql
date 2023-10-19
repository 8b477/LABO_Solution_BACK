CREATE TABLE Participant
(
   IDUtilisateur  INT,
   IDContrepartie INT,
   DateParticipation DATE,
   PRIMARY KEY(IDUtilisateur, IDContrepartie),
   FOREIGN KEY(IDUtilisateur) REFERENCES Utilisateur(IDUtilisateur),
   FOREIGN KEY(IDContrepartie) REFERENCES Contrepartie(IDContrepartie)
)
