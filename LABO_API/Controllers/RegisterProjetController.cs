using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_Tools.Filters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CancellationFilter))]
    [ServiceFilter(typeof(JwtUserIdentifiantFilter))]
    [Authorize("RequireRegisterRole")]
    [ApiController]
    public class RegisterProjetController : ControllerBase
    {

        #region Dependancy injection

        #region Fields


        private readonly IProjetRepo _projetRepo;


        #endregion


        public RegisterProjetController(IProjetRepo projetRepo)
        {
            _projetRepo = projetRepo;
        }


        #endregion



        private int GetLoggedInUserId()
        {
            string? identifiant = HttpContext?.Items["identifiant"]?.ToString();

            if (int.TryParse(identifiant, out int id))
            {
                return id;
            }
            return 0;
        }




        /// <summary>
        /// Récupère la liste des projets.
        /// </summary>
        /// <returns>La liste des projets.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] //--> consulter son propre projet ?
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                //Récupère l'id de la personne préalablement connecter
                int id = GetLoggedInUserId();

                int idProjet = await _projetRepo.GetIdProjetByIdUser(id);

                if (idProjet != 0)
                {
                    ProjetDTOList? modelProjet = await _projetRepo.GetById(idProjet);

                    if (modelProjet is not null)
                    {
                        ProjetDTO projet = new()
                        {
                            IDProjet = idProjet,
                            IDUtilisateur = id,
                            Nom = modelProjet.Nom,
                            Montant = modelProjet.Montant,
                            DateCreation = modelProjet.DateCreation,
                            DateMiseEnLigne = modelProjet.DateMiseEnLigne,
                            DateDeFin = modelProjet.DateDeFin
                        };

                        return Ok(projet);
                    }
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la récupération des projets. Source : " + ex.Source + " Message : " + ex.Message);
            }
        }




        /// <summary>
        /// Crée un nouveaux projet.
        /// </summary>
        /// <param name="model">Modèle projetDTOCreate contenant les informations du projet à créer.</param>
        /// <returns>Le modèle de l'utilisateur créé.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //--> check de le côté unique du nom du projet ?
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(JwtUserIdentifiantFilter))]
        public async Task<IActionResult> Post([FromBody] ProjetDTOCreate model)
        {
            try
            {
                //Récupère l'id de la personne préalablement connecter
                int id = GetLoggedInUserId();

                // Vérifie si user peut créer un nouveau projet
                bool result = await _projetRepo.IsUserEligibleForProjectCreation(id);

                if (result)
                {
                    //Faire une vérif vers la db en regardant la table projet si un le user de l'id est déjà lié a un projet
                    ProjetDTO projet = new()
                    {
                        IDUtilisateur = id, // -> insère l'id lié a l'utilisateur qui créée le projet
                        Nom = model.Nom,
                        Montant = model.Montant,
                        DateCreation = DateTime.Now
                    };

                    if (projet is not null)
                    {
                        if (await _projetRepo.Create(projet))
                            return CreatedAtAction(nameof(Post), projet);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de l'ajout d'un projet. Source : " + ex.Source + " Message : " + ex.Message);
            }
        }



        /// <summary>
        /// Supprime un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à supprimer.</param>
        /// <returns>Statut NoContent en cas de suppression réussie.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] ProjetDTOCreate model)
        {
            try
            {
                ProjetDTO? user = _projetRepo.ToModelCreate(model);

                //Récupère l'id de la personne préalablement connecter
                int id = GetLoggedInUserId();


                if (user is not null && user.EstValid == false)
                {
                    var result = await _projetRepo.Update(id, user);

                    if (result is not null)
                        return Ok(model);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la mise à jour d'un projet. Source : " + ex.Source + " Message : " + ex.Message);
            }
        }



        /// <summary>
        /// Met à jour d'un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à mettre à jour.</param>
        /// <param name="model">Modèle projetDTOCreate contenant les informations mises à jour du projet.</param>
        /// <returns>Le modèle du projet mis à jour.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // ============> ADD MODEL ? GENRE MDP + EMAIL
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(UserDTORegister model)
        {
            try
            {
                int id = HttpContext.Session.GetInt32("ID") ?? 0;

                if (await _projetRepo.AuthenticateUser(model.Email, model.MotDePasse) && id != 0)
                {
                    ///récupérer le projet lié a la personne !
                    int idProjet = await _projetRepo.GetIdProjetByIdUser(id);

                    if (idProjet != 0)
                    {
                        bool result = await _projetRepo.Delete(idProjet);
                        if (result)
                            return NoContent();
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la suppresion d'un projet. Source : " + ex.Source + " Message : " + ex.Message);
            }
        }

    }
}
