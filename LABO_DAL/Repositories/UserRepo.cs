using Dapper;
using LABO_DAL.DTO;
using LABO_Entities;
using System.Data;


namespace LABO_DAL.Repositories
{
    public class UserRepo : BaseRepo<UserDTO, UserDTOCreate, UserDTOList, Utilisateur, int>
    {

        /// <summary>
        /// Initialise une nouvelle instance de la classe UserRepo avec une connexion de base de données.
        /// </summary>
        /// <param name="connection">Objet IDbConnection pour interagir avec la base de données.</param>
        public UserRepo(IDbConnection connection) : base(connection) { }



        /// <summary>
        /// Convertit un objet de modèle UserDTOCreate en un objet de modèle UserDTO.
        /// </summary>
        /// <param name="model">Objet de modèle UserDTOCreate à convertir.</param>
        /// <returns>Un objet de modèle UserDTO.</returns>
        public override UserDTO? ToModelCreate(UserDTOCreate model)
        {
            if (model is not null)
            {
                // Hache le mot de passe
                string hashedPassword = BC.HashPassword(model.MotDePasse); // BC => BCrypt
                return new UserDTO()
                {
                    //IDUtilisateur = 0, => Je ne souhaite pas renseigner d'ID à la création
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    Email = model.Email,
                    MotDePasse = hashedPassword // Attribue le mot de passe haché
                };
            }
            return null;
        }



        /// <summary>
        /// Convertit un objet de modèle UserDTO en un objet de modèle UserDTOList pour l'affichage.
        /// </summary>
        /// <param name="model">Objet de modèle UserDTO à convertir pour l'affichage.</param>
        /// <returns>Un objet de modèle UserDTOList pour l'affichage.</returns>
        public override UserDTOList? ToModelDisplay(UserDTO model)
        {
            if (model is not null)
            {
                return new UserDTOList()
                {
                    IDUtilisateur = model.IDUtilisateur,
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    Email = model.Email,
                    MotDePasse = "********" // masquer le mot de passe ici pour l'affichage.
                };
            }
            return null;
        }



        /// <summary>
        /// Vérifie si le mot de passe en clair correspond au mot de passe haché en base de données pour un utilisateur donné.
        /// </summary>
        /// <param name="email">L'adresse e-mail de l'utilisateur.</param>
        /// <param name="motDePasse">Le mot de passe en clair à vérifier.</param>
        /// <returns>True si le mot de passe est valide, sinon False.</returns>
        public async Task<bool> GetById(string email, string motDePasse)
        {
            string query = "SELECT MotDePasse FROM Utilisateur WHERE Email = @Email";

            string? hashedPassword  =await _connection.QueryFirstOrDefaultAsync<string>(query, new { Email = email });

            if (hashedPassword != null)
            {
                // Utilise BCrypt.Verify pour comparer le mot de passe en clair avec le hachage en base de données
                bool isPasswordValid = BC.Verify(motDePasse, hashedPassword); // BC => BCrypt
                return isPasswordValid;
            }

            return false;
        }



    }
}
