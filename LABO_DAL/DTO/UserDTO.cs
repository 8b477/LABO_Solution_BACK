

using System.ComponentModel.DataAnnotations;

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
        [Required]
        [MinLength(2,ErrorMessage = $"Le champ {nameof(Nom)} est requis et doit comporter au min 2 caratères")]
        [MaxLength(50, ErrorMessage = $"Le champ {nameof(Nom)} est requis et doit comporter au max 50 caratères")]
        public string Nom { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = $"Le champ {nameof(Nom)} est requis et doit comporter au min 2 caratères")]
        [MaxLength(50, ErrorMessage = $"Le champ {nameof(Nom)} est requis et doit comporter au max 50 caratères")]
        public string Prenom { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Champ requis, adresse email non valide !")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Champ requis 8 caractères mini, une majuscule, une minuscule, un chiffre et un caractère spécial")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
        public string MotDePasse { get; set; }
    }


    /// <summary>
    /// UserDTOList est une classe qui représente la façon d'afficher des utilisateurs.
    /// </summary>
    public record class UserDTOList
    {

        public int IDUtilisateur { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Email { get; set; }

        public string MotDePasse { get; set; }

        public string UserRole { get; set; }
    }


    /// <summary>
    /// UserDTORegister représente la façon de log un utilisateur.
    /// </summary>
    public record class UserDTORegister
    {
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage = "Champ requis, adresse email non valide !")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password,ErrorMessage ="Champ requis 8 caractères mini, une majuscule, une minuscule, un chiffre et un caractère spécial")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
        public string MotDePasse { get; set; }
    }
}


