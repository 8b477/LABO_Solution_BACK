using LABO_DAL.DTO;

using LABO_Entities;
using System.Data;


namespace LABO_DAL.Repositories
{
    public class ProjetRepo : BaseRepo<ProjetDTO, ProjetDTOCreate, ProjetDTOList, Projet, int>
    {
        public ProjetRepo(IDbConnection connection) : base(connection) { }


        public override ProjetDTO? ToModelCreate(ProjetDTOCreate model)
        {
            if (model is not null)
            {
                return new ProjetDTO()
                {
                    //IDProjet = model.IDUtilisateur, => Je ne souhaite pas renseigner d'ID à la création
                    Nom = model.Nom,
                    Montant = model.Montant,
                    DateCreation = model.DateCreation,
                    DateMiseEnLigne = model.DateMiseEnLigne,
                    DateDeFin = model.DateDeFin,
                    IDUtilisateur = model.IDUtilisateur
                };
            }
            return null;
        }

        public override ProjetDTOList? ToModelDisplay(ProjetDTO model)
        {
            if (model is not null)
            {
                return new ProjetDTOList()
                {
                    IDProjet = model.IDProjet,
                    IDUtilisateur = model.IDUtilisateur,
                    Nom = model.Nom,
                    Montant = model.Montant,
                    DateCreation = model.DateCreation,
                    DateMiseEnLigne = model.DateMiseEnLigne,
                    DateDeFin = model.DateDeFin
                };
            }
            return null;
        }
    }
}
