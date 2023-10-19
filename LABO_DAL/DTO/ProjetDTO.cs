
namespace LABO_DAL.DTO
{
    public record class ProjetDTO
    {
        [ColumnName(nameof(IDProjet))]
        public int IDProjet { get; set; }

        [ColumnName(nameof(Nom))]
        public string Nom { get; set; }

        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

        [ColumnName(nameof(DateCreation))]
        public DateTime DateCreation { get; set; }

        [ColumnName(nameof(DateMiseEnLigne))]
        public DateTime? DateMiseEnLigne { get; set; }

        [ColumnName(nameof(DateDeFin))]
        public DateTime? DateDeFin { get; set; }


        [ColumnName(nameof(IDUtilisateur))]
        public int IDUtilisateur { get; set; } // --> FK
    }

    public record class ProjetDTOCreate
    {

        [ColumnName(nameof(Nom))]
        public string Nom { get; set; }

        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

        [ColumnName(nameof(DateCreation))]
        public DateTime DateCreation { get; set; }

        [ColumnName(nameof(DateMiseEnLigne))]
        public DateTime? DateMiseEnLigne { get; set; }

        [ColumnName(nameof(DateDeFin))]
        public DateTime? DateDeFin { get; set; }


        [ColumnName(nameof(IDUtilisateur))]
        public int IDUtilisateur { get; set; } // --> FK
    }

    public record class ProjetDTOList
    {
        [ColumnName(nameof(IDProjet))]
        public int IDProjet { get; set; }

        [ColumnName(nameof(Nom))]
        public string Nom { get; set; }

        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

        [ColumnName(nameof(DateCreation))]
        public DateTime DateCreation { get; set; }

        [ColumnName(nameof(DateMiseEnLigne))]
        public DateTime? DateMiseEnLigne { get; set; }

        [ColumnName(nameof(DateDeFin))]
        public DateTime? DateDeFin { get; set; }


        [ColumnName(nameof(IDUtilisateur))]
        public int IDUtilisateur { get; set; } // --> FK
    }
}
