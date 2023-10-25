
using LABO_DAL.DTO;
using LABO_Entities;

namespace LABO_DAL.Interfaces
{
    public interface IParticipantRepo : IRepo<ParticipantDTO, ParticipantDTOCreate, ParticipantDTOList, Participant, int, string>
    {

    }
}
