using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_Entities;

using System.Data;

namespace LABO_DAL.Repositories
{
    public class DonationRepo : BaseRepo<DonationDTO, DonationDTOCreate, DonationDTOList, Donation, int, string>, IDonationRepo
    {
        public DonationRepo(IDbConnection connection) : base(connection) { }

        #region Mapper

        public override DonationDTO? ToModelCreate(DonationDTOCreate model)
        {
            throw new NotImplementedException();
        }

        public override DonationDTOList? ToModelDisplay(DonationDTO model)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods



        #endregion
    }
}
