#region USING
using Dapper;

using LABO_DAL.Interfaces;

using System.Data;
using System.Reflection; 

#endregion


namespace LABO_DAL.Repositories
{
    public abstract class BaseRepo<M, MC, MD, T, U> : IRepo<M, MC, MD, T, U>
     where M : class
     where MC : class
     where MD : class
     where T : class
    {

        #region Constructor

        #region Fields
        protected readonly IDbConnection _connection;
        #endregion

        /// <summary>
        /// Initialise une nouvelle instance de la classe BaseRepo avec une connexion de base de données.
        /// </summary>
        /// <param name="connection">Objet IDbConnection pour interagir avec la base de données.</param>
        public BaseRepo(IDbConnection connection)
        {
            _connection = connection;
        }

        #endregion



        #region Public Methods

        /// <summary>
        /// Récupère toutes les entrées de la table correspondante au modèle M.
        /// </summary>
        /// <returns>Une liste d'objets de type M.</returns>
        public IEnumerable<M> Get()
        {
            // Utilise la Reflection pour obtenir les noms de colonnes à partir des attributs
            var columnNames = typeof(T)
                .GetProperties()
                .Select(property =>
                {
                    var attribute = property.GetCustomAttribute<ColumnNameAttribute>();
                    return attribute?.Name ?? property.Name;
                });

            string columns = string.Join(", ", columnNames);
            string query = $"SELECT {columns} FROM {GetTableName()}";
            return _connection.Query<M>(query);
        }



        /// <summary>
        /// Crée une nouvelle entrée dans la table correspondante au modèle M.
        /// </summary>
        /// <param name="item">Objet de type M à insérer dans la base de données.</param>
        /// <returns>True si l'opération a réussi, sinon False.</returns>
        public bool Create(M item)
        {
            string tableName = GetTableName();
            var propertyDict = GetPropertyDictionary(item);

            // Exclue la colonne identité de la mise à jour ID + nom de la table (convention perso)
            propertyDict.Remove($"ID{tableName}");

            string columns = string.Join(", ", propertyDict.Keys);
            string values = string.Join(", ", propertyDict.Keys.Select(k => "@" + k));
            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            int rowAffected = _connection.Execute(query, item);

            return rowAffected > 0;
        }



        /// <summary>
        /// Supprime une entrée dans la table correspondante au modèle M par ID.
        /// </summary>
        /// <param name="id">ID de l'entrée à supprimer.</param>
        /// <returns>True si l'opération a réussi, sinon False.</returns>
        public bool Delete(U id)
        {
            string tableName = GetTableName();
            string query = $"DELETE FROM {tableName} WHERE ID{tableName} = @Id";
            int rowsAffected = _connection.Execute(query, new { Id = id });
            return rowsAffected > 0;
        }



        /// <summary>
        /// Récupère une entrée de la table correspondante au modèle MD par ID.
        /// </summary>
        /// <param name="id">ID de l'entrée à récupérer.</param>
        /// <returns>Un objet de type MD si trouvé, sinon null.</returns>
        public MD? GetById(U id)
        {
            string tableName = GetTableName();
            string query = $"SELECT * FROM {tableName} WHERE ID{tableName} = @Id";
            M? result = _connection.QuerySingleOrDefault<M>(query, new { Id = id });

            if (result is null)
                return null;

            return ToModelDisplay(result);
        }



        /// <summary>
        /// Met à jour une entrée dans la table correspondante au modèle M par ID.
        /// </summary>
        /// <param name="id">ID de l'entrée à mettre à jour.</param>
        /// <param name="item">Objet de type M contenant les nouvelles données.</param>
        /// <returns>Un objet de type M si la mise à jour a réussi, sinon null.</returns>
        public M? Update(U id, M item)
        {
            string tableName = GetTableName();
            var propertyDict = GetPropertyDictionary(item);

            // Exclue la colonne identité de la mise à jour ID + nom de la table (convention perso)
            propertyDict.Remove($"ID{tableName}");

            // Créez un dictionnaire de paramètres pour les colonnes à mettre à jour
            var updateColumns = propertyDict.Keys
                .Select(key => $"{key} = @{key}");
            string setClause = string.Join(", ", updateColumns);

            // CHECK l'ID avec le WHERE
            string query = $"UPDATE {tableName} SET {setClause} WHERE ID{tableName} = @ID{tableName}";

            // Utilisez un dictionnaire de paramètres dynamiques pour définir les valeurs
            var parameters = new DynamicParameters();

            // Ajoutez l'ID de l'utilisateur à partir de la route
            parameters.Add($"ID{tableName}", id);

            // Ajoutez les params spécifiques des colonnes (à l'exception de la colonne identité donc 'ID')
            foreach (var kvp in propertyDict)
            {
                parameters.Add(kvp.Key, kvp.Value);
            }

            int rowsAffected = _connection.Execute(query, parameters);

            return rowsAffected > 0 ? item : null;
        }

        #endregion



        #region Private Methods

        /// <summary>
        /// Récupère le nom de la table correspondante au modèle T.
        /// </summary>
        /// <returns>Le nom de la table.</returns>
        private string GetTableName()
        {
            return typeof(T).Name;
        }



        // Méthode pour obtenir un dictionnaire de propriétés et valeurs de l'objet modèle
        private Dictionary<string, object?> GetPropertyDictionary(M item)
        {
            return typeof(M)
                .GetProperties()
                .ToDictionary(property => property.Name, property => property.GetValue(item));
        }

        #endregion



        #region Abstract Methods

        /// <summary>
        /// Convertit un objet de modèle MC en un objet de modèle M.
        /// </summary>
        /// <param name="model">Objet de modèle MC à convertir.</param>
        /// <returns>Un objet de modèle M.</returns>
        public abstract M? ToModelCreate(MC model); // create/update form sans demander l'id pour update id passe par la route



        /// <summary>
        /// Convertit un objet de modèle M en un objet de modèle MD pour l'affichage.
        /// </summary>
        /// <param name="model">Objet de modèle M à convertir pour l'affichage.</param>
        /// <returns>Un objet de modèle MD pour l'affichage.</returns>
        public abstract MD? ToModelDisplay(M model); // display form sans mot de passe  

        #endregion
    }
}
