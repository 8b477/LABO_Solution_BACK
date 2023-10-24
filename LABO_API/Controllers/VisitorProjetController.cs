using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_DAL.Repositories;
using LABO_Tools.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CancellationFilter))]
    public class VisitorProjetController : ControllerBase
    {
        #region Dependancy injection

        #region Fields

        private readonly IProjetRepo _projetRepo;
        private readonly IContrepartieRepo _contrepartieRepo;
        private readonly IUserRepo _userRepo;

        #endregion

        public VisitorProjetController(IProjetRepo projetRepo,IContrepartieRepo contrepartie, IUserRepo userRepo)
        {
            _projetRepo = projetRepo;
            _contrepartieRepo = contrepartie;
            _userRepo = userRepo;
        }

        #endregion


        /// <summary>
        /// Récupère la liste des projets.
        /// </summary>
        /// <returns>La liste des projets.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
        {
            var projets = await _projetRepo.GetPagedProjects(page, pageSize);

            if (projets is not null)
            {
                var projetModels = projets.Select(projet =>
                {
                    var contreparties = _contrepartieRepo.GetContrepartieByProjectID(projet.IDUtilisateur);
                    var owner = _userRepo.GetById(projet.IDUtilisateur);

                    var projetModel = new
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
                                Nom = owner?.Result?.Nom ?? "", 
                                Prenom = owner?.Result?.Prenom ?? "",
                            },
                        },
                        Contreparties = contreparties?.Result?
                            .Select(ctp => new
                            {
                                Description = ctp.Description,
                                Montant = ctp.Montant,
                            }),
                    };

                    return projetModel;
                });

                return Ok(projetModels);
            }

            return NoContent();
        } // --> FIX ME : CONTREPARTIE NE S AFFICHE PAS



    }

}
