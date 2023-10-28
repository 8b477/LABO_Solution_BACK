using LABO_DAL.DTO;
using LABO_Entities;


namespace LABO_DAL.Interfaces
{
    public interface IProjetRepo : IRepo<ProjetDTO, ProjetDTOCreate, ProjetDTOList, Projet, int, string>
    {

        Task<bool> IsUserEligibleForProjectCreation(int idUser);

        Task<int> GetIdProjetByIdUser(int idUser);

        Task<bool> AuthenticateUser(string email, string motDePasse);

        Task<IEnumerable<ProjetDTO>> GetPagedProjects(int page, int pageSize);


        ProjetDTO? ToModelCreate(ProjetDTOCreate model);

        ProjetDTOList? ToModelDisplay(ProjetDTO model);

    }
}
