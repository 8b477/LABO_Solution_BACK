using Dapper;

using LABO_DAL.Models;


using System.Data;


namespace LABO_DAL.Repositories
{
    public class UserRepo
    {
        private readonly IDbConnection _connection;

        public UserRepo(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<UserModel> Get()
        {
            return _connection.Query<UserModel>("SELECT * FROM Utilisateur");
        }

        public UserModel? GetById(int id)
        {
            return _connection.QuerySingleOrDefault<UserModel>("SELECT * FROM Utilisateur WHERE IDUtilisateur = @Id", new { Id = id });
        }

        public int Create(UserCreateModel model)
        {
            string query = "INSERT INTO Utilisateur (Nom, Prenom, Email, MotDePasse) " +
                           "VALUES (@Nom, @Prenom, @Email, @MotDePasse)";

            return _connection.Execute(query, model);
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM Utilisateur WHERE IDUtilisateur = @Id";

            int rowsAffected = _connection.Execute(query, new { Id = id });

            return rowsAffected > 0;
        }


        public UserCreateModel? Update(int userId, UserCreateModel model)
        {
            string query = "UPDATE Utilisateur " +
                           "SET Nom = @Nom, Prenom = @Prenom, Email = @Email, MotDePasse = @MotDePasse " +
                           "WHERE IDUtilisateur = @UserId";

            // Utilise un objet anonyme pour définir les paramètres de la requête
            var parameters = new
            {
                model.Nom,
                model.Prenom,
                model.Email,
                model.MotDePasse,
                UserId = userId // Identifiant de la route pour l'entité
            };

            int rowsAffected = _connection.Execute(query, parameters);

            return rowsAffected > 0 ? model : null;
        }

    }
}
