using LABO_DAL.DTO;


namespace LABO_DAL.Services.Interfaces
{
    public interface IProjetService
    {
        Task<IEnumerable<object>> GetProjetDetails(string nameOfProject);

        Task<IEnumerable<object>> GetAllProjetDetails(int page, int pageSize);
    }
}
