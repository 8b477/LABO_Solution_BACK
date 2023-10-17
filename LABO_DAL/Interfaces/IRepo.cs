using LABO_DAL.DTO;



namespace LABO_DAL.Interfaces
{
    public interface IRepo<M, MC, MD, T, U>
        where M : class   // prend un model basic = Entité en base de donnée (M)
        where MC : class  // prend un model pour la creation/update = Entité en base de donnée (MC)
        where MD : class  // prend un model pour l'affichage = Entité en base de donnée (MD)
        where T : class   // un type (T)
    {
        bool Create(M item);
        IEnumerable<M> Get();
        MD GetById(U id);
        M Update(U id,M item);
        bool Delete(U id);
    }
}
