using Dapper;
using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_Entities;
using System.Data;


namespace LABO_DAL.Repositories
{
    public class ProjetRepo : BaseRepo<ProjetDTO, ProjetDTOCreate, ProjetDTOList, Projet, int, string>, IProjetRepo
    {

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe ProjetRepo avec une connexion de base de données.
        /// </summary>
        /// <param name="connection">Objet IDbConnection pour interagir avec la base de données.</param>

        public ProjetRepo(IDbConnection connection) : base(connection) { }

        #endregion


        #region Methods

        /// <summary>
        /// Récupère une entrée 'int' IDUtilisateur pour check ça correspondance en base de donnée dans la table Projet
        /// </summary>
        /// <param name="id">Entier correspondant à l'IDUtilisateur à comparé.</param>
        /// <returns>Retourne 'true' si pas de projet lié à l'utilisateur si non retourne 'false'.</returns>
        public async Task<bool> IsUserEligibleForProjectCreation(int idUser)
        {
            string tableName = GetType().Name; // ProjetRepo
            string modelName = tableName[..tableName.IndexOf("Repo")]; // Projet

            string query = $"SELECT * FROM {modelName} WHERE IDUtilisateur = @Id";
            var result = await _connection.QuerySingleOrDefaultAsync<ProjetDTO>(query, new { Id = idUser });

            if (result is null)
                return true; // true rien n'est trouver donc légitime a la création.

            return false; // fale car un projet lié à l'utilisateur est trouver en base de donnée.
        }


        /// <summary>
        /// Récupère l'ID d'un projet associé à un utilisateur dans la base de données.
        /// </summary>
        /// <param name="idUser">L'ID de l'utilisateur pour lequel on souhaite obtenir l'ID du projet.</param>
        /// <returns>Retourne l'ID du projet s'il est associé à l'utilisateur, sinon retourne 0 s'il n'y a pas de correspondance.</returns>
        public async Task<int> GetIdProjetByIdUser(int idUser)
        {
            string tableName = GetType().Name; // ProjetRepo
            string modelName = tableName[..tableName.IndexOf("Repo")]; // Projet

            string query = $"SELECT * FROM {modelName} WHERE IDUtilisateur = @Id";
            var result = await _connection.QuerySingleOrDefaultAsync<ProjetDTO>(query, new { Id = idUser });

            if (result is not null)
                return result.IDProjet; // renvoie l'id trouver.

            return 0; // pas de correpondance renvoie donc zero.
        }



        /// <summary>
        /// Vérifie l'authentification d'un utilisateur en comparant le mot de passe en clair avec le mot de passe haché stocké en base de données.
        /// </summary>
        /// <param name="email">L'adresse e-mail de l'utilisateur.</param>
        /// <param name="password">Le mot de passe en clair à vérifier.</param>
        /// <returns>Retourne 'true' si l'authentification réussit, sinon 'false'.</returns>
        public async Task<bool> AuthenticateUser(string email, string motDePasse)
        {
            string query = "SELECT MotDePasse FROM Utilisateur WHERE Email = @Email";

            string? hashedPassword = await _connection.QueryFirstOrDefaultAsync<string>(query, new { Email = email });

            if (hashedPassword != null)
            {
                // Utilise BCrypt.Verify pour comparer le mot de passe en clair avec le hachage en base de données
                bool isPasswordValid = BC.Verify(motDePasse, hashedPassword); // BC => BCrypt

                if(isPasswordValid) return true;
            }
            return false;
        }



        public async Task<IEnumerable<ProjetDTO>> GetPagedProjects(int page, int pageSize)
        {
            string tableName = "Projet";

            // Calcule l'offset pour la pagination
            int offset = (page - 1) * pageSize;

            // Utilise Dapper pour exécuter la requête SQL en utilisant une chaîne interpolée pour ajouter les paramètres dynamiques
            string query = $@"
                                SELECT *
                                FROM {tableName}
                                ORDER BY DateCreation DESC
                                OFFSET @Offset ROWS
                                FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new { PageSize = pageSize, Offset = offset };

            var result = await _connection.QueryAsync<ProjetDTO>(query, parameters);

            return result;
        }

        #endregion


        #region Mapper

        /// <summary>
        /// Convertit un modèle de création de projet (ProjetDTOCreate) en un modèle de projet (ProjetDTO).
        /// </summary>
        /// <param name="model">Le modèle de création de projet à convertir.</param>
        /// <returns>Le modèle de projet correspondant ou null si le modèle d'entrée est null.</returns>
        public override ProjetDTO? ToModelCreate(ProjetDTOCreate model)
        {
            if (model is not null)
            {
                return new ProjetDTO()
                {
                    Nom = model.Nom,
                    Montant = model.Montant,
                    DateCreation = DateTime.Today
                };
            }
            return null;
        }


        /// <summary>
        /// Convertit un modèle de projet (ProjetDTO) en un modèle d'affichage de projet (ProjetDTOList).
        /// </summary>
        /// <param name="model">Le modèle de projet à convertir.</param>
        /// <returns>Le modèle d'affichage de projet correspondant ou null si le modèle d'entrée est null.</returns>
        public override ProjetDTOList? ToModelDisplay(ProjetDTO model)
        {
            if (model is not null)
            {
                return new ProjetDTOList()
                {
                    Nom = model.Nom,
                    Montant = model.Montant,
                    DateCreation = model.DateCreation,
                    DateMiseEnLigne = model.DateMiseEnLigne,
                    DateDeFin = model.DateDeFin
                };
            }
            return null;
        }


        #endregion

    }
}

