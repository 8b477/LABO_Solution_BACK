
using System.ComponentModel.DataAnnotations;

namespace LABO_DAL.DTO
{
    /// <summary>
    /// ProjetDTO est une classe qui représente la table en base de donnée.
    /// </summary>
    public record class ProjetDTO
    {
        public int IDProjet { get; set; }

        public string Nom { get; set; }

        public decimal Montant { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime? DateMiseEnLigne { get; set; } = null;

        public DateTime? DateDeFin { get; set; } = null;

        public bool EstValid { get; set; } = false;

        public int IDUtilisateur { get; set; } // --> FK
    }


    /// <summary>
    /// ProjetDTOCreate est une classe pour la création de nouveaux projets.
    /// </summary>
    public record class ProjetDTOCreate
    {
        [Required]
        [MinLength(4,ErrorMessage = "Champ requis, 5 cactères minimum attendu pour le Nom du projet")]
        [MaxLength(20,ErrorMessage = "Champ requis, 20 caractères maximum attendu pour le Nom du projet")]
        public string Nom { get; set; }

        [Required]
        [Range(0,20000,ErrorMessage = "Champ requis, le montant ne peux pas être négatif et ne peux être supérieur à 20 000$")]
        public decimal Montant { get; set; }

    }


    /// <summary>
    /// ProjetDTOList est une classe pour afficher des projets.
    /// </summary>
    public record class ProjetDTOList
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
