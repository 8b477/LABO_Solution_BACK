﻿
using System.ComponentModel.DataAnnotations;

namespace LABO_DAL.DTO
{
    public class ParticipantDTO
    {
        public int IDUtilisateur { get; set; } // --> FK

        public int IDContrepartie { get; set; } // --> FK

        public DateTime DateParticipation { get; set; }
    }


    public class ParticipantDTOCreate
    {
        public int IDUtilisateur { get; set; } // --> FK

        public int IDContrepartie { get; set; } // --> FK

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateParticipation { get; set; }
    }


    public class ParticipantDTOList
    {
        public int IDUtilisateur { get; set; } // --> FK

        public int IDContrepartie { get; set; } // --> FK

        public DateTime DateParticipation { get; set; }
    }
}
