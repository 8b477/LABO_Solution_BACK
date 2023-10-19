

namespace LABO_DAL.Interfaces
{
    public interface IRepo<M, MC, MD, T, U>
        where M : class   // prend un model basic = Entité en base de donnée (M)
        where MC : class  // prend un model pour la creation/update = Entité en base de donnée (MC)
        where MD : class  // prend un model pour l'affichage = Entité en base de donnée (MD)
        where T : class   // un type (T)
    {
        Task<bool> Create(M item);
        Task<IEnumerable<M>> Get();
        Task<MD?> GetById(U id);
        Task<M?> Update(U id,M item);
        Task<bool> Delete(U id);
    }
}
