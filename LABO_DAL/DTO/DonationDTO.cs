
using System.ComponentModel.DataAnnotations;

namespace LABO_DAL.DTO
{
    public class DonationDTO
    {
        public int IDUtilisateur { get; set; } // --> FK

        public int IDProjet { get; set; } // --> FK

        public DateTime DateDonation { get; set; }

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

        public DateTime DateDonation { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Champ requis, entrez une somme positif maximum 10 000$")]

        public decimal Montant { get; set; }

    }
}
