
using System.ComponentModel.DataAnnotations;

namespace LABO_DAL.DTO
{
    public class DonationDTO
    {
        [ColumnName(nameof(IDUtilisateur))]
        public int IDUtilisateur { get; set; } // --> FK

        [ColumnName(nameof(IDProjet))]
        public int IDProjet { get; set; } // --> FK

        [ColumnName(nameof(DateDonation))]
        public DateTime DateDonation { get; set; }

        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

    }

    public class DonationDTOCreate
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateDonation { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Champ requis, entrez une somme positif maximum 10 000$")]
        public decimal Montant { get; set; }
    }

    public class DonationDTOList
    {

        [Required]
        [DataType(DataType.Date)]
        [ColumnName(nameof(DateDonation))]
        public DateTime DateDonation { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Champ requis, entrez une somme positif maximum 10 000$")]
        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

    }
}
