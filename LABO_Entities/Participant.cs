

namespace LABO_Entities
{
    public class Participant
    {
        public int IDUtilisateur { get; set; } // --> FK
        public int IDContrepartie { get; set; } // --> FK
        public DateTime DateParticipation { get; set; }
    }
}
