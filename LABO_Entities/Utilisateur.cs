
namespace LABO_Entities
{
    public class Utilisateur
    {
        [ColumnName(nameof(IDUtilisateur))]
        public int IDUtilisateur { get; set; }

        [ColumnName(nameof(Nom))]
        public string Nom { get; set; }

        [ColumnName(nameof(Prenom))]
        public string Prenom { get; set; }

        [ColumnName(nameof(Email))]
        public string Email { get; set; }

        [ColumnName(nameof(MotDePasse))]
        public string MotDePasse { get; set; }

        [ColumnName(nameof(UserRole))]
        public string UserRole { get; set; }
    }
}
