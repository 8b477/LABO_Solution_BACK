﻿

namespace LABO_Entities
{
    public class Donation
    {
        public int IDUtilisateur { get; set; } // --> FK
        public int IDProjet { get; set; } // --> FK
        public DateOnly DateDonation { get; set; }
        public decimal Montant { get; set; }
    }
}
