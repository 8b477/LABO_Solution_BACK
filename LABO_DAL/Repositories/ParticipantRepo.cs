using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_Entities;
using System.Data;

namespace LABO_DAL.Repositories
{
    public class ParticipantRepo : BaseRepo<ParticipantDTO, ParticipantDTOCreate, ParticipantDTOList, Participant, int, string>, IParticipantRepo
    {
        public ParticipantRepo(IDbConnection connection) : base(connection) { }

        #region Mapper
        public override ParticipantDTO? ToModelCreate(ParticipantDTOCreate model)
        {
            throw new NotImplementedException();
        }

        public override ParticipantDTOList? ToModelDisplay(ParticipantDTO model)
        {
            throw new NotImplementedException();
        } 
        #endregion

    }
}
