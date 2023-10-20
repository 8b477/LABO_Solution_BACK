

namespace LABO_DAL.Interfaces
{

    /// <summary>
    /// L'interface générique IRepo définit les opérations courantes pour la gestion des données.
    /// Elle est paramétrée par plusieurs types génériques pour représenter différents modèles de données.
    /// </summary>
    /// <typeparam name="M">Le modèle de données de base (entité en base de données).</typeparam>
    /// <typeparam name="MC">Le modèle de données pour la création ou la mise à jour.</typeparam>
    /// <typeparam name="MD">Le modèle de données pour l'affichage.</typeparam>
    /// <typeparam name="T">Un type générique.</typeparam>
    /// <typeparam name="U">Le type de l'identifiant unique des données.</typeparam>

    public interface IRepo<M, MC, MD, T, U, S>
        where M  : class
        where MC : class
        where MD : class
        where T  : class
        where U  : struct
        where S  : class
    {
        /// <summary>
        /// Crée un nouvel élément en utilisant le modèle de données de base.
        /// </summary>
        /// <param name="item">Le modèle de données de base à créer.</param>
        /// <returns>Une tâche qui représente l'opération de création.</returns>

        Task<bool> Create(M item);


        /// <summary>
        /// Récupère tous les éléments en utilisant le modèle de données de base.
        /// </summary>
        /// <returns>Une tâche qui renvoie une collection d'éléments.</returns>

        Task<IEnumerable<M>> Get();


        /// <summary>
        /// Récupère un élément par son identifiant unique en utilisant le modèle de données d'affichage.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'élément à récupérer.</param>
        /// <returns>Une tâche qui renvoie le modèle de données d'affichage correspondant.</returns>

        Task<MD?> GetById(U id);


        /// <summary>
        /// Récupère un élément par son nom en utilisant le modèle de données d'affichage.
        /// </summary>
        /// <param name="name">Nom de l'élément à récupérer.</param>
        /// <returns>Une tâche qui renvoie le modèle de données d'affichage correspondant.</returns>

        Task<IEnumerable<M>?> GetByString(S name);


        /// <summary>
        /// Met à jour un élément existant par son identifiant unique en utilisant le modèle de données de base.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'élément à mettre à jour.</param>
        /// <param name="item">Le modèle de données de base à utiliser pour la mise à jour.</param>
        /// <returns>Une tâche qui renvoie le modèle de données de base mis à jour.</returns>

        Task<M?> Update(U id, M item);


        /// <summary>
        /// Supprime un élément par son identifiant unique.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'élément à supprimer.</param>
        /// <returns>Une tâche qui représente l'opération de suppression.</returns>

        Task<bool> Delete(U id);


        // Exemple de méthode pour la gestion de l'annulation (décommenter si nécessaire).
        // IAsyncResult? CancelledMethod(CancellationToken cancel);
    }

}
