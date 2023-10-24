using LABO_DAL.DTO;
using LABO_Entities;

namespace LABO_DAL.Interfaces
{
    public interface IContrepartieRepo : IRepo<ContrepartieDTO, ContrepartiDTOCreate, ContrepartiDTOList, Contrepartie, int, string>
    {

        Task<ProjetDTO?> GetProjetByIdUser(int idUser);
        Task<IEnumerable<ContrepartieDTO>?> GetContrepartieByProjectID(int idProjet);

        ContrepartieDTO? ToModelCreate(ContrepartiDTOCreate model);
        ContrepartiDTOList? ToModelDisplay(ContrepartieDTO model);
    }
}
