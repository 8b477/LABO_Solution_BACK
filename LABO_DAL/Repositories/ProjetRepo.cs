using LABO_DAL.DTO;
using LABO_Entities;
using System.Data;
using System.Net.Http;


namespace LABO_DAL.Repositories
{
    public class ProjetRepo : BaseRepo<ProjetDTO, ProjetDTOCreate, ProjetDTOList, Projet, int>
    {

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe ProjetRepo avec une connexion de base de données.
        /// </summary>
        /// <param name="connection">Objet IDbConnection pour interagir avec la base de données.</param>

        public ProjetRepo(IDbConnection connection) : base(connection) { }

        #endregion


        #region Mapper

        /// <summary>
        /// Convertit un modèle de création de projet (ProjetDTOCreate) en un modèle de projet (ProjetDTO).
        /// </summary>
        /// <param name="model">Le modèle de création de projet à convertir.</param>
        /// <returns>Le modèle de projet correspondant ou null si le modèle d'entrée est null.</returns>

        public override ProjetDTO? ToModelCreate(ProjetDTOCreate model)
        {
            if (model is not null)
            {
                return new ProjetDTO()
                {
                    //IDProjet = model.IDUtilisateur, => Je ne souhaite pas renseigner d'ID à la création
                    //IDUtilisateur = model.IDUtilisateur, => Je ne souhaite pas renseigner d'ID à la création
                    Nom = model.Nom,
                    Montant = model.Montant,
                    DateCreation = DateTime.Today
                };
            }
            return null;
        }


        /// <summary>
        /// Convertit un modèle de projet (ProjetDTO) en un modèle d'affichage de projet (ProjetDTOList).
        /// </summary>
        /// <param name="model">Le modèle de projet à convertir.</param>
        /// <returns>Le modèle d'affichage de projet correspondant ou null si le modèle d'entrée est null.</returns>

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

        #endregion

    }
}

