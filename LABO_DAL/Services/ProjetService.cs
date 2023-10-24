using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_DAL.Services.Interfaces;

namespace LABO_DAL.Services
{
    public class ProjetService : IProjetService
    {
        #region Constructeur

        #region Fields
        private readonly IProjetRepo _projetRepo;
        private readonly IContrepartieRepo _contrepartieRepo;
        private readonly IUserRepo _userRepo; 
        #endregion

        public ProjetService(IProjetRepo projetRepo, IContrepartieRepo contrepartieRepo, IUserRepo userRepo)
        {
            _projetRepo = projetRepo;
            _contrepartieRepo = contrepartieRepo;
            _userRepo = userRepo;
        } 

        #endregion


        public async Task<IEnumerable<object>> GetProjetDetails(string nameOfProject)
        {
            var projets = await _projetRepo.GetByString(nameOfProject);

            if (projets is not null)
            {
                var projetModels = new List<object>();

                foreach (var projet in projets)
                {
                    var projetModel = await GetProjetDetailsAsync(projet);
                    projetModels.Add(projetModel);
                }

                return projetModels;
            }

            return Enumerable.Empty<object>();
        }


        public async Task<IEnumerable<object>> GetAllProjetDetails(int page, int pageSize)
        {
            IEnumerable<ProjetDTO> projets = await _projetRepo.GetPagedProjects(page, pageSize);


            if (projets is not null)
            {
                var projetModels = new List<object>();

                foreach (var projet in projets)
                {
                    var projetModel = await GetProjetDetailsAsync(projet);
                    projetModels.Add(projetModel);
                }

                return projetModels;
            }

            return Enumerable.Empty<object>();
        }


        private async Task<object> GetProjetDetailsAsync(ProjetDTO projet)
        {
            var contreparties = await _contrepartieRepo.GetContrepartieByProjectID(projet.IDProjet);
            var owner = await _userRepo.GetById(projet.IDUtilisateur);

            return new
            {
                Projet = new
                {
                    Nom = projet.Nom,
                    MontantTotal = projet.Montant,
                    DateCreation = projet.DateCreation,
                    DateMiseEnLigne = projet.DateMiseEnLigne,
                    DateDeFin = projet.DateDeFin,
                    Proprietaire = new
                    {
                        Nom = owner?.Nom ?? "",
                        Prenom = owner?.Prenom ?? "",
                    },
                },
                Contreparties = contreparties?
                    .Select(ctp => new
                    {
                        Description = ctp.Description,
                        Montant = ctp.Montant,
                    }),
            };
        }

    }
}
