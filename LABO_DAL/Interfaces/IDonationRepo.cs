using LABO_DAL.DTO;
using LABO_Entities;

namespace LABO_DAL.Interfaces
{
    public interface IDonationRepo : IRepo<DonationDTO, DonationDTOCreate, DonationDTOList, Donation, int, string>
    {
        DonationDTO? ToModelCreate(DonationDTOCreate model);
        DonationDTOList? ToModelDisplay(DonationDTO model);
    }
}
