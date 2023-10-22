using LABO_DAL.DTO;
using LABO_DAL.Repositories;
using LABO_Tools.Filters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CancellationFilter))]
    [Authorize("RequireRegisterRole")]
    [ApiController]
    public class RegisterProjetController : ControllerBase
    {

        #region Dependancy injection

        #region Fields


        private readonly ProjetRepo _projetRepo;


        #endregion


        public RegisterProjetController(ProjetRepo projetRepo)
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
            // Gérez ici le cas où la conversion échoue, par exemple en renvoyant une valeur par défaut ou en levant une exception.
            // Vous pouvez personnaliser cette partie en fonction de votre logique.

            // Exemple : return -1; ou throw new Exception("Impossible de récupérer l'ID de l'utilisateur connecté.");
        }





        /// <summary>
        /// Récupère la liste des projets.
        /// </summary>
        /// <returns>La liste des projets.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _projetRepo.Get();

            if(result is not null) return Ok(result.Select(x => _projetRepo.ToModelDisplay(x)).ToList());

            return NoContent();
        }




        /// <summary>
        /// Crée un nouveaux projet.
        /// </summary>
        /// <param name="model">Modèle projetDTOCreate contenant les informations du projet à créer.</param>
        /// <returns>Le modèle de l'utilisateur créé.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //--> check de le côté unique du nom du projet ?
        [ServiceFilter(typeof(JwtUserIdentifiantFilter))]
        public async Task<IActionResult> Post([FromBody] ProjetDTOCreate model)
        {
            //Récupère l'id de la personne préalablement connecter
            int id = GetLoggedInUserId();

            // Vérifie si user peut créer un nouveau projet
            bool result = await _projetRepo.IsUserEligibleForProjectCreation(id);

            if (result)
            {
                //Faire une vers la db en regardant la table projet si un le user de l'id est déjà lié a un projet
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
                        return CreatedAtAction(nameof(Post), model);
                }
            }
            return BadRequest();
        }



        /// <summary>
        /// Supprime un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à supprimer.</param>
        /// <returns>Statut NoContent en cas de suppression réussie.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] ProjetDTOCreate model)
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



        /// <summary>
        /// Met à jour d'un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à mettre à jour.</param>
        /// <param name="model">Modèle projetDTOCreate contenant les informations mises à jour du projet.</param>
        /// <returns>Le modèle du projet mis à jour.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // ============> ADD MODEL ? GENRE MDP + EMAIL
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(UserDTORegister model)
        {
            //Récupère l'id de la personne préalablement connecter
            int id = GetLoggedInUserId();

            if (await _projetRepo.AuthenticateUser(model.Email, model.MotDePasse))
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

    }
}
