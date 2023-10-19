

namespace LABO_DAL.DTO
{
    public record class UserDTO
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
    }

    public record class UserDTOCreate
    {

        [ColumnName(nameof(Nom))]
        public string Nom { get; set; }
        [ColumnName(nameof(Prenom))]
        public string Prenom { get; set; }
        [ColumnName(nameof(Email))]
        public string Email { get; set; }
        [ColumnName(nameof(MotDePasse))]
        public string MotDePasse { get; set; }
    }

    public record class UserDTOList
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
    }

    public record class UserDTORegister
    {
        [ColumnName(nameof(Email))]
        public string Email { get; set; }

        [ColumnName(nameof(MotDePasse))]
        public string MotDePasse { get; set; }
    }
}


