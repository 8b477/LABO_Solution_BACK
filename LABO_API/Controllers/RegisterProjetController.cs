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


        /// <summary>
        /// Récupère la liste des projets.
        /// </summary>
        /// <returns>La liste des projets.</returns>
        [HttpGet]                           // --> 'NICE HAVE' : MASQUER ID_USER ET ID_PROJET
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Get()
        {
            var result = await _projetRepo.Get();

            if(result is not null) return Ok(result);

            return NoContent();
        }




        /// <summary>
        /// Crée un nouveaux projet.
        /// </summary>
        /// <param name="model">Modèle projetDTOCreate contenant les informations du projet à créer.</param>
        /// <returns>Le modèle de l'utilisateur créé.</returns>
        [HttpPost]                              // --> 'NICE HAVE' : NOM DU PROJET UNIQUE + limite de 1projet
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(JwtUserIdentifiantFilter))]

        public async Task<IActionResult> Post([FromBody] ProjetDTOCreate model)
        {
            //Récupère l'id de la personne préalablement connecter
            string? identifiant = HttpContext?.Items["identifiant"]?.ToString();
            int id = int.Parse(identifiant);

            ProjetDTO projet = new()
            {
                IDUtilisateur = id, // -> insère l'id lié a l'utilisateur qui créée le projet
                Nom = model.Nom,
                Montant = model.Montant,
                DateCreation = DateTime.Now
            };

            if(projet is not null)
            {
                if(await _projetRepo.Create(projet))
                    return CreatedAtAction(nameof(Post), model);
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
            string? identifiant = HttpContext?.Items["identifiant"]?.ToString();// ---------> REFACTO
            int id = int.Parse(identifiant!);


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

        public async Task<IActionResult> Delete()
        {

            //Récupère l'id de la personne préalablement connecter
            string? identifiant = HttpContext?.Items["identifiant"]?.ToString();// ---------> REFACTO
            int id = int.Parse(identifiant!);

            bool result = await _projetRepo.Delete(id);

            if (result) return NoContent();

            return BadRequest();
        }

    }
}
