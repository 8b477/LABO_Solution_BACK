
namespace LABO_DAL.DTO
{
    /// <summary>
    /// ProjetDTO est une classe qui représente la table en base de donnée.
    /// </summary>

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
        public DateTime? DateMiseEnLigne { get; set; } = null;

        [ColumnName(nameof(DateDeFin))]
        public DateTime? DateDeFin { get; set; } = null;

        [ColumnName(nameof(EstValid))] // --> Passe à valide quand 3 contrepartie son lié au projet
        public bool EstValid { get; set; } = false;

        [ColumnName(nameof(IDUtilisateur))]
        public int IDUtilisateur { get; set; } // --> FK
    }


    /// <summary>
    /// ProjetDTOCreate est une classe pour la création de nouveaux projets.
    /// </summary>

    public record class ProjetDTOCreate
    {

        [ColumnName(nameof(Nom))]
        public string Nom { get; set; }

        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

    }


    /// <summary>
    /// ProjetDTOList est une classe pour afficher des projets.
    /// </summary>

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
