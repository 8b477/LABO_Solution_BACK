
namespace LABO_Entities
{
    public class Contrepartie
    {
        public int IDContrepartie { get; set; }
        public decimal Montant { get; set; }
        public string Description { get; set; }
        public int IDProjet { get; set; } // -> FK
    }
}

