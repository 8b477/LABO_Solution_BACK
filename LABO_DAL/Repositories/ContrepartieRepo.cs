using Dapper;

using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_Entities;
using System.Data;


namespace LABO_DAL.Repositories
{
    public class ContrepartieRepo : BaseRepo<ContrepartieDTO, ContrepartiDTOCreate, ContrepartiDTOList, Contrepartie, int, string>, IContrepartieRepo
    {
        public ContrepartieRepo(IDbConnection connection) : base(connection) { }


        #region Mapper
        public override ContrepartieDTO? ToModelCreate(ContrepartiDTOCreate model)
        {
            throw new NotImplementedException();
        }


        public override ContrepartiDTOList? ToModelDisplay(ContrepartieDTO model)
        {
            throw new NotImplementedException();
        }
        #endregion


        /// <summary>
        /// Récupère un projet associé à un utilisateur dans la base de données.
        /// </summary>
        /// <param name="idUser">L'ID de l'utilisateur pour lequel on souhaite obtenir le projet associé.</param>
        /// <returns>Retourne le projet s'il est associé à l'utilisateur, sinon retourne null s'il n'y a pas de correspondance.</returns>
        public async Task<ProjetDTO?> GetProjetByIdUser(int idUser)
        {

            string tableName = "Projet";

            string query = $"SELECT * FROM {tableName} WHERE IDUtilisateur = @Id";
            var result = await _connection.QuerySingleOrDefaultAsync<ProjetDTO>(query, new { Id = idUser });

            if (result is not null)
                return result; // renvoie le projet

            return null;
        }


        public async Task<IEnumerable<ContrepartieDTO>?> GetContrepartieByProjectID(int idProjet)
        {
            string tableName = "Contrepartie";

            string query = $"SELECT * FROM {tableName} WHERE IDProjet = @Id";

            var result = await _connection.QueryAsync<ContrepartieDTO>(query, new { Id = idProjet });


            if (result is not null)
                return result;

            return null;

        }
    }
}
