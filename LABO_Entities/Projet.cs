
namespace LABO_Entities
{
    public class Projet
    {
        public int IDProjet { get; set; }
        public string Nom { get; set; }
        public decimal Montant { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateMiseEnLigne { get; set; }
        public DateTime? DateDeFin { get; set; }

        public int IDUtilisateur { get; set; } // --> FK
    }
}

