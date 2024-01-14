﻿CREATE TABLE Utilisateur
(
   IDUtilisateur  INT IDENTITY,
   Nom VARCHAR(50) NOT NULL,
   Prenom VARCHAR(50) NOT NULL,
   Email VARCHAR(150) UNIQUE NOT NULL,
   MotDePasse VARCHAR(200) NOT NULL,
   UserRole VARCHAR(20) DEFAULT 'Visitor',
   PRIMARY KEY(IDUtilisateur)
)
