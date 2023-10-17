using LABO_DAL.DTO;



namespace LABO_DAL.Interfaces
{
    public interface IUserRepo
    {
        IEnumerable<UserDTO> Get();
        UserDTO GetById(int id);
        void Create(UserDTO item);
        bool Delete(int id);
        void Update(UserDTO item);
    }
}
