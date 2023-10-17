using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABO_Entities
{
    public class Projet
    {
        public int IDProjet { get; set; }
        public string Nom { get; set; }
        public decimal Montant { get; set; }
        public DateOnly DateCreation { get; set; }
        public DateOnly? DateMiseEnLigne { get; set; }
        public DateOnly? DateDeFin { get; set; }

        public int IDUtilisateur { get; set; } // --> FK
    }
}

