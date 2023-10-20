

namespace LABO_DAL.DTO
{

    /// <summary>
    /// UserDTO est une classe qui représente la table en base de donnée.
    /// </summary>

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

        [ColumnName(nameof(UserRole))]
        public string UserRole { get; set; }
    }


    /// <summary>
    /// UserDTOCreate est une classe qui représente le modèle visuel pour la création/update d'un utilisateur.
    /// </summary>

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


    /// <summary>
    /// UserDTOList est une classe qui représente la façon d'afficher des utilisateurs.
    /// </summary>

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

        [ColumnName(nameof(UserRole))]
        public string UserRole { get; set; }
    }


    /// <summary>
    /// UserDTORegister représente la façon de log un utilisateur.
    /// </summary>

    public record class UserDTORegister
    {
        [ColumnName(nameof(Email))]
        public string Email { get; set; }

        [ColumnName(nameof(MotDePasse))]
        public string MotDePasse { get; set; }
    }
}


