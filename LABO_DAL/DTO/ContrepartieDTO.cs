using System.ComponentModel.DataAnnotations;

namespace LABO_DAL.DTO
{
    public class ContrepartieDTO
    {
        [ColumnName(nameof(IDContrepartie))]
        public int IDContrepartie { get; set; }

        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

        [ColumnName(nameof(Description))]
        public string Description { get; set; }

        [ColumnName(nameof(IDProjet))]
        public int IDProjet { get; set; }
    }


    public class ContrepartiDTOCreate
    {
        [Required]
        [Range(0,1000,ErrorMessage = "Champ requis valeur du montant ne peux pas être inférieur à 0 ni supérieur à 1000$")]
        public decimal Montant { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Champ requis, taille minimal attendu : 5")]
        [MaxLength(50, ErrorMessage = "Champ requis, taille maximal attendu : 50")]
        public string Description { get; set; }
    }


    public class ContrepartiDTOList
    {
        public decimal Montant { get; set; }

        public string Description { get; set; }

        public int IDProjet { get; set; }
    }
}
